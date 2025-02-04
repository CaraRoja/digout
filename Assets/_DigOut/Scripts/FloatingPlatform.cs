using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    public enum MovementType
    {
        Parada,
        Horizontal,
        Vertical
    }

    [Header("Tipo de movimento da plataforma")]
    public MovementType movementType = MovementType.Parada;

    [Header("Amplitude/Frequ�ncia para Horizontal/Vertical")]
    public float amplitudeHV = 3f;
    public float frequencyHV = 1f;

    private Vector3 startPos;
    private Vector3 previousPosition;

    // Lista de jogadores que est�o em contato com a plataforma
    private List<Transform> playersOnPlatform = new List<Transform>();

    void Start()
    {
        startPos = transform.position;
        previousPosition = transform.position;
    }

    void Update()
    {
        // Movimenta a plataforma de acordo com o tipo selecionado
        switch (movementType)
        {
            case MovementType.Parada:
                MoverParada();
                break;
            case MovementType.Horizontal:
                MoverHorizontal();
                break;
            case MovementType.Vertical:
                MoverVertical();
                break;
        }

        // Calcula o deslocamento (delta) da plataforma
        Vector3 delta = transform.position - previousPosition;
        previousPosition = transform.position;

        // Aplica o mesmo deslocamento para cada jogador que est� na plataforma
        foreach (Transform player in playersOnPlatform)
        {
            player.position += delta;
        }
    }

    /// <summary>
    /// Movimento "parado", mas com oscila��o no eixo Y (amplitude 0.5 e frequ�ncia 1).
    /// </summary>
    private void MoverParada()
    {
        float newY = startPos.y + 0.5f * Mathf.Sin(Time.time * 1f);
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    /// <summary>
    /// Movimento na horizontal (eixo X), usando amplitudeHV e frequencyHV.
    /// </summary>
    private void MoverHorizontal()
    {
        float newX = startPos.x + amplitudeHV * Mathf.Sin(Time.time * frequencyHV);
        transform.position = new Vector3(newX, startPos.y, startPos.z);
    }

    /// <summary>
    /// Movimento na vertical (eixo Y), usando amplitudeHV e frequencyHV.
    /// </summary>
    private void MoverVertical()
    {
        float newY = startPos.y + amplitudeHV * Mathf.Sin(Time.time * frequencyHV);
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    // Usando os m�todos de colis�o 2D para detectar o jogador
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (movementType == MovementType.Horizontal) {

            if (collision.gameObject.CompareTag("Player"))
            {
                // Adiciona o jogador � lista
                if (!playersOnPlatform.Contains(collision.transform))
                {
                    playersOnPlatform.Add(collision.transform);
                }
            }

        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (movementType == MovementType.Horizontal) {

            if (collision.gameObject.CompareTag("Player"))
            {
                // Remove o jogador da lista
                if (playersOnPlatform.Contains(collision.transform))
                {
                    playersOnPlatform.Remove(collision.transform);
                }
            }

        }
        
    }
}
