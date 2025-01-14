using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HealthP : Item
{
    public void Use()
    {
        Player player = FindObjectOfType<Player>();
        int addHealth = Mathf.RoundToInt(player.healthMax*0.4f);
        player.healthPoints += addHealth;
        Destroy(gameObject);
    }
}
