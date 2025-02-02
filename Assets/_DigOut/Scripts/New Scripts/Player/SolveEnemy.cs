using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//Classe implementada no GameObject SolveRange do Player
public class SolveEnemy : MonoBehaviour
{

    public bool enemyIsInRange = false;
    public EnemyGauge enemyGauge;
    public PlayerSolve solvePlayer;
    //public List<EnemyGauge> enemyGauges;
    public LayerMask enemyLayer;

    // Start is called before the first frame update
    void Start()
    {
        solvePlayer = GetComponentInParent<PlayerSolve>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectEnemyByRaycast();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))// && solvePlayer.IsPlayerPressingSolveInput())
        {
            /*
            enemyIsInRange =true;
            if(!enemyGauges.Contains(collision.GetComponent<EnemyGauge>()) && collision.GetComponent<EnemyGauge>().GaugeIsWorking())
            {
                enemyGauges.Add(collision.GetComponent<EnemyGauge>());
            }
            else if (enemyGauges.Contains(collision.GetComponent<EnemyGauge>()) && !collision.GetComponent<EnemyGauge>().GaugeIsWorking())
            {
                enemyGauges.Remove(collision.GetComponent<EnemyGauge>());
            }
            //collision.GetComponent<EnemyGauge>().DecreaseGauge(5f);
            */
        }
        else
        {
            /*
            enemyIsInRange = false;
            */
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        /*
        if (enemyGauges.Contains(collision.GetComponent<EnemyGauge>()))
        {
            enemyGauges.Remove(collision.GetComponent<EnemyGauge>());
        }
        */
    }

    public bool IsListEnemyEmpty()
    {
        /*
        if (enemyGauges != null)
         return true;
        else return false;
        */
        return false;
    }

    public void DecreaseGaugeEnemy(EnemyGauge enemy)
    {
        /* 
         if (enemyGauges.Count > 0)
         {

             foreach (EnemyGauge enemy in enemyGauges)
             {
                 enemy.DecreaseGauge(value);

             }
         }
        */
        if (enemy != null)
        {
            enemy.DecreaseGauge(solvePlayer.DecreaseValueOfSolve);
        }
    }

    public bool IsEnemyInRange()
    {
        return enemyIsInRange;
    }

    public void DetectEnemyByRaycast()
    {
        Debug.DrawRay(this.transform.parent.position,Vector3.right * 3f * transform.parent.localScale.x, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(this.transform.parent.position, Vector3.right * transform.parent.localScale.x, 3f, LayerMask.GetMask("Enemy"));

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy") && solvePlayer.IsPlayerPressingSolveInput())
            {
                //Debug.Log("Hit player " + hit.collider.gameObject.name);
                DecreaseGaugeEnemy(hit.collider.gameObject.GetComponent<EnemyGauge>());
            }
        }
    }

}
