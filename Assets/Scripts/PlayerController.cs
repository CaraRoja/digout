using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public GameObject pauseMenu;
    public GameObject moveTutorial;
    public GameObject jumpTutorial;
    public GameObject meditateTutorial;
    public GameObject solveTutorial;
    public float radius = .6f;
    public float meditationTime = 3f; // Tempo máximo de meditação
    public float solvingTime = 3f; // Tempo necessário para resolver o problema com ritmo correto
    public float meditationCooldown = 15f; // Tempo de espera antes de poder meditar novamente

    public LayerMask groundLayer;
    public LayerMask platformLayer;

    private Rigidbody2D rb;
    private bool isGrounded = true;
    private PlayerManager playerManager;
    private bool isMeditating = false;
    private bool isSolvingProblem = false;
    private bool isPaused = false;

    private PlayerControls controls;
    private Vector2 movementInput;

    private bool hasMoved = false;
    private bool hasJumped = false;
    private bool hasMeditated = false;
    private bool hasSolved = false;

    private int leftButtonPresses = 0;
    private int rightButtonPresses = 0;
    private float lastLeftButtonTime = 0;
    private float lastRightButtonTime = 0;
    private float bpmThreshold = 240f / 60f; // 240 BPM em pressões por segundo
    private Coroutine solvingCoroutine;
    private Coroutine meditationCoroutine;
    private bool canMeditate = true; // Flag para controlar o cooldown da meditação

    private string currentControlScheme; // Para armazenar o esquema de controle ativo

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerManager = GetComponent<PlayerManager>();

        controls = new PlayerControls();
        controls.PlayerActions.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        controls.PlayerActions.Move.canceled += ctx => movementInput = Vector2.zero;
        controls.PlayerActions.Jump.performed += _ => Jump();
        controls.PlayerActions.Meditate.performed += _ => StartMeditation();
        controls.PlayerActions.Meditate.canceled += _ => StopMeditation();
        controls.PlayerActions.SolveProblemLeft.performed += ctx => StartSolvingProblems("left");
        controls.PlayerActions.SolveProblemRight.performed += ctx => StartSolvingProblems("right");
        controls.PlayerActions.SolveProblemLeft.canceled += ctx => StopSolvingProblems("left");
        controls.PlayerActions.SolveProblemRight.canceled += ctx => StopSolvingProblems("right");
        controls.PlayerActions.Pause.performed += _ => TogglePause();

        // Adiciona um callback para mudar o esquema de controle
        controls.PlayerActions.Move.performed += ctx => currentControlScheme = ctx.control.device.name;
        controls.PlayerActions.Jump.performed += ctx => currentControlScheme = ctx.control.device.name;
    }

    void OnEnable()
    {
        controls.PlayerActions.Enable();
    }

    void OnDisable()
    {
        controls.PlayerActions.Disable();
    }

    void Update()
    {
        if (!isPaused)
        {
            HandleMovement();
            CheckGroundedStatus();
        }
    }

    void HandleMovement()
    {
        if (!isMeditating && !isSolvingProblem)
        {
            if (movementInput.x != 0)
            {
                rb.velocity = new Vector2(movementInput.x * playerManager.speed, rb.velocity.y);
                transform.localScale = new Vector3(Mathf.Sign(movementInput.x), 1, 1);
                animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

                // Tocar som de passos em loop
                if (!playerManager.playerAudioSource.isPlaying || playerManager.playerAudioSource.clip != playerManager.walkClip)
                {
                    playerManager.playerAudioSource.clip = playerManager.walkClip;
                    playerManager.playerAudioSource.loop = true;
                    playerManager.playerAudioSource.Play();
                }

                if (!hasMoved)
                {
                    hasMoved = true;
                    StartCoroutine(HideTutorialAfterDelay(moveTutorial, 1f));
                }
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                animator.SetFloat("Speed", 0);

                // Parar som de passos
                if (playerManager.playerAudioSource.clip == playerManager.walkClip)
                {
                    playerManager.playerAudioSource.Stop();
                    playerManager.playerAudioSource.loop = false;
                }
            }
        }
    }

    void Jump()
    {
        if (isGrounded && !isMeditating && !isSolvingProblem && !isPaused)
        {
            rb.AddForce(new Vector2(0, playerManager.jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetTrigger("Jump");

            // Parar som de passos e tocar som de pulo
            if (playerManager.playerAudioSource.isPlaying && playerManager.playerAudioSource.clip == playerManager.walkClip)
            {
                playerManager.playerAudioSource.Stop();
                playerManager.playerAudioSource.loop = false;
            }
            playerManager.playerAudioSource.PlayOneShot(playerManager.jumpClip);

            if (!hasJumped)
            {
                hasJumped = true;
                StartCoroutine(HideTutorialAfterDelay(jumpTutorial, 1f));
            }
        }
    }

    void StartMeditation()
    {
        if (!isMeditating && !isSolvingProblem && !isPaused && canMeditate)
        {
            isMeditating = true;
            animator.SetBool("Meditate", true);
            playerManager.StartMeditation();
            meditationCoroutine = StartCoroutine(MeditationRoutine());

            if (!hasMeditated)
            {
                hasMeditated = true;
                StartCoroutine(HideTutorialAfterDelay(meditateTutorial, 1f));
            }
        }
    }

    void StopMeditation()
    {
        if (isMeditating)
        {
            isMeditating = false;
            animator.SetBool("Meditate", false);
            playerManager.StopMeditation();
            if (meditationCoroutine != null)
            {
                StopCoroutine(meditationCoroutine);
                meditationCoroutine = null;
            }
            StartCoroutine(MeditationCooldownRoutine());
        }
    }

    private IEnumerator MeditationRoutine()
    {
        yield return new WaitForSeconds(meditationTime);
        StopMeditation();
    }

    private IEnumerator MeditationCooldownRoutine()
    {
        canMeditate = false;
        yield return new WaitForSeconds(meditationCooldown);
        canMeditate = true;
    }

    void StartSolvingProblems(string button)
    {
        if (!isSolvingProblem && !isPaused)
        {
            if (button == "left")
            {
                leftButtonPresses++;
                lastLeftButtonTime = Time.time;
            }
            else if (button == "right")
            {
                rightButtonPresses++;
                lastRightButtonTime = Time.time;
            }

            if (solvingCoroutine == null)
            {
                solvingCoroutine = StartCoroutine(SolvingProblemRoutine());
                StopNearbyEnemies();
                animator.SetBool("Solve", true);
            }
        }
    }

    void StopSolvingProblems(string button)
    {
        if (isSolvingProblem)
        {
            if (button == "left")
            {
                leftButtonPresses--;
            }
            else if (button == "right")
            {
                rightButtonPresses--;
            }

            if (leftButtonPresses <= 0 && rightButtonPresses <= 0)
            {
                StopCoroutine(solvingCoroutine);
                solvingCoroutine = null;
                isSolvingProblem = false;
                animator.SetBool("Solve", false);
            }
        }
    }

    private void StopNearbyEnemies()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1.5f);  // Define o raio conforme necessário
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = Vector2.zero;
                }
                Animator enemyAnimator = hit.GetComponent<Animator>();
                if (enemyAnimator != null)
                {
                    enemyAnimator.SetBool("IsStopped", true);  // Assegure-se de ter esse parâmetro no animator do inimigo
                }
            }
        }
    }

    private IEnumerator SolvingProblemRoutine()
    {
        float startTime = Time.time;

        while (Time.time - startTime < solvingTime)
        {
            if (IsPressingAtCorrectBPM())
            {
                yield return new WaitForSeconds(solvingTime); // Espera os 3 segundos para resolver o problema
                yield return StartCoroutine(ResolveProblems()); // Chama a corrotina corretamente
                yield break;
            }
            yield return null;
        }

        isSolvingProblem = false;
        animator.SetBool("Solve", false);
        leftButtonPresses = 0;
        rightButtonPresses = 0;
        solvingCoroutine = null;
    }


    private bool IsPressingAtCorrectBPM()
    {
        float currentTime = Time.time;
        float leftButtonRate = leftButtonPresses / (currentTime - lastLeftButtonTime + 1e-5f);
        float rightButtonRate = rightButtonPresses / (currentTime - lastRightButtonTime + 1e-5f);

        return leftButtonRate >= bpmThreshold && rightButtonRate >= bpmThreshold;
    }

    private IEnumerator ResolveProblems()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1.5f);  // Define o raio conforme necessário
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                if (enemy != null)
                {
                    yield return StartCoroutine(enemy.ProblemSolving());  // Certifique-se de que 'ProblemSolving' é uma corrotina
                }
            }
            if (hit.CompareTag("Problem"))
            {
                ObstacleScript obstacle = hit.GetComponent<ObstacleScript>();
                if (obstacle != null)
                {
                    yield return StartCoroutine(obstacle.ObstacleTrigger());  // Certifique-se de que 'ObstacleTrigger' é uma corrotina
                }
            }
        }

        if (!hasSolved)
        {
            hasSolved = true;
            yield return StartCoroutine(HideTutorialAfterDelay(solveTutorial, 1f));  // Certifique-se de que 'HideTutorialAfterDelay' é uma corrotina
        }

        isSolvingProblem = false;
        animator.SetBool("Solve", false);
        leftButtonPresses = 0;
        rightButtonPresses = 0;
        solvingCoroutine = null;

        yield return null; // Finaliza a corrotina corretamente
    }


    void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    void CheckGroundedStatus()
    {
        // Ajuste este valor conforme necessário
        isGrounded = Physics2D.OverlapCircle(transform.position, radius, groundLayer | platformLayer);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & (groundLayer | platformLayer)) != 0)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & (groundLayer | platformLayer)) != 0)
        {
            isGrounded = false;
        }
    }

    private IEnumerator HideTutorialAfterDelay(GameObject tutorial, float delay)
    {
        yield return new WaitForSeconds(delay);
        tutorial.SetActive(false);
    }
}
