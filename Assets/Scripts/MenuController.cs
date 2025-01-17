using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject menuPrincipalPanel; // Painel do menu principal
    public GameObject configuracoesPanel; // Painel de configurações

    public void IniciarJogo()
    {
        SceneManager.LoadScene("Fase1"); // Substitua "Fase1" pelo nome da cena do seu jogo
    }

    public void AbrirConfiguracoes()
    {
        if (configuracoesPanel != null && menuPrincipalPanel != null)
        {
            configuracoesPanel.SetActive(true);
            menuPrincipalPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Painéis de menu principal ou configurações não estão atribuídos.");
        }
    }

    public void FecharConfiguracoes()
    {
        if (configuracoesPanel != null && menuPrincipalPanel != null)
        {
            configuracoesPanel.SetActive(false);
            menuPrincipalPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Painéis de menu principal ou configurações não estão atribuídos.");
        }
    }

    public void Sair()
    {
        Application.Quit();
    }
}
