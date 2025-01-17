using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public Transform[] waypoints;
    public float threshold = 0.1f;

    private int currentWaypointIndex = 0;
    private Enemy enemyComponent;
    private bool isMoving = true;  // Controla se o inimigo está se movendo
    private bool problemSolving = false;  // Novo estado para controle de resolução de problemas

    private void Start()
    {
        enemyComponent = GetComponent<Enemy>();
        // Ajustar a direção inicial se necessário
        if (waypoints.Length > 1 && transform.position.x > waypoints[0].position.x)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void Update()
    {
        if (isMoving && !problemSolving)
        {
            MoveBetweenWaypoints();
        }
    }

    public void StopMoving()
    {
        isMoving = false;  // Parar o movimento do inimigo
    }

    public void StartMoving()
    {
        isMoving = true;  // Permite que o inimigo comece a se mover novamente
    }

    private void MoveBetweenWaypoints()
    {
        if (waypoints.Length == 0) return;

        if (enemyComponent != null)
        {
            Vector3 nextPosition = waypoints[currentWaypointIndex].position;
            transform.position = Vector2.MoveTowards(transform.position, nextPosition, enemyComponent.speed * Time.deltaTime);

            // Verifica se o inimigo deve virar (inverter a lógica de comparação para corrigir a direção)
            if (transform.position.x > nextPosition.x && transform.localScale.x > 0 ||
                transform.position.x < nextPosition.x && transform.localScale.x < 0)
            {
                // Inverte a direção no eixo X
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }

            if (Vector2.Distance(transform.position, nextPosition) < threshold)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
        }
    }

    
}
