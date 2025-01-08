using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CastSlot : MonoBehaviour
{
    Player player;
    TurnSystem TurnSystem;
    TargetMonster targetMonster;
    public GameObject spellSlot;
    public GameObject spellGrid;
    [SerializeField] private Text spellDamageText;
    [SerializeField] private Text spellManaCostText;
    [SerializeField] private Text spellDiscriptionText;
    [SerializeField] private Text spellNameText;
    Spells Spells;
    public OffenseSpell OffenseSpell;
    DefenseSpell DefenseSpell;
    OtherSpells OtherSpells;
    private void Update()
    {
        if (spellSlot.transform.childCount == 0)
        {
            spellDamageText.text = "---";
            spellManaCostText.text = "---";
            spellDiscriptionText.text = "---";
            spellNameText.text = "---";
        }
        else
        {
            if (!spellSlot.transform.GetChild(0).GetComponent<Spells>() == true)
            {
                
            }
            else
            {
                Spells = spellSlot.transform.GetChild(0).GetComponent<Spells>();
                spellNameText.text = Spells.name;
                spellDiscriptionText.text = Spells.spellDiscription;
                if (Spells.state == Spells.Status.offense)
                {
                    OffenseSpell = spellSlot.transform.GetChild(0).GetComponent<OffenseSpell>();
                    spellDamageText.text = "Damage: " + OffenseSpell.damage;
                    spellManaCostText.text = "Mana Cost: " + Spells.manaCost;
                }
                else if (Spells.state == Spells.Status.defense)
                {
                    DefenseSpell = spellSlot.transform.GetChild(0).GetComponent<DefenseSpell>();
                    spellDamageText.text = DefenseSpell.smallDiscription + DefenseSpell.shielding;
                    spellManaCostText.text = "Mana Cost: " + Spells.manaCost;
                }
                else if (Spells.state == Spells.Status.other)
                {
                    spellDamageText.text = OtherSpells.smallDiscription;
                    spellManaCostText.text = "Mana Cost: " + Spells.manaCost;
                }
            }
        }
    }

    public void Use()
    {
        targetMonster = FindObjectOfType<TargetMonster>();
        TurnSystem = FindObjectOfType<TurnSystem>();
        player = FindObjectOfType<Player>();
        if (!spellSlot.transform.GetChild(0).GetComponent<Spells>() == true)
        {

        }else
        {
            if (Manacheck())
            {
                if (Spells.state == Spells.Status.offense)
                {
                    player.manaPoints -= Spells.manaCost;
                    
                    Spells.cooldownTimer = Spells.cooldownTime + TurnSystem.turns;
                    targetMonster.monster.TakeDamage(OffenseSpell.damage);

                }
                else if (Spells.state == Spells.Status.defense)
                {

                }
            }

            if (Spells.state == Spells.Status.other)
            {

            }
        }
    }
    private bool Manacheck()
    {
        int tempMana = Mathf.RoundToInt(player.manaPoints);
        if (tempMana - Spells.manaCost < 0 || Spells.cooldownTimer > TurnSystem.turns) 
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
