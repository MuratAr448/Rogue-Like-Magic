using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatReward : MonoBehaviour
{
    public bool gainHp;
    public int gainMaxstats;
    public string manaCost = "---";
    public string spellDiscription;
    public string spellName;
    public string damage = "---";
    public void GetHp()
    {
        Player player = FindObjectOfType<Player>();
        player.healthMax += gainMaxstats;
        player.healthPoints = player.healthMax;
    }
    public void GetMp()
    {
        Player player = FindObjectOfType<Player>();
        player.manaMax += gainMaxstats;
        player.manaPoints = player.manaMax;
    }
}
