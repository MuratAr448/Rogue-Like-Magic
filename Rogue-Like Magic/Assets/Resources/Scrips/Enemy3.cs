using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Monster
{
    protected override void DoSecondMove()
    {
        StartCoroutine(Attacktwice());
    }
    private IEnumerator Attacktwice()
    {
        if (player.skeletonActive)
        {
            Skeleton skeleton = FindObjectOfType<Skeleton>();
            StartCoroutine(skeleton.GetSkeletonHurt(Attack()));
        }else
        if (player.shieldOn)
        {
            StartCoroutine(player.GetShieldHurt(Attack()));
        }
        else
        {
            StartCoroutine(player.GetHurt(Attack()));
        }
        yield return new WaitForSeconds(0.5f);
        if (player.skeletonActive)
        {
            Skeleton skeleton = FindObjectOfType<Skeleton>();
            StartCoroutine(skeleton.GetSkeletonHurt(Attack()));
        }
        else
        if (player.shieldOn)
        {
            StartCoroutine(player.GetShieldHurt(Attack()));
        }
        else
        {
            StartCoroutine(player.GetHurt(Attack()));
        }
    }
}
