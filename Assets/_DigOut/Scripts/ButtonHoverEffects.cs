using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonHoverEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI buttonText;
    private Vector3 originalScale;
    private Color originalColor;
    public Color hoverColor = new Color(1f, 1f, 0.9f); // Offwhite color
    public float hoverScaleFactor = 1.1f;
    public AudioClip hoverSound; // Adicione esta linha para o som de hover

    private AudioSource audioSource;

    void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            originalColor = buttonText.color;
            originalScale = buttonText.transform.localScale;
        }

        audioSource = gameObject.AddComponent<AudioSource>(); // Adicionar um AudioSource ao GameObject
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Hover Enter");
        if (buttonText != null)
        {
            buttonText.color = hoverColor;
            buttonText.transform.localScale = originalScale * hoverScaleFactor;
        }

        if (hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound); // Tocar o som de hover
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Hover Exit");
        if (buttonText != null)
        {
            buttonText.color = originalColor;
            buttonText.transform.localScale = originalScale;
        }
    }
}
