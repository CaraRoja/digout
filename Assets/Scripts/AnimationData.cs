using UnityEngine;

[System.Serializable] // Permite que o objeto seja editado no Inspector do Unity
public class AnimationData
{
    public Material material; // Material a ser usado na anima��o
    public Texture2D[] masks; // M�scaras para cada quadro da anima��o
}
