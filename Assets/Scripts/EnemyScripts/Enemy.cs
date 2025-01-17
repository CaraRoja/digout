using UnityEngine;
using System.Collections;
using System.Linq;

public abstract class Enemy : MonoBehaviour
{
    public float speed; // Velocidade do inimigo
    public int coinLoss; // Quantidade de moedas perdidas na colis�o
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
            playerManager.LoseCoins(coinLoss); // O jogador perde moedas na colis�o
        }
    }

    public IEnumerator ProblemSolving()
    {
        if (isSolvingProblem) yield break; // Previne chamadas m�ltiplas simult�neas
        isSolvingProblem = true;

        // Parar o movimento do inimigo
        if (controller != null)
        {
            controller.StopMoving();
        }

        yield return new WaitForSeconds(playerManager.solvingTime); // Espera o tempo de resolu��o de problemas

        // Reativa o movimento do inimigo
        if (controller != null)
        {
            controller.StartMoving();
        }

        solved = true;
        isSolvingProblem = false;
        DeactivateClosestEnemies(); // Chama o m�todo para desativar os inimigos mais pr�ximos
    }

    private void DeactivateClosestEnemies()
    {
        // Encontrar todos os inimigos na cena
        var enemies = FindObjectsOfType<Enemy>();

        // Filtrar os inimigos que est�o dentro do raio de alcance
        var enemiesInRange = enemies.Where(e => Vector3.Distance(playerManager.transform.position, e.transform.position) <= problemSolvingRadius).OrderBy(e => Vector3.Distance(playerManager.transform.position, e.transform.position)).ToList();

        // Pega o inimigo mais pr�ximo
        if (enemiesInRange.Count > 0)
        {
            enemiesInRange[0].Deactivate(); // Desativa o inimigo mais pr�ximo
            if (enemiesInRange[0] is EnemyFragment && enemiesInRange.Count > 1)
            {
                enemiesInRange[1].Deactivate(); // Desativa o segundo inimigo mais pr�ximo se o primeiro for EnemyFragment
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
