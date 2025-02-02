using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float xInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //HorizontalInput();
        //JumpInput();
    }

    public float HorizontalInput()
    {
        return xInput = Input.GetAxis("Horizontal");
    }

    public bool JumpInput()
    {
        return Input.GetKeyDown(KeyCode.Space);    
    }

    public bool MeditationInput()
    {
        return Input.GetKey(KeyCode.M);
    }

    public bool SolveInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
}
