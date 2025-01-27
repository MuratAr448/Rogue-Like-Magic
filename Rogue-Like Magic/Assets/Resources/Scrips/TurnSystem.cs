using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    public List<Monster> enemyList;
    Player player;
    public bool playersTurn = true;
    public int turns = 0;
    private int enemys = 0;

    private float delay = 1.0f;
    TargetMonster targetMonster;
    public GameObject actionMenu;
    CastSlot slot;


    private void Start()
    {
        slot = FindObjectOfType<CastSlot>();
        turns = 0;
        targetMonster = FindObjectOfType<TargetMonster>();
        SwapTurn();
        
    }
    public void SwapTurn()
    {
        if (enemyList.Count != 0)
        {
            player = FindObjectOfType<Player>();
            if (!slot.inCastSlot)
            {
                if (playersTurn)
                {
                    
                    player.manaPoints += player.manaMax;
                    turns++;//turn count
                    playersTurn = false;
                    StartCoroutine(TurnOn());
                }
                else
                if (!playersTurn)
                {
                    if (player.skeletonActive)
                    {
                        StartCoroutine(SkeletonAttacks());
                    }
                    else
                    {
                        targetMonster.target = null;
                        actionMenu.SetActive(false);
                        enemys = 0;
                        StartCoroutine(EnemyAttacks());
                        playersTurn = true;
                    }
                }
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
                if (player.skeletonActive)
                {
                    Skeleton skeleton = FindObjectOfType<Skeleton>();
                    StartCoroutine(skeleton.GetSkeletonHurt(enemyList[enemys].Attack()));
                }else
                if (player.shieldOn)
                {
                    StartCoroutine(player.GetShieldHurt(enemyList[enemys].Attack()));
                }
                else
                {
                    StartCoroutine(player.GetHurt(enemyList[enemys].Attack()));
                }
                
            }
        } 
        else
        {
            if (player.skeletonActive)
            {
                Skeleton skeleton = FindObjectOfType<Skeleton>();
                StartCoroutine(skeleton.GetSkeletonHurt(enemyList[enemys].Attack()));
            }
            else
            if (player.shieldOn)
            {
                StartCoroutine(player.GetShieldHurt(enemyList[enemys].Attack()));
            }
            else
            {
                StartCoroutine(player.GetHurt(enemyList[enemys].Attack()));
            }
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
    private IEnumerator SkeletonAttacks()
    {
        targetMonster.target = null;
        actionMenu.SetActive(false);
        yield return new WaitForSeconds(delay);
        Skeleton skeleton = FindObjectOfType<Skeleton>();
        enemyList[0].GetComponent<Monster>().TakeDamageSkeleton(skeleton.damage);
        yield return new WaitForSeconds(delay);

        enemys = 0;
        StartCoroutine(EnemyAttacks());
        playersTurn = true;
    }
    private IEnumerator TurnOn()
    {
        yield return new WaitForSeconds(delay);
        actionMenu.SetActive(true);
    }
}
