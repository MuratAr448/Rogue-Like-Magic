using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaP : Item
{
    public void Use()
    {
        Player player = FindObjectOfType<Player>();
        int addHealth = Mathf.RoundToInt(player.manaMax * 0.4f);
        player.manaPoints += addHealth;
        Destroy(gameObject);
    }

}
