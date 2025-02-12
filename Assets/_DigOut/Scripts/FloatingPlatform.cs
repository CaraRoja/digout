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

    [Header("Amplitude/Frequência para Horizontal/Vertical")]
    public float amplitudeHV = 3f;
    public float frequencyHV = 1f;

    // Valores fixos para o movimento 'Parada'
    private const float paradaAmplitude = 0.5f;
    private const float paradaFrequency = 1f;

    private Vector3 startPos;
    private Vector3 previousPosition;
    private Meditation meditation;

    private readonly List<Transform> playersOnPlatform = new List<Transform>();

    private void Start()
    {
        startPos = transform.position;
        previousPosition = transform.position;
        meditation = FindObjectOfType<Meditation>();

        if (meditation == null)
        {
            Debug.LogError("Meditation não carregado!");
        }
    }

    private void FixedUpdate()
    {
        // Atualiza o movimento da plataforma conforme o tipo selecionado
        MovePlatform();

        // Calcula o deslocamento (delta) da plataforma
        Vector3 delta = transform.position - previousPosition;
        previousPosition = transform.position;

        // Aplica o deslocamento para cada jogador que não esteja como filho (para evitar dupla movimentação)
        foreach (Transform player in playersOnPlatform)
        {
            if (player.parent != transform)
            {
                player.position += delta;
            }
        }
    }

    /// <summary>
    /// Seleciona o movimento conforme o tipo definido.
    /// </summary>
    private void MovePlatform()
    {
        switch (movementType)
        {
            case MovementType.Parada:
                MoveParada();
                break;
            case MovementType.Horizontal:
                MoveHorizontal();
                break;
            case MovementType.Vertical:
                MoveVertical();
                break;
        }
    }

    private void MoveParada()
    {
        float newY = startPos.y + paradaAmplitude * Mathf.Sin(Time.time * paradaFrequency);
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    private void MoveHorizontal()
    {
        float newX = startPos.x + amplitudeHV * Mathf.Sin(Time.time * frequencyHV);
        transform.position = new Vector3(newX, startPos.y, startPos.z);
    }

    private void MoveVertical()
    {
        float newY = startPos.y + amplitudeHV * Mathf.Sin(Time.time * frequencyHV);
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        Transform playerTransform = collision.transform;
        if (!playersOnPlatform.Contains(playerTransform))
        {
            playersOnPlatform.Add(playerTransform);
        }

        // Se o jogador estiver meditando, define-o como filho da plataforma
        if (meditation != null && meditation.PlayerIsMeditating())
        {
            SetAsChild(playerTransform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        Transform playerTransform = collision.transform;
        if (playersOnPlatform.Contains(playerTransform))
        {
            playersOnPlatform.Remove(playerTransform);
        }

        // Se o jogador estava como filho da plataforma, remove essa relação
        if (playerTransform.parent == transform)
        {
            RemoveAsChild(playerTransform);
        }
    }

    /// <summary>
    /// Define o jogador como filho da plataforma, mantendo sua posição e escala globais.
    /// </summary>
    private void SetAsChild(Transform player)
    {
        Vector3 originalScale = player.lossyScale;
        player.SetParent(transform, true); // true: mantém a posição global
        player.localScale = transform.InverseTransformVector(originalScale); // Restaura a escala global
        Debug.Log("Jogador está meditando e agora é filho da plataforma.");
    }

    /// <summary>
    /// Remove a relação de pai do jogador, restaurando sua escala global.
    /// </summary>
    private void RemoveAsChild(Transform player)
    {
        Vector3 originalScale = player.lossyScale;
        player.SetParent(null);
        player.localScale = originalScale;
        Debug.Log("Jogador parou de meditar e não é mais filho da plataforma.");
    }
}
