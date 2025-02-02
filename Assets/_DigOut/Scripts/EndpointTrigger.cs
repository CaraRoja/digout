using UnityEngine;

public class EndpointTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.PlayerReachedEnd();
        }
    }
}
