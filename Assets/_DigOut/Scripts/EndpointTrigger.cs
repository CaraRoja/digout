using UnityEngine;

public class EndpointTrigger : MonoBehaviour
{
    public EndLevel completeLevel;
    private bool levelIsComplete = false;

    private void Start()
    {
        completeLevel = GameObject.Find("EndLevelHandler").GetComponent<EndLevel>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !levelIsComplete)
        {
            levelIsComplete = true;
            completeLevel.ShowVictoryMenu();
        }
    }
}
