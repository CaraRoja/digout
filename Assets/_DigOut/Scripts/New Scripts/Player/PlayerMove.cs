using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D body;

    public float horizontalMovement;
    public float speed;
    public float jumpSpeed;
    public float minSpeed;
    public float maxSpeed;
    public bool rightDirection = true;

    private PlayerInput input;
    private CheckPlayerGround playerGround;
    private Meditation meditation;
    private PlayerCoin playerCoin;
    private PlayerCheckDialogue checkDialogue;

    private Vector3 rightScale = new Vector3(1f, 1f, 1f);
    private Vector3 leftScale = new Vector3(-1f, 1f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerGround = GetComponent<CheckPlayerGround>();
        input = GetComponent<PlayerInput>();
        meditation = GetComponent<Meditation>();
        playerCoin = GetComponent<PlayerCoin>();
        checkDialogue = GetComponent<PlayerCheckDialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeDirection();
        UpdateSpeed();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void Move() 
    {
        if (!meditation.PlayerIsMeditating() && !checkDialogue.dialogue.DialogueIsRunning())
        {
            horizontalMovement = input.HorizontalInput();
            this.body.velocity = new Vector2(horizontalMovement * speed, this.body.velocity.y);
        }
        else
        {
            this.body.velocity = new Vector2(0f, this.body.velocity.y);
        }
    }

    public void ChangeDirection()
    {
        if (!checkDialogue.dialogue.DialogueIsRunning())
        {
            if (input.HorizontalInput() > 0f && !rightDirection)
            {
                rightDirection = true;
            }
            else if (input.HorizontalInput() < 0f && rightDirection)
            {
                rightDirection = false;
            }

            ChangeScale();
        }
        
    }

    public void ChangeScale()
    {
        if (rightDirection)
        {
            this.transform.localScale = rightScale;
        }
        else if (!rightDirection)
        {
            this.transform.localScale = leftScale;
        }
    }

    private void UpdateSpeed()
    {
        speed = playerCoin.coin.CalculateLogarithmicInterpolation(minSpeed, maxSpeed);
    }
}
