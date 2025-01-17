using UnityEngine;

[System.Serializable]
public class Level
{
    public Transform startPoint;
    public Transform endPoint;
    public Color groundColor;
    public Color skyColor;
    public Color grassColor;
    public Sprite groundSprite;
    public Sprite parallaxLayer1Sprite; // Adicionando referência à primeira camada de parallax
    public Sprite parallaxLayer2Sprite; // Adicionando referência à segunda camada de parallax
    public AudioClip levelMusic; // Adicionando a referência ao AudioClip para a música do nível
}
