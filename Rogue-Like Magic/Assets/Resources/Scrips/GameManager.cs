using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Player player;
    TurnSystem TurnSystem;

    public Text coins;
    public float coinsCount;
    private float coinsDisplay;
    private float transitionSpeed = 10;

    [SerializeField] private GameObject Enemy1;
    [SerializeField] private GameObject Enemy2;
    [SerializeField] private GameObject Enemy3;
    [SerializeField] private GameObject Enemy4;
    [SerializeField] private GameObject Enemy5;
    [SerializeField] private GameObject Boss;

    private bool doneBattle = false;
    public int difeculty = 0;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        TurnSystem = GetComponent<TurnSystem>();
    }
    private void Update()
    {
        ShowCoins();
        if (TurnSystem.enemyList.Count==0&& !doneBattle)
        {
            doneBattle = true;
            WinBattle();
        }
    }
    private void ShowCoins()
    {
        coinsCount = player.coins;
        coinsDisplay = Mathf.MoveTowards(coinsDisplay, coinsCount, transitionSpeed * Time.deltaTime);
        int displayC = Mathf.RoundToInt(coinsDisplay);
        coins.text = "Coin amount: "+ displayC;
    }
    public void NewBattle()
    {
        NewEnemys();
        doneBattle = false;
        TurnSystem.turns = 0;
        TurnSystem.actionMenu.SetActive(true);
        TurnSystem.useButton.SetActive(true);
        TurnSystem.endTurnButton.enabled = true;
        TurnSystem.endTurnImage.enabled = true;
        TurnSystem.endTurnText.enabled = true;
    }
    private void NewEnemys()
    {
        //Instantiate(Enemy1);
    }
    public void WinBattle()
    {
        TurnSystem.actionMenu.SetActive(false);
        TurnSystem.useButton.SetActive(false);
        TurnSystem.endTurnB.SetActive(false);
    }
}
