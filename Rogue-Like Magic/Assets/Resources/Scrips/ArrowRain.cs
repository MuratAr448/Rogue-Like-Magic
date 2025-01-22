using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRain : OtherSpells
{
    public int damage;
    protected override void GetInfo()
    {
        TurnSystem = FindObjectOfType<TurnSystem>();
        for (int i = 0; i < TurnSystem.enemyList.Count; i++)
        {
            TurnSystem.enemyList[i].GetComponent<Monster>().TakeDamage(damage);
        }
    }
}
