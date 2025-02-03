using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public Level[] levels;
    private int currentLevelIndex = 0;
    public GameObject player;
    public float completionDistance = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGame();  // Carrega o jogo ao iniciar
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // Se não houver informações de save, ativa o primeiro nível
        if (!PlayerPrefs.HasKey("SavedLevel"))
        {
            ActivateLevel(0);
        }
        else
        {
            int savedLevel = PlayerPrefs.GetInt("SavedLevel");
            ActivateLevel(savedLevel);
        }
    }

    void Update()
    {
        if (player != null && levels[currentLevelIndex].endPoint != null &&
            Vector3.Distance(player.transform.position, levels[currentLevelIndex].endPoint.position) < completionDistance)
        {
            //GameManager.Instance.PlayerReachedEnd();
            SaveGame(currentLevelIndex + 1);  // Salva o próximo nível como o nível atual
        }
    }

    public void ActivateLevel(int index)
    {
        if (index < 0 || index >= levels.Length) return;

        currentLevelIndex = index;
        Level currentLevel = levels[currentLevelIndex];
        if (player != null && currentLevel.startPoint != null)
        {
            player.transform.position = currentLevel.startPoint.position;
        }

        GameManager.Instance.UpdateMaterialColors(currentLevel.groundColor, currentLevel.skyColor, currentLevel.grassColor);
        GameManager.Instance.UpdateSprites(currentLevel.groundSprite);

        // Parar a música do menu e iniciar a música do nível
        //if (SoundManager.Instance != null)
        //{
        //    SoundManager.Instance.musicSource.Stop();
        //    if (currentLevel.levelMusic != null)
        //    {
        //        SoundManager.Instance.musicSource.clip = currentLevel.levelMusic;
        //        SoundManager.Instance.musicSource.Play();
        //    }
        //}

        // Atualizar parallax layers diretamente através dos scripts ParallaxEffect anexados aos objetos da cena
        UpdateParallaxSprites(currentLevel.parallaxLayer1Sprite, currentLevel.parallaxLayer2Sprite);

        
        // Iniciar a transição de fade in para o novo nível
        
        //FadeImageManager.Instance.StartFase1FadeIn();
    }

    private void SaveGame(int levelIndex)
    {
        PlayerPrefs.SetInt("SavedLevel", levelIndex);
        PlayerPrefs.Save();
    }

    private void LoadGame()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            int savedLevel = PlayerPrefs.GetInt("SavedLevel");
            ActivateLevel(savedLevel);
        }
    }

    public int GetCurrentLevelIndex()
    {
        return currentLevelIndex;
    }

    private void UpdateParallaxSprites(Sprite parallaxLayer1Sprite, Sprite parallaxLayer2Sprite)
    {
        GameObject parallaxLayer1 = GameObject.FindGameObjectWithTag("ParallaxLayer1");
        GameObject parallaxLayer2 = GameObject.FindGameObjectWithTag("ParallaxLayer2");

        if (parallaxLayer1 != null)
        {
            parallaxLayer1.GetComponent<SpriteRenderer>().sprite = parallaxLayer1Sprite;
        }

        if (parallaxLayer2 != null)
        {
            parallaxLayer2.GetComponent<SpriteRenderer>().sprite = parallaxLayer2Sprite;
        }
    }
}
