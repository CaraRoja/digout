using UnityEngine;

public class AnimationMaterialController : MonoBehaviour
{
    public Material[] animationMaterials; // Array de materiais para cada animação

    private Animator animator;
    private Renderer renderer;
    private int currentAnimationIndex = -1;

    void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        int animationIndex = (int)(animator.GetCurrentAnimatorStateInfo(0).normalizedTime * animationMaterials.Length) % animationMaterials.Length;
        if (animationIndex != currentAnimationIndex)
        {
            renderer.material = animationMaterials[animationIndex];
            currentAnimationIndex = animationIndex;
        }
    }
}
