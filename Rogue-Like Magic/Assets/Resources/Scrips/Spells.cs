using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Spells : MonoBehaviour
{
    public enum status
    {
        offense,
        defense,
        other
    }
    public status state;
    public string spellName;
    public int manaCost;
    public string spellDiscription;
}
