using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public List<Monster> enemyList;
    Player player;
    private bool playersTurn = true;
    public int turns = 0;
    private int enemys = 0;

    private float delay = 1.0f;

    public void SwapTurn()
    {
        playersTurn = !playersTurn;
        
        if (playersTurn)
        {
            player = FindObjectOfType<Player>();
            player.manaPoints += player.manaMax;
            turns++;
            Debug.Log(turns);
            //Avaleble spells
        }
        else
        if (!playersTurn)
        {
            enemys = 0;
            StartCoroutine(EnemyAttacks());
        }

    }
    private IEnumerator EnemyAttacks()
    {
        if (enemys > enemyList.Count)
        {
            SwapTurn();
            StopCoroutine(EnemyAttacks());
        }   
        player = FindObjectOfType<Player>();
        yield return new WaitForSeconds(delay);
        /*        
        enemyList[enemys].moves = UnityEngine.Random.Range(0, 2);
        if (enemyList[enemys].moves==1)
        {
            if (!enemyList[enemys].secondMoveActive)
            {
                enemyList[enemys].SecondMove();
            }
            else
            {
                player.hurt(enemyList[enemys].Attack());
            }
        } 
        else
        {
            
        }
         */
        StartCoroutine(player.GetHurt(enemyList[enemys].Attack()));
        enemys++;
        if (enemys < enemyList.Count)
        {
            StartCoroutine(EnemyAttacks());
        }
    }
}
