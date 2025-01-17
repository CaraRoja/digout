using UnityEngine;

public class EnemySlow : Enemy
{
    public float slowDownFactor = 0.5f; // Fator de redução de velocidade

    private void Start()
    {
        speed = 0.5f;
        coinLoss = 4;
    }

    public override void ApplyEffect(bool entering)
    {
        Rigidbody2D playerRb = FindObjectOfType<PlayerManager>().GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            if (entering)
            {
                Debug.Log("Reduzindo a velocidade do jogador");
                playerRb.velocity = new Vector2(playerRb.velocity.x * slowDownFactor, playerRb.velocity.y);
            }
            else
            {
                Debug.Log("Restaurando a velocidade normal do jogador");
                // Considerar a lógica para restaurar corretamente dependendo do sistema de movimento
                playerRb.velocity = new Vector2(playerRb.velocity.x / slowDownFactor, playerRb.velocity.y);
            }
        }
    }
    protected override void Deactivate()
    {

    }
}

