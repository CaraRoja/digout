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
    public Sprite parallaxLayer1Sprite; // Adicionando refer�ncia � primeira camada de parallax
    public Sprite parallaxLayer2Sprite; // Adicionando refer�ncia � segunda camada de parallax
    public AudioClip levelMusic; // Adicionando a refer�ncia ao AudioClip para a m�sica do n�vel
}
