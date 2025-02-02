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

    public bool coroutineIsRunning = false;

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

        RaycastHit2D hitMaterial = Physics2D.Raycast(this.transform.parent.position, Vector3.right * transform.parent.localScale.x, 3f, LayerMask.GetMask("FadeObstacle"));

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy") && solvePlayer.IsPlayerPressingSolveInput())
            {
                //Debug.Log("Hit player " + hit.collider.gameObject.name);
                DecreaseGaugeEnemy(hit.collider.gameObject.GetComponent<EnemyGauge>());
            }
        }

        if (hitMaterial.collider != null) 
        {
            if (hitMaterial.collider.CompareTag("FadeObstacle") && solvePlayer.IsPlayerPressingSolveInput())
            {
                if (hitMaterial.collider.GetComponent<MaterialObjectHandler>().IsWorking() && !coroutineIsRunning)
                {
                    StartCoroutine(DelayIncreaseFadeObstacleValue(hitMaterial.collider.GetComponent<MaterialObjectHandler>()));
                    Debug.Log("ENTROU");
                }
            }
        }
    }

    public IEnumerator DelayIncreaseFadeObstacleValue(MaterialObjectHandler mat )
    {
        coroutineIsRunning = true;
        mat.SetWorkingStatus(false);
        yield return new WaitForSeconds(0.2f);
        mat.SetWorkingStatus(true);
        coroutineIsRunning = false;
    }

}
