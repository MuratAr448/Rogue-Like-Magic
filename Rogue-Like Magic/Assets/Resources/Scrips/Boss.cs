using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Monster
{
    protected override void DoSecondMoveAble()
    {
        if (gameManager.EnemyplaceFF3.transform.childCount == 0&& gameManager.EnemyplaceFL3.transform.childCount == 0)
        {
            secondMoveAble = true;
        }else
        {
            secondMoveAble = false;
        }
    }
    protected override void DoSecondMove()
    {
        GameObject summonEnemy = choseSummon();
        Monster  monster = summonEnemy.GetComponent<Monster>();
        monster.isSummond = true;
        if (summonEnemy == gameManager.Enemy3|| summonEnemy == gameManager.Enemy4)
        {
            Instantiate(summonEnemy, gameManager.EnemyplaceFF3.transform.position, Quaternion.identity, gameManager.EnemyplaceFF3.transform);
        }
        else
        {
            Instantiate(summonEnemy, gameManager.EnemyplaceFF3.transform.position, Quaternion.identity, gameManager.EnemyplaceFF3.transform);
            Instantiate(summonEnemy, gameManager.EnemyplaceFL3.transform.position, Quaternion.identity, gameManager.EnemyplaceFL3.transform);
        }

    }
    private GameObject choseSummon()
    {
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                return gameManager.Enemy1;
                break;
            case 1:
                return gameManager.Enemy2;
                break;
            case 2:
                return gameManager.Enemy3;
                break;
            case 3:
                return gameManager.Enemy4;
                break;
            default:
                return null;
                break;
        }
    }
}
