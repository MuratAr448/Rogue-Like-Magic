using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    public List<Monster> enemyList;
    Player player;
    private bool playersTurn = true;
    public int turns = 0;
    private int enemys = 0;

    private float delay = 1.0f;
    TargetMonster targetMonster;
    public GameObject actionMenu;
    public GameObject useButton;
    CastSlot slot;
    Button endTurnButton;
    Image endTurnImage;
    Text endTurnText;


    private void Start()
    {
        slot = FindObjectOfType<CastSlot>();
        turns = 0;
        targetMonster = FindObjectOfType<TargetMonster>();
        endTurnButton = GetComponent<Button>();
        endTurnImage = GetComponent<Image>();
        endTurnText = GetComponentInChildren<Text>();
        SwapTurn();
        
    }
    public void SwapTurn()
    {
        if (!slot.inCastSlot)
        {
            if (playersTurn)
            {
                player = FindObjectOfType<Player>();
                player.manaPoints += player.manaMax;
                turns++;//turn count
                Debug.Log(turns);
                playersTurn = false;
                StartCoroutine(TurnOn());
            }
            else
            if (!playersTurn)
            {
                targetMonster.target = null;
                actionMenu.SetActive(false);
                useButton.SetActive(false);
                endTurnButton.enabled = false;
                endTurnImage.enabled = false;
                endTurnText.enabled = false;
                enemys = 0;
                StartCoroutine(EnemyAttacks());
                playersTurn = true;
            }
        }
    }
    private IEnumerator EnemyAttacks()
    {
        player = FindObjectOfType<Player>();
        yield return new WaitForSeconds(delay);
        enemyList[enemys].moves = UnityEngine.Random.Range(0, 2);//the enemy has a chance to use the second move but if is not ready than normal attack
        if (enemyList[enemys].moves==1)
        {
            if (!enemyList[enemys].secondMoveActive)
            {
                enemyList[enemys].SecondMove();
            }
            else
            {
                StartCoroutine(player.GetHurt(enemyList[enemys].Attack()));
            }
        } 
        else
        {
            StartCoroutine(player.GetHurt(enemyList[enemys].Attack()));
        }
        
        enemys++;
        if (enemys < enemyList.Count)
        {
            StartCoroutine(EnemyAttacks());//every enemy attacks
        }else
        if (enemys >= enemyList.Count)
        {
            SwapTurn();
        }
    }
    private IEnumerator TurnOn()
    {
        yield return new WaitForSeconds(delay);
        actionMenu.SetActive(true);
        useButton.SetActive(true);
        endTurnButton.enabled = true;
        endTurnImage.enabled = true;
        endTurnText.enabled = true;
    }
}
