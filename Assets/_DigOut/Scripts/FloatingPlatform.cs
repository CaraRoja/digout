using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    public float amplitude = 0.5f; // Altura do movimento para cima e para baixo
    public float frequency = 1f; // Velocidade do movimento

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Calcula a nova posição Y usando uma função senoidal
        float newY = startPos.y + amplitude * Mathf.Sin(Time.time * frequency);
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}