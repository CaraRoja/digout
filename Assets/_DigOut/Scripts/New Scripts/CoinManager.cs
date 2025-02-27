using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public float maxCoins = 100f;
    public int coins;
    public float floatCoin = 100f;
    public float coinPickupValue = 5f;
    public float coinLoseTimeValue;
    public bool isCoinWorking = true;

    private DialogueManager dialogue;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        dialogue = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MaintainMaxCoin();
        LoseCoinsInTime(coinLoseTimeValue);
    }

    public void MaintainMaxCoin()
    {
        if (floatCoin >= maxCoins)
        {
            floatCoin = maxCoins;
            coins = Mathf.FloorToInt(floatCoin);
        }
    }

    //Atualiza o valor de perda gradual de coins pelo tempo
    public void SetCoinLoseTimeValue(float value)
    {
        coinLoseTimeValue = value;
    }

    public float CalculateLogarithmicInterpolation(float min, float max)
    {
        float baseValue = (min + max) / 2;

        if (coins <= 100)
        {
            float normalized = Mathf.Log10(1 + 9 * (coins / 100f));
            return Mathf.Lerp(min, baseValue, normalized / Mathf.Log10(10));
        }

        if (coins <= 200)
        {
            float normalized = Mathf.Log10(1 + 9 * ((coins - 100f) / 100f));
            return Mathf.Lerp(baseValue, max, normalized / Mathf.Log10(10));
        }

        return max;
    }

    public void AddCoins()
    {
        floatCoin += coinPickupValue;
        coins = Mathf.FloorToInt(floatCoin);

    }
    public void AddCoins(float value)
    {
        floatCoin += value;
        coins = Mathf.FloorToInt(floatCoin);

    }

    public void LoseCoinsInTime(float time)
    {
        if (isCoinWorking)
        {
            floatCoin = Mathf.MoveTowards(floatCoin, 0f, time * Time.deltaTime);
            coins = Mathf.FloorToInt(floatCoin);
        }
    }

    public void SetCoinStatusWorking(bool status)
    {
        if (dialogue.DialogueIsRunning())
        {
            isCoinWorking = false;
        }
        else
        {
            isCoinWorking = status;
        }
        
    }

    public float GetCoins()
    {
        return floatCoin;
    }

}
