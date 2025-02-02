using UnityEngine;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    public string key;

    private TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        if (text != null && LocalizationManager.instance != null)
        {
            LocalizationManager.instance.OnLanguageChanged += UpdateText;
            UpdateText();
        }
    }

    void OnDestroy()
    {
        if (LocalizationManager.instance != null)
        {
            LocalizationManager.instance.OnLanguageChanged -= UpdateText;
        }
    }

    public void UpdateText()
    {
        if (text == null)
        {
            text = GetComponent<TMP_Text>();
        }

        if (text != null && LocalizationManager.instance != null)
        {
            text.text = LocalizationManager.instance.GetLocalizedValue(key);
        }
    }
}
