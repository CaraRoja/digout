using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance;
    private Dictionary<string, string> localizedText;
    private bool isReady = false;
    private string missingTextString = "Localized text not found";

    // Evento para notificar a mudança de idioma
    public event Action OnLanguageChanged;

    // Definir o idioma padrão
    public string defaultLanguage = "pt";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Carregar o idioma padrão
            LoadLocalizedText(defaultLanguage + ".csv");
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadLocalizedText(string fileName)
    {
        localizedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string[] keyValue = line.Split(',');
                    if (keyValue.Length == 2)
                    {
                        localizedText.Add(keyValue[0], keyValue[1]);
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Cannot find file: " + filePath);
        }
        isReady = true;

        // Notificar a mudança de idioma
        OnLanguageChanged?.Invoke();
    }

    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;
        if (localizedText != null && localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }
        return result;
    }

    public bool GetIsReady()
    {
        return isReady;
    }
}
