using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [Header("Status do Jogador")]
    public int coins = 100;
    public float jumpForce = 6.5f; // Ajustado
    public float speed = 4.5f; // Ajustado

    [Header("Configurações Máximas e Mínimas")]
    public float maxJumpForce = 10f;
    public float maxSpeed = 7f;
    public float minJumpForce = 4f;
    public float minSpeed = 2f;

    [Header("Meditação e Resolução de Problemas")]
    public float meditationCoinRate = 2f; // Ajustado
    public float solvingTime = 2f;

    [Header("Audio Clips")]
    public AudioClip walkClip;  // Efeito sonoro de andar
    public AudioClip jumpClip;  // Efeito sonoro de pular
    public AudioClip solvingClip;  // Efeito sonoro de resolver problemas
    public AudioClip meditatingClip;  // Efeito sonoro de meditar

    [Header("Audio Source")]
    public AudioSource playerAudioSource; // Fonte de áudio do jogador
    public AudioSource bgmAudioSource; // Fonte de áudio da música de fundo

    private Coroutine meditationRoutine;

    private void Start()
    {
        StartCoroutine(LoseCoinsOverTime());
        SetInitialPlayerStats();
        GameManager.Instance.UpdateSaturation(coins);

        // Ajusta os volumes
        playerAudioSource.volume = bgmAudioSource.volume * 0.5f; // Metade do volume da música de fundo
    }

    private void Update()
    {
        CheckForFalls();
    }

    private void SetInitialPlayerStats()
    {
        jumpForce = 6.5f; // Ajustado
        speed = 4.5f; // Ajustado
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdatePlayerStats();
    }

    public void LoseCoins(int amount)
    {
        coins = Mathf.Max(coins - amount, 0);
        UpdatePlayerStats();
    }

    private IEnumerator LoseCoinsOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f);
            LoseCoins(3); // Ajustado
            UpdatePlayerStats();
        }
    }

    private void UpdatePlayerStats()
    {
        jumpForce = CalculateLogarithmicInterpolation(minJumpForce, maxJumpForce, coins);
        speed = CalculateLogarithmicInterpolation(minSpeed, maxSpeed, coins);
        GameManager.Instance.UpdateSaturation(coins);
    }

    private float CalculateLogarithmicInterpolation(float min, float max, int coins)
    {
        float baseValue = (min + max) / 2;

        if (coins <= 100)
        {
            float normalized = Mathf.Log10(1 + 9 * (coins / 100f));
            return Mathf.Lerp(min, baseValue, normalized / Mathf.Log10(10));
        }

        if (coins <= 200)
        {
            float normalized = Mathf.Log10(1 + 9 * ((coins - 100f) / 100f));
            return Mathf.Lerp(baseValue, max, normalized / Mathf.Log10(10));
        }

        return max;
    }

    private void CheckForFalls()
    {
        if (transform.position.y < -10)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Fim de jogo!");
    }

    public void StartMeditation()
    {
        if (meditationRoutine == null)
        {
            playerAudioSource.clip = meditatingClip;
            playerAudioSource.Play();
            meditationRoutine = StartCoroutine(GainCoinsWhileMeditating());
        }
    }

    public void StopMeditation()
    {
        if (meditationRoutine != null)
        {
            StopCoroutine(meditationRoutine);
            playerAudioSource.Stop();
            meditationRoutine = null;
        }
    }

    private IEnumerator GainCoinsWhileMeditating()
    {
        while (true)
        {
            AddCoins((int)meditationCoinRate);
            yield return new WaitForSeconds(1f);
        }
    }

    public void StartSolvingProblems()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 4f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Problem") || hit.CompareTag("Enemy"))
            {
                Debug.Log("Entrou");
                StartCoroutine(SolveProblem(hit.gameObject));
            }
        }
    }

    private IEnumerator SolveProblem(GameObject problem)
    {
        if (problem.CompareTag("Enemy"))
        {
            // Inimigo
            Enemy enemy = problem.GetComponent<Enemy>();
            if (enemy != null)
            {

                

                playerAudioSource.clip = solvingClip;
                playerAudioSource.Play();


                enemy.ProblemSolving();


                playerAudioSource.Stop();
            }
        }
        else if (problem.CompareTag("Problem"))
        {
            // Obstáculo (não tem Rigidbody)
            playerAudioSource.clip = solvingClip;
            playerAudioSource.Play();

            yield return new WaitForSeconds(solvingTime);  // Tempo necessário para resolver o problema

            Collider2D collider = problem.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;  // Desabilita o colisor do obstáculo
            }

            playerAudioSource.Stop();
        }
    }

}
