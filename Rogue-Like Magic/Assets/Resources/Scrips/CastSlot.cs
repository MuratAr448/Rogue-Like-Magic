using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastSlot : MonoBehaviour
{
    TurnSystem TurnSystem;
    Monster monster;
    [SerializeField] private GameObject spellSlot;
    [SerializeField] private Text spellDamageText;
    [SerializeField] private Text spellManaCostText;
    [SerializeField] private Text spellDiscriptionText;
    [SerializeField] private Text spellNameText;
    Spells Spells;
    OffenseSpell OffenseSpell;
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
            Spells = spellSlot.transform.GetChild(0).GetComponent<Spells>();
            spellNameText.text = Spells.name;
            spellDiscriptionText.text = Spells.spellDiscription;
            if (Spells.state == Spells.status.offense)
            {
                OffenseSpell = spellSlot.transform.GetChild(0).GetComponent<OffenseSpell>();
                spellDamageText.text = "Damage: " + OffenseSpell.damage;
                spellManaCostText.text = "Mana Cost: " + Spells.manaCost;
            }
            else if(Spells.state == Spells.status.defense)
            {
                DefenseSpell = spellSlot.transform.GetChild(0).GetComponent<DefenseSpell>();
                spellDamageText.text = DefenseSpell.smallDiscription + DefenseSpell.shilding;
                spellManaCostText.text = "Mana Cost: " + Spells.manaCost;
            }
            else if (Spells.state == Spells.status.other)
            {
                spellDamageText.text = OtherSpells.smallDiscription;
                spellManaCostText.text = "Mana Cost: " + Spells.manaCost;
            }
            
        }
    }
    public void Use()
    {

    }
}
