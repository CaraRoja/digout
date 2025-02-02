using UnityEngine;
public class EnemyNevoa : Enemy
{
    public float visibilityReductionFactor = 0.5f; // Exemplo de fator de redu��o de visibilidade

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
            // Aqui voc� reduziria a visibilidade, como diminuir a intensidade de ilumina��o ao redor do jogador
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
