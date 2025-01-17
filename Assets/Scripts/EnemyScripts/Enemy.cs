using UnityEngine;
using System.Collections;
using System.Linq;

public abstract class Enemy : MonoBehaviour
{
    public float speed; // Velocidade do inimigo
    public int coinLoss; // Quantidade de moedas perdidas na colisão
    public float problemSolvingRadius = 5.0f; // Raio de alcance para resolver problemas

    protected PlayerManager playerManager;
    private EnemyController controller;
    public bool solved = false;
    private bool isSolvingProblem = false;

    protected virtual void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        controller = GetComponent<EnemyController>();
    }

    public abstract void ApplyEffect(bool entering);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!solved && other.gameObject.CompareTag("Player"))
        {
            ApplyEffect(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!solved && other.gameObject.CompareTag("Player"))
        {
            ApplyEffect(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerManager.LoseCoins(coinLoss); // O jogador perde moedas na colisão
        }
    }

    public IEnumerator ProblemSolving()
    {
        if (isSolvingProblem) yield break; // Previne chamadas múltiplas simultâneas
        isSolvingProblem = true;

        // Parar o movimento do inimigo
        if (controller != null)
        {
            controller.StopMoving();
        }

        yield return new WaitForSeconds(playerManager.solvingTime); // Espera o tempo de resolução de problemas

        // Reativa o movimento do inimigo
        if (controller != null)
        {
            controller.StartMoving();
        }

        solved = true;
        isSolvingProblem = false;
        DeactivateClosestEnemies(); // Chama o método para desativar os inimigos mais próximos
    }

    private void DeactivateClosestEnemies()
    {
        // Encontrar todos os inimigos na cena
        var enemies = FindObjectsOfType<Enemy>();

        // Filtrar os inimigos que estão dentro do raio de alcance
        var enemiesInRange = enemies.Where(e => Vector3.Distance(playerManager.transform.position, e.transform.position) <= problemSolvingRadius).OrderBy(e => Vector3.Distance(playerManager.transform.position, e.transform.position)).ToList();

        // Pega o inimigo mais próximo
        if (enemiesInRange.Count > 0)
        {
            enemiesInRange[0].Deactivate(); // Desativa o inimigo mais próximo
            if (enemiesInRange[0] is EnemyFragment && enemiesInRange.Count > 1)
            {
                enemiesInRange[1].Deactivate(); // Desativa o segundo inimigo mais próximo se o primeiro for EnemyFragment
            }
        }
    }

    protected virtual void Deactivate()
    {
        // Desativar todos os BoxCollider2D do inimigo
        Collider2D[] boxColliders = GetComponents<Collider2D>();
        foreach (var collider in boxColliders)
        {
            collider.enabled = false;
        }
    }
}
