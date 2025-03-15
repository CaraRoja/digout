using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerMove move;
    private PlayerJump jump;
    private Meditation meditation;
    private PlayerCheckDialogue checkDialogue;

    private float xInput;
    public bool meditationIsActive = false;
    public bool solveLeft = false;
    public bool solveRight = false;
    public bool nextSolveInput = false;
    public bool solvingProblem = false;

    public Coroutine coroutineLeft = null;
    public Coroutine coroutineRight = null;
    public Coroutine coroutineSolve = null;

    private void Awake()
    {
        this.playerInput = GetComponent<PlayerInput>();
        PlayerControls playerInputActions = new PlayerControls();
        //playerInputActions.Player.Enable();

        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.Player.Move.performed += HorizontalMovement;
        playerInputActions.Player.Meditate.performed += Meditation;
        playerInputActions.Player.SolveProblemLeft.performed += SolveInputLeft;
        playerInputActions.Player.SolveProblemRight.performed += SolveInputRight;

        playerInputActions.Dialogue.SkipDialogue.performed += SkipDialogueInput;
        
    }
    // Start is called before the first frame update
    public void Start()
    {
        move = GetComponent<PlayerMove>();
        jump = GetComponent<PlayerJump>();
        meditation = GetComponent<Meditation>();
        checkDialogue = GetComponent<PlayerCheckDialogue>();
    }

    // Update is called once per frame
    public void Update()
    {
        //HorizontalInput();
        //JumpInput();
    }

    public void CheckInputActionMap()
    {
        if (checkDialogue.dialogue.DialogueIsRunning())
        {
            playerInput.actions.FindActionMap("Dialogue").Enable();
            playerInput.actions.FindActionMap("Player").Disable();
        }
        else
        {
            playerInput.actions.FindActionMap("Dialogue").Disable();
            playerInput.actions.FindActionMap("Player").Enable();
        }
    }

    public float HorizontalInput()
    {
        return xInput = Input.GetAxis("Horizontal");
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            jump.Jump();
        }

        if (context.canceled)
        {
        }

    }

    public void HorizontalMovement(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        move.SetHorizontalValue(inputVector);
    }

    public void Meditation(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            meditationIsActive = true;
        }

        if (context.canceled)
        {
            meditationIsActive = false;
        }
        
    }

    public bool MeditationInputIsActive()
    {
        return meditationIsActive;
    }

    public bool SolveInput()
    {
        return solvingProblem;
    }

    public void SkipDialogueInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            checkDialogue.SetDialogueSkipStatus();
        }

    }

    public void SolveInputLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (solveLeft.Equals(true) )
            {
                StopCoroutine(coroutineLeft);
                solveLeft = false;
            }
            else
            {
                solveLeft = true;
                coroutineLeft = StartCoroutine(DelayLeftInput());

                if (nextSolveInput)
                {
                    nextSolveInput = false;
                    solvingProblem = true;
                    if(coroutineSolve != null)
                        StopCoroutine(coroutineSolve);
                    coroutineSolve = StartCoroutine(DelaySolveInput());
                }
            }
            
        }

    }

    public void SolveInputRight(InputAction.CallbackContext context)
    {
        if (context.performed )
        {
            if (solveRight.Equals(true))
            {
                StopCoroutine(coroutineRight);
                solveRight = false;
            }
            else
            {
                solveRight = true;
                coroutineRight = StartCoroutine(DelayRightInput());

                if (!nextSolveInput)
                {
                    nextSolveInput = true;
                    solvingProblem = true;
                    if (coroutineSolve != null)
                        StopCoroutine(coroutineSolve);
                    coroutineSolve = StartCoroutine(DelaySolveInput());
                }
            }
        }

    }

    public IEnumerator DelayLeftInput()
    {
        yield return new WaitForSeconds(0.1f);
        solveLeft = false;
    }
    public IEnumerator DelayRightInput()
    {
        yield return new WaitForSeconds(0.1f);
        solveRight = false;
    }

    public IEnumerator DelaySolveInput()
    {
        yield return new WaitForSeconds(0.2f);
        solvingProblem = false;
    }
}
