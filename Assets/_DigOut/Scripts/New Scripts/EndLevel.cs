using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    //Necessário puxar o GameObject do Canvas para o EndLevel de forma manual
    public GameObject completeLevelScreen;
    public GameObject HUD;
    public Pause pause;
    public int indexNextLevel;
    // Start is called before the first frame update
    void Start()
    {
        //completeLevelScreen = GameObject.Find("LevelCompleteMenu");
        HUD = GameObject.Find("HUD");
        pause = GameObject.Find("Canvas").GetComponent<Pause>();
    }

    // Update is called once per frame
    void Update()
    {
        //ShowVictoryMenu();
    }

    public void PlayerReachedEnd()
    {
        //ShowVictoryMenu();
    }

    public void ShowVictoryMenu()
    {
        //Método responsável por desativar o pause para mostrar a tela de vitória
        pause.DeactivatePause();
        completeLevelScreen.SetActive(true);
        HUD.SetActive(false);
        Time.timeScale = 0f;
        //levelSelectionMenu.SetActive(true);
        
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void Continue()
    {
        SceneManager.LoadScene(indexNextLevel);
        Time.timeScale = 1f;
    }
}
