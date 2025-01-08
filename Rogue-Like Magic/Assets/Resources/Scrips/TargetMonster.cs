using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetMonster : MonoBehaviour
{
    public Monster monster;
    public GameObject target;
    public void SetTarget()
    {
        if (monster != null)
        {
            GameObject temp = Instantiate(target);
            target.transform.position = monster.transform.position;
        }
    } 
}
