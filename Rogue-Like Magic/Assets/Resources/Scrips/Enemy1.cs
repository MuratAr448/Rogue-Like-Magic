using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Monster
{
    int chosenEnemy;
    protected override void DoSecondMoveAble()
    {
        chosenEnemy = Random.Range(0, turnSystem.enemyList.Count);
        if (turnSystem.enemyList[chosenEnemy].healthPoints == turnSystem.enemyList[chosenEnemy].healthMax)
        {
            secondMoveAble = false;
        }else
        {
            secondMoveAble = true;
        }
    }
    protected override void DoSecondMove()
    {
        if (turnSystem.enemyList[chosenEnemy].GetComponent<Boss>())
        {
            turnSystem.enemyList[chosenEnemy].healthPoints += turnSystem.enemyList[chosenEnemy].healthMax / 5;
        }
        else
        {
            turnSystem.enemyList[chosenEnemy].healthPoints += turnSystem.enemyList[chosenEnemy].healthMax / 3;

        }
    }
}
