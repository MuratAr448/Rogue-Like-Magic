using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ChosenReward : MonoBehaviour
{
    public bool isRare;
    public GameObject Reward;
    GameManager gameManager;
    [SerializeField] private GameObject Image;
    [SerializeField] private Text Spellname;
    [SerializeField] private Text SpellDiscription;
    [SerializeField] private Text SpellManaCost;
    [SerializeField] private Text SpellDamage;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Image.GetComponent<Image>().sprite = Reward.GetComponent<Image>().sprite;
        if (Reward.GetComponent<Spells>() != null)
        {
            Spells spells = Reward.GetComponent<Spells>();
            Spellname.text = spells.spellName;
            SpellDiscription.text = spells.spellDiscription;
            SpellManaCost.text = "Mana Cost: " + spells.manaCost;
            if (Reward.GetComponent<OffenseSpell>() != null)
            {
                OffenseSpell spell = Reward.GetComponent<OffenseSpell>();
                SpellDamage.text = "Damage" + spell.damage;
            }
            if (Reward.GetComponent<DefenseSpell>() != null)
            {
                DefenseSpell spell = Reward.GetComponent<DefenseSpell>();
                SpellDamage.text = "Damage" + spell.effect;
            }
        }
        else
        {
            StatReward statReward = Reward.GetComponent<StatReward>();
            Spellname.text = statReward.spellName;
            SpellDiscription.text = statReward.spellDiscription;
            SpellManaCost.text = ""+statReward.gainMaxstats;
            SpellDamage.text = statReward.damage;
        }
    }
    public void GetReward()
    {
        RewardSystem rewardSystem = FindObjectOfType<RewardSystem>();
        if (Reward.GetComponent<Spells>() != null)
        {
            CastSlot castSlot = FindObjectOfType<CastSlot>();
            int spellSlotChekker = 1;
            for (int i = 0; i < spellSlotChekker; i++)
            {
                GameObject temp = castSlot.spellGrid.transform.GetChild(i).gameObject;//after the spell is used the spell go's back in the spell grid in the first slot posible
                if (temp.transform.childCount == 0)
                {
                    Instantiate(Reward, transform.position, transform.rotation, temp.transform);
                    rewardSystem.RemoveIt(isRare, Reward);
                }
                else
                {
                    spellSlotChekker++;
                }
            }
        }else
        {
            StatReward statReward = Reward.GetComponent<StatReward>();
            if (statReward.gainHp == true)
            {
                statReward.GetHp();
            }
            else
            {
                statReward.GetMp();
            }
        }
        rewardSystem.Noshow();
        gameManager.SeePath();
    }
}
