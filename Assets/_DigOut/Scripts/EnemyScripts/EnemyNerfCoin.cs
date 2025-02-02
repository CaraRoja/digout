using UnityEngine;

public class EnemyNerfCoin : Enemy
{
    private void Start()
    {
        speed = 1.2f;
        coinLoss = 6;
    }

    public override void ApplyEffect(bool entering)
    {
        if (entering)
        {
            Debug.Log("Emitindo sussurros que impactam negativamente a coleta de moedas");
            // Implementação do efeito que aumenta a perda de moedas
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
