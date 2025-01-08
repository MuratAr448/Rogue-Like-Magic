using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Spells : MonoBehaviour
{
    public enum Status
    {
        offense,
        defense,
        other
    }
    public Status state;
    public string spellName;
    public int manaCost;
    public string spellDiscription;
    public int cooldownTimer;
    public int cooldownTime;
}
