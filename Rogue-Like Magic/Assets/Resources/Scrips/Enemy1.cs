using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Monster
{
    protected override void DoSecondMove()
    {
        int chosenEnemy = Random.Range(0, turnSystem.enemyList.Count);
        turnSystem.enemyList[chosenEnemy].healthPoints += turnSystem.enemyList[chosenEnemy].healthMax/3;
    }
}
