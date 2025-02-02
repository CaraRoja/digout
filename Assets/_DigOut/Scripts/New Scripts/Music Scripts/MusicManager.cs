using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    public AudioSource music;
    public List<AudioClip> musicClipList;

    private bool isMuted = false;

    private void Awake()
    {
        // Implementação do padrão Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Manter o SoundManager persistente entre cenas
            Debug.Log("MusicManager: Instance set in Awake");
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic (AudioClip clip)
    {
        music.clip = clip;
        music.Play();
    }

    // Método para ajustar o pitch da música de fundo
    public void SetMusicPitch(float pitch)
    {
        if (music != null)
        {
            music.pitch = pitch;
        }
        else
        {
            Debug.LogError("SoundManager: musicSource is not assigned!");
        }
    }

    public void MuteAll()
    {
        isMuted = true;
        
        if (music != null) 
            music.mute = true;
 
    }

    public void UnmuteAll()
    {
        isMuted = false;
        
        if (music != null) 
            music.mute = false;

    }

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

    public bool IsMuted()
    {
        return isMuted;
    }


}
