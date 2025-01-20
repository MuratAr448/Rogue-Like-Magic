using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHeal : DefenseSpell
{

    protected override void Update()
    {
        base.Update();
        effect = Mathf.RoundToInt(player.healthMax * 0.5f);
    }
    protected override void GetInfo()
    {
        player.healthPoints += effect;
    }
}
