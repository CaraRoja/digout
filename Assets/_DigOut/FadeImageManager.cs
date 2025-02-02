using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeImageManager : MonoBehaviour
{
    //public static FadeImageManager Instance { get; private set; }
    //public Image fadeImage;
    //public float fadeDuration = 1.0f;

    //void Awake()
    //{
    //    //if (Instance == null)
    //    //{
    //    //    Instance = this;
    //    //    DontDestroyOnLoad(gameObject);

    //    //    if (fadeImage == null)
    //    //    {
    //    //        fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();
    //    //    }

    //    //    SceneManager.sceneLoaded += OnSceneLoaded; // Register callback for scene load
    //    //}
    //    //else
    //    //{
    //    //    Destroy(gameObject);
    //    //}
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    if (scene.name == "Menu")
    //    {
    //        //StartMenuFadeIn();
    //    }
    //    else if (scene.name == "Fase1")
    //    {
    //        StartFase1FadeIn();
    //    }
    //}

    ////public void StartMenuFadeIn()
    ////{
    ////    if (fadeImage != null)
    ////    {
    ////        fadeImage.gameObject.SetActive(true);
    ////        StopAllCoroutines();
    ////        StartCoroutine(MenuFadeIn());
    ////    }
    ////}

    ////public void StartMenuFadeOut(string sceneName)
    ////{
    ////    if (fadeImage != null)
    ////    {
    ////        fadeImage.gameObject.SetActive(true);
    ////        StopAllCoroutines();
    ////        StartCoroutine(MenuFadeOut(sceneName));
    ////    }
    ////}

    //public void StartFase1FadeIn()
    //{
    //    if (fadeImage != null)
    //    {
    //        fadeImage.gameObject.SetActive(true);
    //        StopAllCoroutines();
    //        StartCoroutine(Fase1FadeIn());
    //    }
    //}

    //public void StartFase1FadeOut()
    //{
    //    if (fadeImage != null)
    //    {
    //        fadeImage.gameObject.SetActive(true);
    //        StopAllCoroutines();
    //        StartCoroutine(Fase1FadeOut());
    //    }
    //}

    //public void SetTransparency(float transparency)
    //{
    //    if (fadeImage != null)
    //    {
    //        Color color = fadeImage.color;
    //        color.a = transparency;
    //        fadeImage.color = color;
    //    }
    //}

    //private IEnumerator MenuFadeIn()
    //{
    //    if (fadeImage == null)
    //    {
    //        yield break;
    //    }

    //    float elapsedTime = 0f;
    //    Color color = fadeImage.color;
    //    color.a = 1f;
    //    fadeImage.color = color;


    //    while (elapsedTime < fadeDuration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        color.a = Mathf.Clamp01(1.0f - (elapsedTime / fadeDuration));
    //        fadeImage.color = color;
    //        yield return null;
    //    }

    //    color.a = 0f;
    //    fadeImage.color = color;
    //    fadeImage.gameObject.SetActive(false);
    //}

    //private IEnumerator MenuFadeOut(string sceneName)
    //{
    //    if (fadeImage == null)
    //    {
    //        yield break;
    //    }

    //    float elapsedTime = 0f;
    //    Color color = fadeImage.color;
    //    color.a = 0f;
    //    fadeImage.color = color;

    //    Debug.Log("Starting Menu Fade Out");

    //    while (elapsedTime < fadeDuration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
    //        fadeImage.color = color;
    //        yield return null;
    //    }

    //    color.a = 1f;
    //    fadeImage.color = color;
    //    Debug.Log("Menu Fade Out Complete");

    //    if (SoundManager.Instance != null && SoundManager.Instance.musicSource != null)
    //    {
    //        SoundManager.Instance.musicSource.Stop();
    //    }

    //    SceneManager.LoadScene(sceneName);
    //}

    //private IEnumerator Fase1FadeIn()
    //{
    //    if (fadeImage == null)
    //    {
    //        yield break;
    //    }

    //    float elapsedTime = 0f;
    //    Color color = fadeImage.color;
    //    color.a = 1f;
    //    fadeImage.color = color;

    //    Debug.Log("Starting Fase1 Fade In");

    //    while (elapsedTime < fadeDuration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        color.a = Mathf.Clamp01(1.0f - (elapsedTime / fadeDuration));
    //        fadeImage.color = color;

            
    //    }

    //    color.a = 0f;
    //    fadeImage.color = color;
    //    fadeImage.gameObject.SetActive(false);

    //}

    //private IEnumerator Fase1FadeOut()
    //{
    //    if (fadeImage == null)
    //    {
    //        yield break;
    //    }

    //    float elapsedTime = 0f;
    //    Color color = fadeImage.color;
    //    color.a = 0f;
    //    fadeImage.color = color;


    //    while (elapsedTime < fadeDuration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
    //        fadeImage.color = color;
    //        yield return null;
    //    }

    //    color.a = 1f;
    //    fadeImage.color = color;
    //    Debug.Log("Fase1 Fade Out Complete");

    //    if (SoundManager.Instance != null && SoundManager.Instance.musicSource != null)
    //    {
    //        SoundManager.Instance.musicSource.Stop();
    //    }

    //    SceneManager.LoadScene("Menu");
    //}
}
