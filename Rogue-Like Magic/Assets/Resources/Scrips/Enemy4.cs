using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : Monster
{
    protected override void DoSecondMove()
    {
        if(player.shieldOn)
        {
            StartCoroutine(player.GetShieldHurt(Attack() * 2));
        }
        else
        {
            StartCoroutine(player.GetHurt(Attack() * 2));
        }
        
    }
}
