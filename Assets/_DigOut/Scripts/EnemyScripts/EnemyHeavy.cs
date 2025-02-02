using UnityEngine;

public class EnemyHeavy : Enemy
{
    public float increasedGravityScale = 2.0f; // Fator pelo qual a gravidade � aumentada
    private float originalGravityScale; // Para armazenar a gravidade original do jogador

    private void Start()
    {
        speed = 0.8f; // Velocidade espec�fica
        coinLoss = 7; // Moedas perdidas na colis�o
    }

    public override void ApplyEffect(bool entering)
    {
        Rigidbody2D playerRb = FindObjectOfType<PlayerManager>().GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            if (entering) // Se o jogador est� entrando na �rea de efeito
            {
                originalGravityScale = playerRb.gravityScale; // Salvando a gravidade original
                playerRb.gravityScale *= increasedGravityScale; // Aplicando a gravidade aumentada
                Debug.Log("Aumentando a gravidade ao redor do jogador");
            }
            else // Se o jogador est� saindo da �rea de efeito
            {
                playerRb.gravityScale = originalGravityScale; // Restaurando a gravidade original
                Debug.Log("Restaurando a gravidade original do jogador");
            }
        }
    }
    protected override void Deactivate()
    {
        
    }

    public override void SetEnemySolved()
    {
        solved = true;
    }

    public override bool GetEnemySolved()
    {
        return solved;
    }

    public override void ChangeTagToSolvedProblem()
    {
        this.gameObject.tag = "EnemySolved";
    }
}

