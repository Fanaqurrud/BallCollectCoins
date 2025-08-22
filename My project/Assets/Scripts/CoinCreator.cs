using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCreator : MonoBehaviour
{
    public GameObject CoinPrefab;
    public GameObject ArrowPrefab;
    public Transform Player;

    public List<Coin> CoinsList = new List<Coin>();
    public Text CoinsText;

    private GameObject arrowInstance;
    public float maxArrowDistance = 20f;

    void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            Vector3 position = new Vector3(Random.Range(-20f, 20f), 0.5f, Random.Range(-20f, 20f));
            GameObject newCoin = Instantiate(CoinPrefab, position, Quaternion.identity);
            CoinsList.Add(newCoin.GetComponent<Coin>());
        }
        TextCoin();

        if (ArrowPrefab != null)
        {
            arrowInstance = Instantiate(ArrowPrefab, Player.position + Vector3.up, Quaternion.identity);
        }
    }

    public void CollectCoin(Coin coin)
    {
        CoinsList.Remove(coin);
        Destroy(coin.gameObject);

        TextCoin();
    }

    void TextCoin()
    {
        CoinsText.text = "ќсталось собрать: " + CoinsList.Count.ToString();
    }



    void Update()
    {
        UpdateArrow();
    }

    void UpdateArrow()
    {
        if (arrowInstance == null || Player == null) return;

        Coin closest = GetClosest(Player.position);
        if (closest == null)
        {
            arrowInstance.SetActive(false);
            return;
        }

        float distance = Vector3.Distance(Player.position, closest.transform.position);

        if (distance > maxArrowDistance)
        {
            arrowInstance.SetActive(false);
            return;
        }

        arrowInstance.SetActive(true);
        arrowInstance.transform.position = Player.position + Vector3.up * 1.5f;

        // »справленное направление стрелки:
        Vector3 direction = closest.transform.position - arrowInstance.transform.position;
        direction.y = 0;

        // ѕоворачиваем стрелку правильно, учитыва€ ее начальную ориентацию
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(90, 0, -90);
            arrowInstance.transform.rotation = targetRotation;
        }
    }
public Coin GetClosest(Vector3 point)
    {
        float minDistance = Mathf.Infinity;
        Coin closestcoin = null;
        for (int i = 0; i < CoinsList.Count; i++)
        {
            float distance = Vector3.Distance(point, CoinsList[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestcoin = CoinsList[i];
            }
        }
        return closestcoin;
    }

}