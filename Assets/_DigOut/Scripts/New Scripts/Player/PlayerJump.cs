using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    private PlayerInput input;
    private CheckPlayerGround ground;
    private PlayerMove player;
    private PlayerCoin playerCoin;
    private Meditation meditation;
    private PlayerCheckDialogue checkDialogue;


    public float jumpForce;
    public float maxJumpForce;
    public float minJumpForce;
    public bool isJumping = false;
    public float verticalVelocity;


    // Start is called before the first frame update
    void Start()
    {
        playerCoin = GetComponent<PlayerCoin>();
        input = GetComponent<PlayerInput>();
        ground = GetComponent<CheckPlayerGround>();
        player = GetComponent<PlayerMove>();
        meditation = GetComponent<Meditation>();
        checkDialogue = GetComponent<PlayerCheckDialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateJumpForce();
        Jump();
    }

    public void Jump()
    {
        if (input.JumpInput() && ground.IsGrounded() && !isJumping && !meditation.PlayerIsMeditating() && !checkDialogue.dialogue.DialogueIsRunning())
        {
            isJumping = true;
            player.body.velocity = new Vector2(player.body.velocity.x, jumpForce);
        }

        if (ground.IsGrounded())
        {
            isJumping = false;
        }

        //player.body.velocity = new Vector2(player.body.velocity.x, player.body.velocity.y);
    }

    public float GetVerticalVelocity()
    {
        return player.body.velocity.y;
    }

    public void UpdateJumpForce()
    {
        jumpForce = playerCoin.coin.CalculateLogarithmicInterpolation(minJumpForce, maxJumpForce);
    }
}
