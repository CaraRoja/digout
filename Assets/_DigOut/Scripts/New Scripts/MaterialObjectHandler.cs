using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialObjectHandler : MonoBehaviour
{

    public Material material;
    public float fadeSpeed = 1f;
    public float fadeValue;
    public bool isWorking = true;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFadeToZero();
        DestroyObject();
        IncreaseFadeValue();
    }

    public void UpdateFadeToZero()
    {
        if (isWorking)
        {
            if (fadeValue > -0.1f)
            {
                fadeValue = Mathf.MoveTowards(fadeValue, 0f, fadeSpeed *0.5f * Time.deltaTime);
                material.SetFloat("_FadeAmount",fadeValue);
            }
        }

    }

    public void DestroyObject()
    {
        if (fadeValue >= 0.6f)
        {
            Destroy(this.gameObject);
        }
    }

    public void IncreaseFadeValue()
    {
        if (!isWorking)
        {
            fadeValue = Mathf.MoveTowards(fadeValue, 0.7f, fadeSpeed * 2f * Time.deltaTime);
            material.SetFloat("_FadeAmount", fadeValue);
        }
    }

    public bool IsWorking()
    {
        return isWorking;
    }

    public void SetWorkingStatus(bool flag)
    {
        isWorking = flag;
    }
}
