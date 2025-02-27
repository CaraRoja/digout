using UnityEngine;
using TMPro;  // Importe o namespace para usar TextMeshPro
using UnityEngine.UI;  // Importe o namespace para usar UI elements

public class GameUIController : MonoBehaviour
{
    // Referência para o componente PlayerManager no jogador
    public PlayerManager playerManager;

    // Referências para os elementos TextMeshPro UI no editor
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI jumpPowerText;

    // Referências para a barra de ânimo e o indicador
    public Slider hudSlider;
    public RectTransform indicator;
    public float maxValue = 200f; // Valor máximo para a barra de ânimo

    void Start()
    {
        // Inicializa a barra de ânimo
        /*
        hudSlider.minValue = 0;
        hudSlider.maxValue = maxValue;
        hudSlider.value = maxValue / 2; // Começa no meio
        */
    }

    void Update()
    {

    }

    // Função para atualizar o valor da HUD (ânimo)
    public void UpdateHUDValue(float newValue)
    {
        /*
        // Clampa o valor para o valor máximo
        float clampedValue = Mathf.Clamp(newValue, 0, maxValue);

        // Atualiza o valor do slider
        hudSlider.value = clampedValue;

        // Atualiza a posição do indicador
        float normalizedValue = clampedValue / maxValue;
        Vector2 indicatorPosition = new Vector2(normalizedValue * hudSlider.GetComponent<RectTransform>().sizeDelta.x, 0);
        indicator.anchoredPosition = indicatorPosition;
        */
    }
}
