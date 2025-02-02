using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerGround : MonoBehaviour
{
    public bool isGrounded;
    public Transform ground;
    public LayerMask whatIsGround;
    public Vector2 GroundDistanceToCheck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
    }

    public void CheckGround()
    {
        this.isGrounded = Physics2D.OverlapBox(this.ground.position, GroundDistanceToCheck, 0f, this.whatIsGround);
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}
