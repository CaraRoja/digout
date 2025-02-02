using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerSolve : MonoBehaviour
{

    public bool isSolving;
    public bool canMeditate;
    public float DecreaseValueOfSolve = 10f;
    //public float meditationTime;

    /*
    public float solveTime;
    public float solveLastTime;
    */

    private PlayerAnim anim;
    private PlayerInput input;
    public SolveEnemy solveEnemy;
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        solveEnemy = GetComponentInChildren<SolveEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerIsSolving();
    }

    public void PlayerIsSolving()
    {
        /*
        if (input.SolveInput())
        {
            solveTime = Time.time;
            if (solveLastTime == 0f)
            {
                solveLastTime = solveTime;
            }
            else
            {
                //Debug.Log("Time first press = " + solveTime);
               // Debug.Log("Time last press = " + solveLastTime);
                solveLastTime = solveTime;
            }

            if (solveEnemy.IsEnemyInRange())
            {

            }
        */
            /*
            if (!solveRange.IsListEnemyEmpty())
            {
                Debug.Log("Existe lista");
                foreach (EnemyGauge enemy in solveRange.enemyGauges)
                {
                    enemy.DecreaseGauge(5f);
                }
            }
            */
            //Debug.Log("Apertou o botão");
            //solveEnemy.DecreaseGaugeEnemy(5f);
        //}
    
    }

    public bool IsPlayerPressingSolveInput()
    {
        return input.SolveInput();
    }
}
