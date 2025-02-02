using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LanguageSelector : MonoBehaviour
{
    // Lista de idiomas e seus c�digos
    private List<string> languages = new List<string> { "en", "pt" };
    private int currentLanguageIndex = 0;

    public TMP_Text buttonText;

    void Start()
    {
        // Carrega a configura��o do idioma
        if (GameManager.Instance != null)
        {
            currentLanguageIndex = languages.IndexOf(GameManager.Instance.currentLanguage);
            if (currentLanguageIndex == -1) currentLanguageIndex = 0;  // Se n�o encontrado, use o primeiro idioma
        }

        // Inicializa o idioma
        if (LocalizationManager.instance != null)
        {
            UpdateLanguage();
        }
        else
        {
            Debug.LogError("LocalizationManager instance is not set.");
        }
    }

    public void ToggleLanguage()
    {
        // Alterna para o pr�ximo idioma na lista
        if (LocalizationManager.instance != null)
        {
            currentLanguageIndex = (currentLanguageIndex + 1) % languages.Count;
            UpdateLanguage();

            // Salva a configura��o do idioma
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SetLanguage(languages[currentLanguageIndex]);
            }
        }
        else
        {
            Debug.LogError("LocalizationManager instance is not set.");
        }
    }

    private void UpdateLanguage()
    {
        string languageCode = languages[currentLanguageIndex];
        LocalizationManager.instance.LoadLocalizedText(languageCode + ".csv");

        // Atualiza todos os textos localizados na cena
        foreach (LocalizedText localizedText in FindObjectsOfType<LocalizedText>())
        {
            localizedText.UpdateText();
        }

        // Atualiza o texto do bot�o para mostrar o idioma atual
        string languageName = GetLanguageName(languageCode);
        buttonText.text = languageName;
    }

    private string GetLanguageName(string languageCode)
    {
        // Retorna o nome do idioma baseado no c�digo
        switch (languageCode)
        {
            case "en":
                return "English";
            case "pt":
                return "Portuguese";
            default:
                return "Unknown";
        }
    }
}
