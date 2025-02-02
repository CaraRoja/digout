using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{

    public CoinManager coin;

    public Material groundMaterial;
    public Material skyMaterial;
    public Material grassMaterial;
    public Material platformMaterial;
    public SpriteRenderer groundRenderer;

    public Color groundColor;
    public Color skyColor;
    public Color grassColor;
    public Sprite groundSprite;

    private void Awake()
    {
        coin = GameObject.Find("CoinManager").GetComponent<CoinManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //UpdateMaterialColors(this.groundColor, this.skyColor, this.grassColor);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSaturation(coin.GetCoins());
    }

    public void UpdateMaterialColors(Color groundColor, Color skyColor, Color grassColor)
    {
        groundMaterial.color = groundColor;
        skyMaterial.color = skyColor;
        grassMaterial.color = grassColor;
    }

    public void UpdateSprites(Sprite groundSprite)
    {
        groundRenderer.sprite = groundSprite;
    }

    public void UpdateSaturation(float coins)
    {
        float saturation = (coins <= 100) ? 0.8f * (coins / 100f) : 0.8f + 0.2f * ((coins - 100) / 100f);
        groundMaterial.SetFloat("_Saturation", saturation);
        skyMaterial.SetFloat("_Saturation", saturation);
        grassMaterial.SetFloat("_Saturation", saturation);
        platformMaterial.SetFloat("_Saturation", saturation);

        // Atualiza o status de ter moedas ou não
        //SetHasCoins(coins > 0);
    }
}
