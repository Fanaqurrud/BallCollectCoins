using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CoinCreator CoinCreator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Coin>())
        {
            CoinCreator.CollectCoin(other.GetComponent<Coin>());
        }
    }
}
