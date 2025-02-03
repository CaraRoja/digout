using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject HUD;
    public GameObject PauseMenuPanel;
    public GameObject ConfigPanel;
    public enum PausePanels {Pause, Config};
    public PausePanels panels = PausePanels.Pause;

    public bool isPaused = false;
    public bool canPause = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
        UpdatePanels();
    }

    public void PauseGame()
    {
        if (canPause)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !IsPaused())
            {
                isPaused = true;
                Time.timeScale = 0f;
                SetEnumPanel(PausePanels.Pause);

            }
            else if (Input.GetKeyDown(KeyCode.Escape) && IsPaused() && GetEnumPanel().Equals(PausePanels.Config))
            {

                SetEnumPanel(PausePanels.Pause);

            }
            else if (Input.GetKeyDown(KeyCode.Escape) && IsPaused() && GetEnumPanel().Equals(PausePanels.Pause))
            {
                isPaused = false;
                Time.timeScale = 1f;

            }
        }
        
    }

    public void DeactivatePause()
    {
        canPause = false;
    }

    public void ResumeGameByButton()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void RestartGameByButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void ReturnToMenuByButton()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void OpenConfigPanel()
    {
        SetEnumPanel(PausePanels.Config);
    }

    //Muda os painéis da tela de pause
    public void UpdatePanels()
    {
        if (IsPaused())
        {
            HUD.SetActive(false);
            if (panels.Equals(PausePanels.Pause))
            { 
                PauseMenuPanel.SetActive(true);
                ConfigPanel.SetActive(false);
            }
            else if (panels.Equals(PausePanels.Config))
            {
                PauseMenuPanel.SetActive(false);
                ConfigPanel.SetActive(true);
            }
        }
        else
        {
            HUD.SetActive(true);
            PauseMenuPanel.SetActive(false);
            ConfigPanel.SetActive(false);
        }
    }

    public void SetEnumPanel(PausePanels panel)
    {
        switch (panel)
        {
            case PausePanels.Pause:
                panels = PausePanels.Pause;
                break;
            case PausePanels.Config:
                panels = PausePanels.Config;
                break;
            default:
                break;
        }
    }

    //Recupera qual o Enum do painel está selecionado no momento
    public PausePanels GetEnumPanel()
    {
        return panels;
    }

    public bool IsPaused()
    {
        return isPaused;
    }



}
