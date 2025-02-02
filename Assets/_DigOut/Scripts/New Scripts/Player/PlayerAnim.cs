using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{

    
    private PlayerInput input;
    private Animator anim;
    private PlayerJump jump;
    private CheckPlayerGround ground;
    private Meditation meditation;

    //private PlayerMove player;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();  
        input = GetComponent<PlayerInput>();
        ground = GetComponent<CheckPlayerGround>();
        jump = GetComponent<PlayerJump>();
        meditation = GetComponent<Meditation>();

    }

    // Update is called once per frame
    void Update()
    {
        PlayerAnimation();
    }

    public void PlayerAnimation()
    {
        anim.SetFloat("Speed", Mathf.Abs(input.HorizontalInput()));

        anim.SetFloat("JumpSpeed", jump.GetVerticalVelocity());

        anim.SetBool("Grounded", ground.IsGrounded());

        anim.SetBool("Meditate", meditation.PlayerIsMeditating());

        //anim.SetBool("Meditate", meditate.GetMeditationStatus());

    }
}
