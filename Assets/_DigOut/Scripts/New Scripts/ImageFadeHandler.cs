using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFadeHandler : MonoBehaviour
{
    private GameManager game;
    public Image image;
    public Color color;
    public float alphaValue;
    public float fadeSpeed;
    public bool isFading;

    private void Awake()
    {
        game = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        color = image.color;
        alphaValue = color.a;


    }

    // Update is called once per frame
    void Update()
    {
        FadeOut();
        FadeIn();

    }

    public void ExecuteFading(bool flag)
    {
        isFading = flag;
    }

    public void FadeIn()
    {
        if (isFading)
        {
            if (alphaValue >= 0f && alphaValue != 1f)
            {
                
                    alphaValue = Mathf.MoveTowards(alphaValue, 1f, fadeSpeed * Time.deltaTime);
                    color.a = alphaValue;
                    image.color = color;

            }
        }  
    }

    public void FadeOut()
    {
        if (!isFading)
        {

            if (alphaValue <= 1f && alphaValue != 0f)
            {
                
                    alphaValue = Mathf.MoveTowards(alphaValue, 0f, fadeSpeed * Time.deltaTime);
                    color.a = alphaValue;
                    image.color = color;

            }                
        }
    }
}
