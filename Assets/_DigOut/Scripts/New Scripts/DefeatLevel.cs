using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatLevel : MonoBehaviour
{
    private CoinManager coin;
    private Pause pause;

    //Necessário puxar o GameObject do Canvas para o EndLevel de forma manual
    public GameObject defeatScreen;
    public GameObject HUD;
    
    public float timeWithoutCoins = 0f;
    public float maxTimeWithoutCoin = 60f;
    public bool playerIsDefeated = false;
    // Start is called before the first frame update
    void Start()
    {
        coin = GameObject.Find("CoinManager").GetComponent<CoinManager>();
        pause = GameObject.Find("Canvas").GetComponent<Pause>();
        HUD = GameObject.Find("HUD");
    }

    // Update is called once per frame
    void Update()
    {
        VerifyDefeatConditions();
    }

    public void VerifyDefeatConditions()
    {
        if (coin.GetCoins() <= 0f && !playerIsDefeated)
        {
            timeWithoutCoins += Time.deltaTime;
            if (timeWithoutCoins >= maxTimeWithoutCoin)
            {
                ActivateDefeatScreen();
                pause.DeactivatePause();
                playerIsDefeated = true;
            }

            /*
            // Atualizar a transparência do fadeImage de acordo com o tempo sem moedas
            float transparency = Mathf.Clamp01(timeWithoutCoins / 60f);
            //FadeImageManager.Instance.SetTransparency(transparency);
            */
        }
        else if (coin.GetCoins() > 0f)
        {
            timeWithoutCoins = 0f;
            //FadeImageManager.Instance.SetTransparency(0f);
        }
    }

    public void ActivateDefeatScreen()
    {
        Time.timeScale = 0f;
        HUD.SetActive(false);
        defeatScreen.SetActive(true);

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
