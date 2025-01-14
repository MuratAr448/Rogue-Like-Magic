using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    Player player;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject itemH;
    [SerializeField] private int costIH;
    [SerializeField] private GameObject itemM;
    [SerializeField] private int costIM;
    [SerializeField] private GameObject itemB;
    [SerializeField] private int costIB;
    public void BuyHealthP()
    {
        if (CheckMoney(costIH))
        {
            int inventorySlotChekker = 1;
            for (int i = 0; i < inventorySlotChekker; i++)
            {
                GameObject temp = inventory.transform.GetChild(i).gameObject;//after the spell is used the spell go's back in the spell grid in the first slot posible
                if (temp.transform.childCount == 0)
                {
                    Instantiate(itemH, Vector3.zero, Quaternion.identity, temp.transform);
                    player.coins -= costIH;
                }
                else
                {
                    inventorySlotChekker++;
                }
            }
        }
    }
    public void BuyManaP() 
    {
        if (CheckMoney(costIM))
        {
            int inventorySlotChekker = 1;
            for (int i = 0; i < inventorySlotChekker; i++)
            {
                GameObject temp = inventory.transform.GetChild(i).gameObject;//after the spell is used the spell go's back in the spell grid in the first slot posible
                if (temp.transform.childCount == 0)
                {
                    Instantiate(itemM, Vector3.zero, Quaternion.identity, temp.transform);
                    player.coins -= costIM;
                }
                else
                {
                    inventorySlotChekker++;
                }
            }
        }
    }
    public void BuyBombs() 
    {
        if (CheckMoney(costIB))
        {
            int inventorySlotChekker = 1;
            for (int i = 0; i < inventorySlotChekker; i++)
            {
                GameObject temp = inventory.transform.GetChild(i).gameObject;//after the spell is used the spell go's back in the spell grid in the first slot posible
                if (temp.transform.childCount == 0)
                {
                    Instantiate(itemB, Vector3.zero, Quaternion.identity, temp.transform);
                    player.coins -= costIB;
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
