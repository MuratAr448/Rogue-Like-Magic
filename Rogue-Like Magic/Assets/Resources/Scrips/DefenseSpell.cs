using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseSpell : Spells
{
    public int effect;
    public string smallDiscription;
    
    public void Use()
    {
        GetInfo();
    }
    protected virtual void GetInfo()
    {

    }
}
