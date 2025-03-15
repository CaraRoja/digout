using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meditation : MonoBehaviour
{
    public PlayerCoin playerCoin;
    public bool isMeditating;
    public float meditationCoinValue = 1f;
    public bool beginCoroutine = false;

    private Coroutine co;
    private PlayerAnim anim;
    private PlayerInputHandler input;
    private CheckPlayerGround ground;
    private PlayerCheckDialogue dialogue;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<PlayerAnim>();
        input = GetComponent<PlayerInputHandler>();
        playerCoin = GetComponent<PlayerCoin>();
        ground = GetComponent<CheckPlayerGround>();
        dialogue = GetComponent<PlayerCheckDialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        StartMeditation();

    }

    public void StartMeditation()
    {
        if (ground.IsGrounded() && !dialogue.dialogue.DialogueIsRunning() && input.MeditationInputIsActive())
        {
            isMeditating = true;
            playerCoin.SetCoinStatusWorking(false);
            playerCoin.AddCoinWithTime();

        }
        else if (!input.MeditationInputIsActive())
        {
            isMeditating = false;
            playerCoin.SetCoinStatusWorking(true);
        }

    }

    public bool PlayerIsMeditating()
    {
        return isMeditating;
    }
}
