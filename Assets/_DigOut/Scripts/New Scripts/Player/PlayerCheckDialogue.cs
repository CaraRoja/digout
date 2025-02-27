using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckDialogue : MonoBehaviour
{
    public PlayerCoin coin;
    public DialogueManager dialogue;
    public Meditation meditation;
    public bool collided = false;

    private void Awake()
    {
        dialogue = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        coin = GetComponent<PlayerCoin>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Dialogue") && !collided)
        {
            collided = true;
            StartCoroutine(DelayDialogueCollision(collision.gameObject.name));
        }
    }

    public IEnumerator DelayDialogueCollision(string dialogueName)
    {
        collided = true;
        dialogue.PlayDialogueOnScene(dialogueName);
        yield return new WaitForSecondsRealtime(0.5f);
        collided = false;
    }
}
