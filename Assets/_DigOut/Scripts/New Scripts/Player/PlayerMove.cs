using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D body;

    public float horizontalMovement;
    public float speed;
    public float jumpSpeed;
    public bool rightDirection = true;

    private PlayerInput input;
    private CheckPlayerGround playerGround;
    private Meditation meditation;

    private Vector3 rightScale = new Vector3(1f, 1f, 1f);
    private Vector3 leftScale = new Vector3(-1f, 1f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerGround = GetComponent<CheckPlayerGround>();
        input = GetComponent<PlayerInput>();
        meditation = GetComponent<Meditation>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeDirection();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void Move() 
    {
        if (!meditation.PlayerIsMeditating())
        {
            horizontalMovement = input.HorizontalInput();
            this.body.velocity = new Vector2(horizontalMovement * speed, this.body.velocity.y);
        }
        else
        {
            this.body.velocity = Vector2.zero;
        }
    }

    public void ChangeDirection()
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
}
