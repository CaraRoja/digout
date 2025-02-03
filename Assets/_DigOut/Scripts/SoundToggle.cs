using UnityEngine;
using TMPro;

public class SoundToggle : MonoBehaviour
{
    public TMP_Text buttonText;

    private bool isSoundOn = true;

    void Start()
    {
        // Carrega a configura��o do som
        if (GameManager.Instance != null)
        {
            isSoundOn = GameManager.Instance.isSoundOn;
        }

        UpdateSoundStatus();

        // Inscrever-se no evento de mudan�a de idioma
        if (LocalizationManager.instance != null)
        {
            LocalizationManager.instance.OnLanguageChanged += UpdateSoundStatus;
        }
    }

    void OnDestroy()
    {
        // Cancelar a inscri��o no evento de mudan�a de idioma
        if (LocalizationManager.instance != null)
        {
            LocalizationManager.instance.OnLanguageChanged -= UpdateSoundStatus;
        }
    }

    
    public void ToggleSound()
    {
        if (SoundManager.Instance != null)
        {
            isSoundOn = !isSoundOn;

            if (isSoundOn)
            {
                SoundManager.Instance.UnmuteAll();
            }
            else
            {
                SoundManager.Instance.MuteAll();
            }

            /*
            // Salva a configura��o do som
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SetSoundState(isSoundOn);
            }
            */

            UpdateSoundStatus();
        }
        else
        {
            Debug.LogError("SoundManager instance is not set.");
        }

        if (MusicManager.Instance != null)
        {
            isSoundOn = !isSoundOn;

            if (isSoundOn)
            {
                MusicManager.Instance.UnmuteAll();
            }
            else
            {
                MusicManager.Instance.MuteAll();
            }

            /*
            // Salva a configura��o do som
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SetSoundState(isSoundOn);
            }
            */

            UpdateSoundStatus();
        }
        else
        {
            Debug.LogError("MusicManager instance is not set.");
        }
    }
    

    private void UpdateSoundStatus()
    {
        string soundStatusKey = isSoundOn ? "SOUND_ON" : "SOUND_OFF";
        if (LocalizationManager.instance != null)
        {
            buttonText.text = LocalizationManager.instance.GetLocalizedValue(soundStatusKey);
        }
        else
        {
            Debug.LogError("LocalizationManager instance is not set.");
            buttonText.text = soundStatusKey;  // Default to the key if localization is not available
        }
    }
}
