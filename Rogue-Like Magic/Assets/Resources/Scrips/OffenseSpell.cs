using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffenseSpell : Spells
{
    public enum Element
    {
        none,
        fire,
        lightning,
        psycic,
        rock
    }
    public Element elements;
    public int damage;
}
