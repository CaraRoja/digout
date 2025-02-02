using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    //public AudioSource musicSource;  // Para m�sica de fundo
    public AudioSource sfxSource;    // Para efeitos sonoros gerais

    [Header("Sound Effects")]
    public AudioClip backgroundMusic;  // M�sica de fundo

    private bool isMuted = false;

    void Awake()
    {
        // Implementa��o do padr�o Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Manter o SoundManager persistente entre cenas
            Debug.Log("SoundManager: Instance set in Awake");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    // M�todo para tocar efeitos sonoros gen�ricos
    public void PlaySfx(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);  // Tocar o efeito sonoro uma vez
        }
    }



    // M�todo para mutar todos os sons
    public void MuteAll()
    {
        isMuted = true;
        if (sfxSource != null) sfxSource.mute = true;
    }

    // M�todo para desmutar todos os sons
    public void UnmuteAll()
    {
        isMuted = false;
        if (sfxSource != null) sfxSource.mute = false;
    }

    // M�todo para alternar entre mutar e desmutar
    public void ToggleSound()
    {
        if (isMuted)
        {
            UnmuteAll();
        }
        else
        {
            MuteAll();
        }
    }

    // M�todo para definir o estado do som
    public void SetSoundState(bool isOn)
    {
        if (isOn)
        {
            UnmuteAll();
        }
        else
        {
            MuteAll();
        }
    }
}
