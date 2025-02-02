using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSystem : MonoBehaviour
{
    public Slider HP;
    public CoinManager coin;

    private void Awake()
    {
        coin = GameObject.Find("CoinManager").GetComponent<CoinManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHP();
    }

    public void UpdateHP()
    {
        float value = coin.GetCoins();
        HP.value = value;
    }

}
