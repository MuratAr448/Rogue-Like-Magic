using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSkeleton : OtherSpells
{
    [SerializeField] private GameObject Skeleton;
    [SerializeField] private Transform Transform;
    protected override void GetInfo()
    {
        Transform = FindObjectOfType<Player>().transform;
        
        player.skeletonActive = true;
        Instantiate(Skeleton,new Vector3(-2.5f,0,0),Quaternion.identity, Transform);
    }
}
