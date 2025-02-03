using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonHandler : MonoBehaviour
{
    public MenuSoundController SoundManager;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager = GameObject.Find("SoundEffects").GetComponent<MenuSoundController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClickSound()
    {
        SoundManager.PlayClickSound();
    }


    //OBS: Mudar após inserir várias fases, para colocar condições de fazer loading das fases e etc
    public void OpenLevel()
    {
        StartCoroutine(DelayOpenScene());
    }

    public IEnumerator DelayOpenScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }
}
