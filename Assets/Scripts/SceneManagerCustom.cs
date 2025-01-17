using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerCustom : MonoBehaviour
{
    // Singleton para acesso f�cil ao SceneManagerCustom em todo o jogo
    public static SceneManagerCustom Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mant�m o objeto vivo entre as cenas
        }
        else
        {
            Destroy(gameObject); // Destroi duplicatas
        }
    }

    // Carrega uma cena pelo nome
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Carrega uma cena pelo �ndice
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Reinicia a cena atual
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Carrega a pr�xima cena baseada no �ndice atual
    public void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    // Retorna � cena inicial (geralmente a cena do menu principal)
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Sair do jogo
    public void QuitGame()
    {
        Application.Quit();
    }
}
