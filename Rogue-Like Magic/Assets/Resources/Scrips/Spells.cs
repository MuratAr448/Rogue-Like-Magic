using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    TurnSystem TurnSystem;
    public GameObject countdownTimer;
    private Text countdownText;

    private void Start()
    {
        TurnSystem = FindObjectOfType<TurnSystem>();
        countdownText = countdownTimer.GetComponentInChildren<Text>();

    }
    private void Update()
    {
        if (cooldownTimer > TurnSystem.turns)
        {
            countdownTimer.SetActive(true);//shows cooldownTimer
            countdownText.text = "" + (cooldownTimer - TurnSystem.turns);
        }
        else
        {
            countdownTimer.SetActive(false);
        }
        
    }
}
