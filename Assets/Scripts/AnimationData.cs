using UnityEngine;

[System.Serializable] // Permite que o objeto seja editado no Inspector do Unity
public class AnimationData
{
    public Material material; // Material a ser usado na animação
    public Texture2D[] masks; // Máscaras para cada quadro da animação
}
