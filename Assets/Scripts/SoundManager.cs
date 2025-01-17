using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource musicSource;  // Para música de fundo
    public AudioSource sfxSource;    // Para efeitos sonoros gerais

    [Header("Sound Effects")]
    public AudioClip backgroundMusic;  // Música de fundo

    private bool isMuted = false;

    void Awake()
    {
        // Implementação do padrão Singleton
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

    // Método para tocar música de fundo
    public void PlayMusic(AudioClip musicClip)
    {
        if (musicClip != null)
        {
            musicSource.clip = musicClip;
            musicSource.loop = true;  // Música de fundo deve repetir
            musicSource.Play();
        }
    }

    // Método para tocar efeitos sonoros genéricos
    public void PlaySfx(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);  // Tocar o efeito sonoro uma vez
        }
    }

    // Método para ajustar o pitch da música de fundo
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

    // Método para mutar todos os sons
    public void MuteAll()
    {
        isMuted = true;
        if (musicSource != null) musicSource.mute = true;
        if (sfxSource != null) sfxSource.mute = true;
    }

    // Método para desmutar todos os sons
    public void UnmuteAll()
    {
        isMuted = false;
        if (musicSource != null) musicSource.mute = false;
        if (sfxSource != null) sfxSource.mute = false;
    }

    // Método para alternar entre mutar e desmutar
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

    // Método para definir o estado do som
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
