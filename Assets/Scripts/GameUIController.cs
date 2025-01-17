using UnityEngine;
using TMPro;  // Importe o namespace para usar TextMeshPro
using UnityEngine.UI;  // Importe o namespace para usar UI elements

public class GameUIController : MonoBehaviour
{
    // Refer�ncia para o componente PlayerManager no jogador
    public PlayerManager playerManager;

    // Refer�ncias para os elementos TextMeshPro UI no editor
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI jumpPowerText;

    // Refer�ncias para a barra de �nimo e o indicador
    public Slider hudSlider;
    public RectTransform indicator;
    public float maxValue = 200f; // Valor m�ximo para a barra de �nimo

    void Start()
    {
        // Inicializa a barra de �nimo
        hudSlider.minValue = 0;
        hudSlider.maxValue = maxValue;
        hudSlider.value = maxValue / 2; // Come�a no meio
    }

    void Update()
    {
        // Garante que playerManager foi atribu�do
        if (playerManager != null)
        {
            // Atualiza os textos UI com as informa��es atuais vindas do PlayerManager
            coinText.text = "Coins: " + playerManager.coins.ToString();
            speedText.text = "Speed: " + playerManager.speed.ToString("F2");
            jumpPowerText.text = "Jump Power: " + playerManager.jumpForce.ToString("F2");

            // Atualiza o valor da barra de �nimo com base no valor do playerManager
            UpdateHUDValue(playerManager.coins);
        }
        else
        {
            Debug.LogWarning("PlayerManager n�o est� atribu�do ao GameUIController!");
        }
    }

    // Fun��o para atualizar o valor da HUD (�nimo)
    public void UpdateHUDValue(float newValue)
    {
        // Clampa o valor para o valor m�ximo
        float clampedValue = Mathf.Clamp(newValue, 0, maxValue);

        // Atualiza o valor do slider
        hudSlider.value = clampedValue;

        // Atualiza a posi��o do indicador
        float normalizedValue = clampedValue / maxValue;
        Vector2 indicatorPosition = new Vector2(normalizedValue * hudSlider.GetComponent<RectTransform>().sizeDelta.x, 0);
        indicator.anchoredPosition = indicatorPosition;
    }
}
