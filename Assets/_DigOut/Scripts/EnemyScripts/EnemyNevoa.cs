using UnityEngine;
public class EnemyNevoa : Enemy
{
    public float visibilityReductionFactor = 0.5f; // Exemplo de fator de redução de visibilidade

    private void Start()
    {
        speed = 2.0f;
        coinLoss = 5;
    }

    public override void ApplyEffect(bool entering)
    {
        if (entering)
        {
            Debug.Log("Reduzindo visibilidade do jogador");
            // Aqui você reduziria a visibilidade, como diminuir a intensidade de iluminação ao redor do jogador
        }
        else
        {
            Debug.Log("Restaurando visibilidade do jogador");
            // Restaurar a visibilidade original
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
