using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject HUD;
    public GameObject PauseMenuPanel;
    public GameObject ConfigPanel;
    public enum PausePanels {Pause, Config};
    public PausePanels panels = PausePanels.Pause;

    public bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
        ChangePanels();
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GetEnumPanel().Equals(PausePanels.Config))
            {
                isPaused = true;
                Time.timeScale = 0f;
                SetEnumPanel(PausePanels.Pause);
            }
            else if (GetEnumPanel().Equals(PausePanels.Config))
            {
                SetEnumPanel(PausePanels.Config);
            }
            
        }
    }

    public void ResumeGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GetEnumPanel().Equals(PausePanels.Pause))
        {
            isPaused = false;
            Time.timeScale = 1f;
        }
    }

    //Muda os painéis da tela de pause
    public void ChangePanels()
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
