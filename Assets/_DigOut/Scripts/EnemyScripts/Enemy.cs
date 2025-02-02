using UnityEngine;
using System.Collections;
using System.Linq;

public abstract class Enemy : MonoBehaviour
{
    public float speed; // Velocidade do inimigo
    public int coinLoss; // Quantidade de moedas perdidas na colisão
    
    //public float problemSolvingRadius = 5.0f; // Raio de alcance para resolver problemas

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

    public abstract void SetEnemySolved();
    public abstract bool GetEnemySolved();
    public abstract void ChangeTagToSolvedProblem();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!solved && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entrou TRigger");
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
            //playerManager.LoseCoins(coinLoss); // O jogador perde moedas na colisão
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
