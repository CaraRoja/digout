using UnityEngine;
using System.Collections;

public class EnemyFragment : Enemy
{
    public GameObject miniEnemyPrefab;
    public int numberOfFragments = 2;
    public float currentScale = 1.0f;
    public float scaleDecrement = 0.25f;
    public float minimumScale = 0.25f;
    public float jumpForce = 2.5f; // Reduzi o valor da força de impulso
    public float multiplyCooldown = 1.5f; // Tempo de espera entre multiplicações

    private EnemyGauge gauge;
    private Rigidbody2D rb;
    private Animator animator;
    private bool canMultiply = true; // Variável para controlar a multiplicação
    private bool isFragment = false; // Variável para indicar se é um fragmento

    [Header("Audio Clip")]
    public AudioClip fragmentSound; // Som ao se fragmentar

    [Header("Audio Source")]
    public AudioSource enemyAudioSource; // Fonte de áudio do inimigo

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        gauge = GetComponent<EnemyGauge>();

        speed = 1.0f;
        coinLoss = 2; // Ajustado
    }

    private void Start()
    {
        Jump();

        if (isFragment)
        {
            StartCoroutine(BufferAfterCreation());
        }
    }

    private void Update()
    {
        DeactivateEnemy();
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        animator.SetTrigger("Jump");
    }

    public override void ApplyEffect(bool entering)
    {
        if (!solved && entering && canMultiply)
        {
            FragmentEnemy();
        }
    }

    private void FragmentEnemy()
    {
        if (currentScale > minimumScale)
        {
            float newScale = currentScale - scaleDecrement;
            for (int i = 0; i < numberOfFragments; i++)
            {
                GameObject fragment = Instantiate(miniEnemyPrefab, transform.position, Quaternion.identity);
                var fragmentEnemy = fragment.GetComponent<EnemyFragment>();
                fragmentEnemy.currentScale = newScale;
                fragmentEnemy.isFragment = true; // Marca a instância como um fragmento
                fragmentEnemy.scaleDecrement = this.scaleDecrement; // Mantém a taxa de redução
                fragment.transform.localScale = new Vector3(newScale, newScale, 1);
            }
            enemyAudioSource.PlayOneShot(fragmentSound);
            Destroy(gameObject);
        }
    }

    private IEnumerator BufferAfterCreation()
    {
        canMultiply = false;
        yield return new WaitForSeconds(multiplyCooldown);
        canMultiply = true;
    }

    private void DeactivateEnemy()
    {
        if (!gauge.GaugeIsWorking() && !GetEnemySolved())
        {
            Deactivate();
            SetEnemySolved();
            ChangeTagToSolvedProblem();
        }
    }

    public override void SetEnemySolved()
    {
        solved = true;
    }

    public override bool GetEnemySolved()
    {
        return solved;
    }

    public override void ChangeTagToSolvedProblem()
    {
        this.gameObject.tag = "EnemySolved";
    }

    protected override void Deactivate()
    {
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        Transform childTransform = transform.GetChild(0);
        if (childTransform != null)
        {
            Renderer renderer = childTransform.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.SetColor("_CurrentColor", new Color(0.0f, 0.898f, 0.525f, 1.0f));
            }
        }

        base.Deactivate();
    }
}
