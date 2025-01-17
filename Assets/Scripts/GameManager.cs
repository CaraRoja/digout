using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int currentSaveSlot = 0; // Slot atual do savegame

    public GameObject pauseMenu;
    public GameObject defeatMenu;
    public GameObject completionMenu;
    public GameObject levelSelectionMenu;  // Novo menu de seleção de nível
    public GameObject settingsMenu;  // Novo menu de configurações
    public GameObject gameHUD;  // HUD do jogo

    public Material groundMaterial;
    public Material skyMaterial;
    public Material grassMaterial;
    public Material platformMaterial;
    public SpriteRenderer groundRenderer;

    private SaveData currentSaveData;
    private float timeWithoutCoins = 0f;
    private bool hasCoins = true; // Variável para verificar se o jogador tem moedas

    private AudioLowPassFilter lowPassFilter;
    private float originalVolume;

    // Configurações de idioma e som
    public string currentLanguage = "en";
    public bool isSoundOn = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            currentSaveData = SaveData.Load(currentSaveSlot);

            // Inicializa o filtro de áudio e verifica se o SoundManager está configurado corretamente
            if (SoundManager.Instance != null && SoundManager.Instance.musicSource != null)
            {
                if (SoundManager.Instance.musicSource.GetComponent<AudioLowPassFilter>() == null)
                {
                    lowPassFilter = SoundManager.Instance.musicSource.gameObject.AddComponent<AudioLowPassFilter>();
                }
                else
                {
                    lowPassFilter = SoundManager.Instance.musicSource.GetComponent<AudioLowPassFilter>();
                }
                lowPassFilter.enabled = false;
                originalVolume = SoundManager.Instance.musicSource.volume;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Verifica se o LocalizationManager está configurado corretamente
        if (LocalizationManager.instance != null)
        {
            LocalizationManager.instance.LoadLocalizedText(currentLanguage + ".csv");
        }

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetSoundState(isSoundOn);
        }

        if (SceneManager.GetActiveScene().name == "Menu")
        {
            FadeImageManager.Instance.StartMenuFadeIn();
        }
        else if (SceneManager.GetActiveScene().name == "Fase1")
        {
            FadeImageManager.Instance.StartFase1FadeIn();
        }
    }

    void Update()
    {
        if (!hasCoins)
        {
            timeWithoutCoins += Time.deltaTime;
            if (timeWithoutCoins >= 60f)
            {
                TriggerDefeatDueToNoCoins();
            }

            // Atualizar a transparência do fadeImage de acordo com o tempo sem moedas
            float transparency = Mathf.Clamp01(timeWithoutCoins / 60f);
            FadeImageManager.Instance.SetTransparency(transparency);
        }
        else
        {
            timeWithoutCoins = 0f;
            FadeImageManager.Instance.SetTransparency(0f);
        }
    }

    public void SetHasCoins(bool value)
    {
        hasCoins = value;
        if (!hasCoins)
        {
            timeWithoutCoins = 0f;
        }
    }

    private void TriggerDefeatDueToNoCoins()
    {
        // Função chamada quando o jogador fica 1 minuto sem moedas
        DefeatDueToNoCoinsEffect();
        PlayerFell();
    }

    private void DefeatDueToNoCoinsEffect()
    {
        // Coloque aqui o código do efeito visual que deseja ao perder por ficar sem moedas
    }

    public void EndLevel(float completionTime, int attempts, int score, int items, int enemies, string path)
    {
        currentSaveData.AddLevelData(completionTime, attempts, score, items, enemies, path);
        currentSaveData.Save(currentSaveSlot);
        ShowVictoryMenu();
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        gameHUD.SetActive(false);
        Time.timeScale = 0;

        // Abaixa e abafa a música
        if (lowPassFilter != null)
        {
            lowPassFilter.enabled = true;
            lowPassFilter.cutoffFrequency = 500; // Frequência de corte para o filtro passa-baixas
        }

        if (SoundManager.Instance != null && SoundManager.Instance.musicSource != null)
        {
            SoundManager.Instance.musicSource.volume = originalVolume * 0.5f; // Abaixa o volume
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        gameHUD.SetActive(true);
        Time.timeScale = 1;

        // Restaura a música ao normal
        if (lowPassFilter != null)
        {
            lowPassFilter.enabled = false;
        }

        if (SoundManager.Instance != null && SoundManager.Instance.musicSource != null)
        {
            SoundManager.Instance.musicSource.volume = originalVolume; // Restaura o volume
        }
    }

    public void PlayerFell()
    {
        defeatMenu.SetActive(true);
        gameHUD.SetActive(false);
        Time.timeScale = 0;

        // Desativar o fadeImage ao mostrar a tela de derrota
        FadeImageManager.Instance.SetTransparency(0f);
    }

    public void RestartLevel()
    {
        FadeImageManager.Instance.StartMenuFadeOut("Fase1");
        Time.timeScale = 1;
        gameHUD.SetActive(true);
        defeatMenu.SetActive(false);
        pauseMenu.SetActive(false);
        completionMenu.SetActive(false);
        levelSelectionMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void PlayerReachedEnd()
    {
        ShowVictoryMenu();
    }

    private void ShowVictoryMenu()
    {
        completionMenu.SetActive(true);
        gameHUD.SetActive(false);
        Time.timeScale = 0;
        levelSelectionMenu.SetActive(true);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        FadeImageManager.Instance.StartMenuFadeOut("Menu");
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void UpdateMaterialColors(Color groundColor, Color skyColor, Color grassColor)
    {
        groundMaterial.color = groundColor;
        skyMaterial.color = skyColor;
        grassMaterial.color = grassColor;
    }

    public void UpdateSprites(Sprite groundSprite)
    {
        groundRenderer.sprite = groundSprite;
    }

    public void UpdateSaturation(int coins)
    {
        float saturation = (coins <= 100) ? 0.8f * (coins / 100f) : 0.8f + 0.2f * ((coins - 100) / 100f);
        groundMaterial.SetFloat("_Saturation", saturation);
        skyMaterial.SetFloat("_Saturation", saturation);
        grassMaterial.SetFloat("_Saturation", saturation);
        platformMaterial.SetFloat("_Saturation", saturation);

        // Atualiza o status de ter moedas ou não
        SetHasCoins(coins > 0);
    }

    // Adicionando a função para fechar o jogo
    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetLanguage(string language)
    {
        currentLanguage = language;
        LocalizationManager.instance.LoadLocalizedText(language + ".csv");
    }

    public void SetSoundState(bool isOn)
    {
        isSoundOn = isOn;
        if (isOn)
        {
            SoundManager.Instance.UnmuteAll();
        }
        else
        {
            SoundManager.Instance.MuteAll();
        }
    }

    public void LoadScene(string sceneName)
    {
        FadeImageManager.Instance.StartMenuFadeOut(sceneName);
    }
}
