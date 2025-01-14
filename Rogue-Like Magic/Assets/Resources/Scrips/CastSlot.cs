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
    public bool inCastSlot;
    private void Update()
    {
        if (spellSlot.transform.childCount == 0)
        {
            spellDamageText.text = "---";
            spellManaCostText.text = "---";
            spellDiscriptionText.text = "---";
            spellNameText.text = "---";
            inCastSlot=false;
        }
        else
        {
            inCastSlot = true;
            if (!spellSlot.transform.GetChild(0).GetComponent<Spells>() == true)
            {//if items are in the cast slot
                if (spellSlot.transform.GetChild(0).GetComponent<HealthP>() == true)
                {
                    HealthP healthP = spellSlot.transform.GetChild(0).GetComponent<HealthP>();
                    spellNameText.text = healthP.name;
                    spellDiscriptionText.text = healthP.itemDiscription;
                    spellManaCostText.text = "---";
                    spellDamageText.text = "---";
                }
                else if (spellSlot.transform.GetChild(0).GetComponent<ManaP>() == true)
                {
                    ManaP ManaP = spellSlot.transform.GetChild(0).GetComponent<ManaP>();
                    spellNameText.text = ManaP.name;
                    spellDiscriptionText.text = ManaP.itemDiscription;
                    spellManaCostText.text = "---";
                    spellDamageText.text = "---";
                }
                else if (spellSlot.transform.GetChild(0).GetComponent<Bomb>() == true)
                {
                    Bomb bomb = spellSlot.transform.GetChild(0).GetComponent<Bomb>();
                    spellNameText.text = bomb.name;
                    spellDiscriptionText.text = bomb.itemDiscription;
                    spellManaCostText.text = "---";
                    spellDamageText.text = "Damage: " + bomb.damageAmount;
                }
            }
            else
            {//get info of the spell in the cast slot
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
            if(spellSlot.transform.GetChild(0).GetComponent<HealthP>() == true)
            {
                HealthP healthP = spellSlot.transform.GetChild(0).GetComponent<HealthP>();
                healthP.Use();
            }
            else if (spellSlot.transform.GetChild(0).GetComponent<ManaP>() == true)
            {
                ManaP manaP = spellSlot.transform.GetChild(0).GetComponent<ManaP>();
                manaP.Use();
            }
            else if (spellSlot.transform.GetChild(0).GetComponent<Bomb>() == true)
            {
                Bomb bomb = spellSlot.transform.GetChild(0).GetComponent<Bomb>();
                bomb.Use();
            }
        }else
        {
            if (Manacheck())
            {
                if (Spells.state == Spells.Status.offense&& targetMonster.target!=null)
                {
                    int spellSlotChekker = 1;
                    for(int i = 0; i < spellSlotChekker; i++)
                    {
                        GameObject temp = spellGrid.transform.GetChild(i).gameObject;//after the spell is used the spell go's back in the spell grid in the first slot posible
                        if (temp.transform.childCount == 0)
                        {
                            StartCoroutine(SpellInBook(temp.transform));
                        }
                        else
                        {
                            spellSlotChekker++;
                        }
                    }
                    player.manaPoints -= Spells.manaCost;
                    
                    Spells.cooldownTimer = Spells.cooldownTime + TurnSystem.turns;
                    targetMonster.target.GetComponent<Monster>().TakeDamage(OffenseSpell.damage);//do damage with spell
                    targetMonster.target = null;

                }
                else if (Spells.state == Spells.Status.defense)
                {
                    int spellSlotChekker = 1;
                    for (int i = 0; i < spellSlotChekker; i++)
                    {
                        GameObject temp = spellGrid.transform.GetChild(i).gameObject;//after the spell is used the spell go's back in the spell grid in the first slot posible
                        if (temp.transform.childCount == 0)
                        {
                            StartCoroutine(SpellInBook(temp.transform));
                        }
                        else
                        {
                            spellSlotChekker++;
                        }
                    }
                    player.manaPoints -= Spells.manaCost;
                    Spells.cooldownTimer = Spells.cooldownTime + TurnSystem.turns;
                    DefenseSpell.Use();
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
        if (tempMana - Spells.manaCost < 0 || Spells.cooldownTimer > TurnSystem.turns) //checks if it is posible to use the spell
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private IEnumerator SpellInBook(Transform transform)
    {
        yield return new WaitForSeconds(0.1f);
        Spells.gameObject.transform.SetParent(transform);
    }
}
