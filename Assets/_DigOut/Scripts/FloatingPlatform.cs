using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    public enum MovementType
    {
        Parada,     // Faz o movimento senoidal no eixo Y com amplitude 0.5f e freq 1f (fixos)
        Horizontal, // Movimento senoidal no eixo X
        Vertical    // Movimento senoidal no eixo Y
    }

    [Header("Tipo de movimento da plataforma")]
    public MovementType movementType = MovementType.Parada;

    [Header("Amplitude/Frequência para Horizontal/Vertical")]
    [Tooltip("Amplitude usada quando o tipo for Horizontal ou Vertical.")]
    public float amplitudeHV = 3f;

    [Tooltip("Frequência usada quando o tipo for Horizontal ou Vertical.")]
    public float frequencyHV = 1f;

    // Posição inicial do objeto
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
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
    }

    /// <summary>
    /// Movimento "parado", mas na verdade oscila no eixo Y com amplitude de 0.5f e freq 1f.
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
}
