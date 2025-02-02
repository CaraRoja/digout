using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoin : MonoBehaviour
{

    public CoinManager coin;
    public bool pickedCoin = false;

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
        
    }

    public void AddCoin()
    {
        coin.AddCoins();
    }

    public void AddCoin(float value)
    {
        coin.AddCoins(value);
    }

    public void SetCoinStatusWorking(bool status)
    {
        coin.SetCoinStatusWorking(status);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin") && !pickedCoin)
        {
            pickedCoin = true;
            StartCoroutine(DelayAddCoin());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("INICIOU o Colidindo com o player");
            //coin.SetCoinLoseTimeValue(2f);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
        //Debug.Log("Colidindo com o player STAY");
            coin.SetCoinLoseTimeValue(2f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("SAIU do Colidindo com o player");
            coin.SetCoinLoseTimeValue(1f);
        }
    }

    public IEnumerator DelayAddCoin()
    {
        AddCoin();
        yield return new WaitForSeconds(0.1f);
        pickedCoin = false;
    }
}
