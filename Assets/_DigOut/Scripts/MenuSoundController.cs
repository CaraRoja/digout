using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuSoundController : MonoBehaviour
{
    public AudioClip hoverSound;
    public AudioClip clickSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHoverSound()
    {
        audioSource.PlayOneShot(hoverSound);
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
