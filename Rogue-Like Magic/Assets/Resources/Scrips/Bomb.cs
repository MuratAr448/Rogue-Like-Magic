using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Item
{
    public int damageAmount;
    public void Use()
    {
        TurnSystem turnSystem = FindObjectOfType<TurnSystem>();
        for (int i = 0; i < turnSystem.enemyList.Count; i++)
        {
            Monster monster = turnSystem.enemyList[i];
            monster.TakeDamage(damageAmount);
        }
        Destroy(gameObject);
    }
}
