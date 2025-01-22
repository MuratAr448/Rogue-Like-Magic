using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    Player player;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject itemH;
    [SerializeField] private GameObject itemM;
    [SerializeField] private GameObject itemB;
    [SerializeField] private Text itemTextName;
    [SerializeField] private Text itemText;
    [SerializeField] private Text itemTextCostH;
    [SerializeField] private Text itemTextCostM;
    [SerializeField] private Text itemTextCostB;
    private int itemCost;
    GameManager manager;
    private void Update()
    {
        manager = FindObjectOfType<GameManager>();
        HealthP healthP = itemH.GetComponent<HealthP>();
        ManaP manaP = itemM.GetComponent<ManaP>();
        Bomb bomb = itemB.GetComponent<Bomb>();
        itemCost = Mathf.RoundToInt(healthP.costs * (manager.turnAmount * 0.1f + 1));
        itemTextCostH.text = "" + itemCost;
        itemCost = Mathf.RoundToInt(manaP.costs * (manager.turnAmount * 0.1f + 1));
        itemTextCostM.text = "" + itemCost;
        itemCost = Mathf.RoundToInt(bomb.costs * (manager.turnAmount * 0.1f + 1));
        itemTextCostB.text = "" + itemCost;
    }
    public void CheckBoughtItem()
    {
        HealthP healthP = itemH.GetComponent<HealthP>();
        ManaP manaP = itemM.GetComponent<ManaP>();
        Bomb bomb = itemB.GetComponent<Bomb>();
        if (itemTextName.text == healthP.itemName)
        {
            BuyHealthP();
        }
        else if (itemTextName.text == manaP.itemName)
        {
            BuyManaP();
        } 
        else if (itemTextName.text == bomb.itemName)
        {
            BuyBombs();
        }
        itemTextName.text = "----";
        itemText.text = "-------------";
    }
    public void SeediscriptionHealthP()
    {
        HealthP item = itemH.GetComponent<HealthP>();
       
        itemTextName.text = item.itemName;
        itemText.text = item.itemDiscription;
    }
    public void SeediscriptionManaP()
    {
        ManaP item = itemM.GetComponent<ManaP>();
        itemCost = Mathf.RoundToInt(item.costs * (manager.turnAmount * 0.1f + 1));
        itemTextName.text = item.itemName;
        itemText.text = item.itemDiscription;
    }
    public void SeediscriptionBombs()
    {
        Bomb item = itemB.GetComponent<Bomb>();
        itemCost = Mathf.RoundToInt(item.costs * (manager.turnAmount * 0.1f + 1));
        itemTextName.text = item.itemName;
        itemText.text = item.itemDiscription;
    }

    private void BuyHealthP()
    {
        HealthP item = itemH.GetComponent<HealthP>();
        itemCost = Mathf.RoundToInt(item.costs * (manager.turnAmount*0.1f+1));
        if (CheckMoney(itemCost))
        {
            int inventorySlotChekker = 1;
            for (int i = 0; i < inventorySlotChekker; i++)
            {
                GameObject temp = inventory.transform.GetChild(i).gameObject;//after the spell is used the spell go's back in the spell grid in the first slot posible
                if (temp.transform.childCount == 0)
                {
                    Instantiate(itemH, Vector3.zero, Quaternion.identity, temp.transform);
                    player.coins -= itemCost;
                }
                else
                {
                    inventorySlotChekker++;
                }
            }
        }
    }
    private void BuyManaP() 
    {
        ManaP item = itemM.GetComponent<ManaP>();
        itemCost = Mathf.RoundToInt(item.costs * (manager.turnAmount * 0.1f + 1));
        if (CheckMoney(itemCost))
        {
            int inventorySlotChekker = 1;
            for (int i = 0; i < inventorySlotChekker; i++)
            {
                GameObject temp = inventory.transform.GetChild(i).gameObject;//after the spell is used the spell go's back in the spell grid in the first slot posible
                if (temp.transform.childCount == 0)
                {
                    Instantiate(itemM, Vector3.zero, Quaternion.identity, temp.transform);
                    player.coins -= itemCost;
                }
                else
                {
                    inventorySlotChekker++;
                }
            }
        }
    }
    private void BuyBombs() 
    {
        Bomb item = itemB.GetComponent<Bomb>();
        itemCost = Mathf.RoundToInt(item.costs * (manager.turnAmount * 0.1f + 1));
        if (CheckMoney(itemCost))
        {
            int inventorySlotChekker = 1;
            for (int i = 0; i < inventorySlotChekker; i++)
            {
                GameObject temp = inventory.transform.GetChild(i).gameObject;//after the spell is used the spell go's back in the spell grid in the first slot posible
                if (temp.transform.childCount == 0)
                {
                    Instantiate(itemB, Vector3.zero, Quaternion.identity, temp.transform);
                    player.coins -= itemCost;
                }
                else
                {
                    inventorySlotChekker++;
                }
            }
        }
    }
    private bool CheckMoney(int cost)
    {
        player = FindObjectOfType<Player>();

        int inventorySlotChekker = 1;
        bool isInventory = false;
        for (int i = 0; i < inventorySlotChekker; i++)
        {
            GameObject temp = inventory.transform.GetChild(i).gameObject;//problem
            if (temp.transform.childCount == 0)
            {
                isInventory = true;
            }
            else
            {
                if (inventorySlotChekker<=2)
                {
                    inventorySlotChekker++;
                }
            }
        }
        if (cost<=player.coins&& isInventory)
        {
            return true;
        }else
        {
            return false;
        }
    }
}
