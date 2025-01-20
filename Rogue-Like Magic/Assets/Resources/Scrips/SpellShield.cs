using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellShield : DefenseSpell
{
    protected override void GetInfo()
    {
        player.ShieldhealthPoints = effect;
        player.shieldOn = true;
    }
}
