using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource musicSource;  // Para m�sica de fundo
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
        Debug.Log("SoundManager: Start called");

        if (musicSource == null)
        {
            Debug.LogError("SoundManager: musicSource is not assigned!");
            return;
        }

        PlayMusic(backgroundMusic);
    }

    // M�todo para tocar m�sica de fundo
    public void PlayMusic(AudioClip musicClip)
    {
        if (musicClip != null)
        {
            musicSource.clip = musicClip;
            musicSource.loop = true;  // M�sica de fundo deve repetir
            musicSource.Play();
        }
    }

    // M�todo para tocar efeitos sonoros gen�ricos
    public void PlaySfx(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);  // Tocar o efeito sonoro uma vez
        }
    }

    // M�todo para ajustar o pitch da m�sica de fundo
    public void SetMusicPitch(float pitch)
    {
        if (musicSource != null)
        {
            musicSource.pitch = pitch;
        }
        else
        {
            Debug.LogError("SoundManager: musicSource is not assigned!");
        }
    }

    // M�todo para mutar todos os sons
    public void MuteAll()
    {
        isMuted = true;
        if (musicSource != null) musicSource.mute = true;
        if (sfxSource != null) sfxSource.mute = true;
    }

    // M�todo para desmutar todos os sons
    public void UnmuteAll()
    {
        isMuted = false;
        if (musicSource != null) musicSource.mute = false;
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
