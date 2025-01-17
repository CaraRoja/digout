using UnityEngine;

public class AdvancedAnimationController : MonoBehaviour
{
    public AnimationData[] animations; // Array dos objetos de anima��o

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private int currentAnimationIndex = -1;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Atualiza o material com base na anima��o atual
        UpdateMaterialBasedOnAnimation();
        // Atualiza a m�scara com base no quadro atual da anima��o
        UpdateMaskBasedOnAnimationFrame();
    }

    private void UpdateMaterialBasedOnAnimation()
    {
        int animationIndex = animator.GetInteger("AnimationIndex"); // Sup�e-se que voc� controle as anima��es com um par�metro "AnimationIndex"

        if (animationIndex != currentAnimationIndex)
        {
            spriteRenderer.material = animations[animationIndex].material;
            currentAnimationIndex = animationIndex;
        }
    }

    private void UpdateMaskBasedOnAnimationFrame()
    {
        int animationIndex = animator.GetInteger("AnimationIndex");
        int frameIndex = (int)(animator.GetCurrentAnimatorStateInfo(0).normalizedTime * animations[animationIndex].masks.Length) % animations[animationIndex].masks.Length;
        spriteRenderer.material.SetTexture("_MaskTex", animations[animationIndex].masks[frameIndex]);
    }
}
