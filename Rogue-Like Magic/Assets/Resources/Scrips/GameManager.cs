using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Player player;

    public Text coins;
    public float coinsCount;
    private float coinsDisplay;
    private float transitionSpeed = 10;
    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        ShowCoins();
    }
    private void ShowCoins()
    {
        coinsCount = player.coins;
        coinsDisplay = Mathf.MoveTowards(coinsDisplay, coinsCount, transitionSpeed * Time.deltaTime);
        int displayC = Mathf.RoundToInt(coinsDisplay);
        coins.text = "Coin amount: "+ displayC;
    }
}
