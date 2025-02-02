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
    private PlayerInput input;
    private CheckPlayerGround ground;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<PlayerAnim>();
        input = GetComponent<PlayerInput>();
        playerCoin = GetComponent<PlayerCoin>();
        ground = GetComponent<CheckPlayerGround>();
    }

    // Update is called once per frame
    void Update()
    {
        StartMeditation();
    }

    void StartMeditation()
    {
        if (input.MeditationInput() && ground.IsGrounded())
        {
            isMeditating = true;
            playerCoin.SetCoinStatusWorking(false);
            if (!beginCoroutine)
            {
                beginCoroutine = true;
                Debug.Log("Criou coroutine");
                StartCoroutine(AddCoinWithMeditation());
            }
            //anim.animator.SetBool("Meditate", true);
            //playerManager.StartMeditation();

            /*
            meditationCoroutine = StartCoroutine(MeditationRoutine());

            if (!hasMeditated)
            {
                hasMeditated = true;
                StartCoroutine(HideTutorialAfterDelay(meditateTutorial, 1f));
            }
            */

        }
        else
        {
            isMeditating = false;
            playerCoin.SetCoinStatusWorking(true);
        }
    }    

    public bool PlayerIsMeditating()
    {
        return isMeditating;
    }

    private IEnumerator AddCoinWithMeditation()
    {
        playerCoin.AddCoin(meditationCoinValue);
        yield return new WaitForSeconds(0.5f);
        beginCoroutine = false;
        //StopMeditation();
    }
}
