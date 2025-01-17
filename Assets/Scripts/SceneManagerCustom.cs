using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerCustom : MonoBehaviour
{
    // Singleton para acesso fácil ao SceneManagerCustom em todo o jogo
    public static SceneManagerCustom Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantém o objeto vivo entre as cenas
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

    // Carrega uma cena pelo índice
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Reinicia a cena atual
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Carrega a próxima cena baseada no índice atual
    public void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    // Retorna à cena inicial (geralmente a cena do menu principal)
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
