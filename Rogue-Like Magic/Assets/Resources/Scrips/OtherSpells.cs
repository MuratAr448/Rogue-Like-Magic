using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherSpells : Spells
{
    public enum SpellOht
    {
        Skeleton,
        ArrowRain
    }
    public SpellOht spellOht;
    public string smallDiscription;
    public void Use()
    {
        GetInfo();
    }
    protected virtual void GetInfo()
    {

    }
}
