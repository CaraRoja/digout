using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStage : MonoBehaviour
{
    public GameManager game;
    public ImageFadeHandler fadeImage;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        game = GameManager.Instance;
        fadeImage =  GameObject.Find("FadeImage").GetComponent<ImageFadeHandler>();
        //StartCoroutine(DelayRestartStage());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadStage()
    {
        if (game != null)
        {
            game.LoadScene(index);
        }
    }

    public void RestartStage()
    {
        if (game != null)
        {
            //game.RestartLevel();
            fadeImage.ExecuteFading(true);


        }
    }

    /*
    public IEnumerator DelayRestartStage()
    {
        yield return new WaitForSeconds(3f);
        RestartStage();
    }
    */
}
