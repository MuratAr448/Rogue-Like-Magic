using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffenseSpell : Spells
{
    public enum Element
    {
        none,
        fire,
        water,
        grass,
        light
    }
    public Element elements;
    public int damage;

    public IEnumerable Attack()
    {
        yield return new WaitForSeconds(0.1f);
    }
}
