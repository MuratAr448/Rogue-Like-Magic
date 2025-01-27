using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Player player;
    TurnSystem TurnSystem;
    RewardSystem rewardSystem;
    CastSlot CastSlot;

    public Text coins;
    public float coinsCount;
    private float coinsDisplay;
    private float transitionSpeed = 10;

    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Enemy4;
    public GameObject Enemy5;
    public GameObject Boss;

    public GameObject EnemyplaceFF3;
    public GameObject EnemyplaceFF2;
    public GameObject EnemyplaceF1Or3;
    public GameObject EnemyplaceFL2;
    public GameObject EnemyplaceFL3;

    [SerializeField] private GameObject NormalBattle;
    [SerializeField] private GameObject HardBattle;
    [SerializeField] private GameObject ShopButton;
    [SerializeField] private GameObject BossBattle;

    [SerializeField] private List<GameObject> MapGrids;
    private List<GameObject> MapList=new List<GameObject>();
    [SerializeField] private GameObject Map;
    [SerializeField] private GameObject WinScreen;

    [SerializeField] private bool doneBattle = true;
    public int difeculty = 0;
    public int turnAmount = 0;
    [SerializeField] private GameObject Shop;
    private bool bossFight = false;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        TurnSystem = GetComponent<TurnSystem>();
        rewardSystem = GetComponent<RewardSystem>();
        CastSlot = GetComponent<CastSlot>();
        for (int i = 0; i < MapGrids.Count; i++)
        {
            RectTransform rectTransform = MapGrids[i].GetComponent<RectTransform>();
            Vector3 vector3 = rectTransform.localPosition;
            if (i == 0)
            {
                MapList.Add(Instantiate(NormalBattle, vector3, Quaternion.identity,rectTransform));
            }else
            {
                if(i == MapGrids.Count-1)
                {
                    MapList.Add(Instantiate(BossBattle, vector3, Quaternion.identity, rectTransform));
                }else
                {
                    int rand = Random.Range(1, 4);
                    for (int j = 0; j < rand; j++)
                    {
                        MapList.Add(Instantiate(RandomPlace(), vector3, Quaternion.identity, rectTransform));
                    }
                    if (i % 2 == 0)
                    {
                        if (!MapList.Contains(ShopButton))
                        {
                            MapList.Add(Instantiate(ShopButton, vector3, Quaternion.identity, MapGrids[i].transform));
                        }
                    }
                    MapList.Clear();
                }
            }
        }
    }
    private GameObject RandomPlace()
    {
        int rand = Random.Range(0, 6);
        switch (rand)
        {
            case 0:
                return NormalBattle;
                break;
            case 1:
                return NormalBattle;
                break;
            case 2:
                return NormalBattle;
                break;
            case 3:
                return HardBattle;
                break;
            case 4:
                return HardBattle;
                break;
            case 5:
                return ShopButton;
                break;
            default:
                return null;
                break;
        }
    }
    private void Update()
    {
        for (int i = 0;i < MapGrids.Count; i++)
        {
            if (MapGrids[i] == MapGrids[turnAmount])
            {
                for (int j = 0; j < MapGrids[i].transform.childCount; j++)
                {
                    MapGrids[i].transform.GetChild(j).gameObject.GetComponent<Button>().enabled = true;
                }
            }
            else
            {
                for (int j = 0; j < MapGrids[i].transform.childCount; j++)
                {
                    MapGrids[i].transform.GetChild(j).gameObject.GetComponent<Button>().enabled = false;
                }
            }
        }
        ShowCoins();
        if (TurnSystem.enemyList.Count==0&& !doneBattle)
        {
            for (int i = 0; i < CastSlot.spellGrid.transform.childCount; i++)
            {
                GameObject temp = CastSlot.spellGrid.transform.GetChild(i).gameObject;
                if (temp.transform.childCount == 0)
                {

                }
                else
                {
                    Spells spells = temp.transform.GetChild(0).GetComponent<Spells>();
                    spells.cooldownTimer = 0;
                }
            }
            doneBattle = true;
            if (bossFight)
            {
                YouWin();
            }
            else
            {
                WinBattle();
            }
            
        }
        if (doneBattle)
        {
            TurnSystem.actionMenu.SetActive(false);
        }
    }
    private void ShowCoins()
    {
        coinsCount = player.coins;
        coinsDisplay = Mathf.MoveTowards(coinsDisplay, coinsCount, transitionSpeed * Time.deltaTime);
        int displayC = Mathf.RoundToInt(coinsDisplay);
        coins.text = "Coin amount: "+ displayC;
    }
    private IEnumerator NewBattle()
    {
        yield return new WaitForSeconds(0.2f);
        TurnSystem.playersTurn =false;
        doneBattle = false;
        TurnSystem.turns = 0;
        TurnSystem.actionMenu.SetActive(true);
    }
    public void WinBattle()
    {
        Skeleton skeleton = FindObjectOfType<Skeleton>();
        if (skeleton != null)
        {
            Destroy(skeleton.gameObject);
        }
        player.shieldOn = false;
        player.skeletonActive = false;
        player.healthPoints = player.healthMax;
        player.manaPoints = player.manaMax;
        StartCoroutine(rewardSystem.Chose());
        TurnSystem.actionMenu.SetActive(false);
    }
    public void YouWin()
    {
        Skeleton skeleton = FindObjectOfType<Skeleton>();
        if (skeleton != null)
        {
            Destroy(skeleton.gameObject);
        }
        player.shieldOn = false;
        player.skeletonActive = false;
        player.healthPoints = player.healthMax;
        player.manaPoints = player.manaMax;
        TurnSystem.actionMenu.SetActive(false);
        WinScreen.SetActive(true);
    }
    public void IncreaseTA()
    {
        Map.SetActive(false);
        turnAmount++;
        Shop.SetActive(false);
    }
    public void ChooseShop()
    {
        IncreaseTA();
        Shop.SetActive(true);
    }
    public void SeePath()
    {
        Map.SetActive(!Map.activeSelf);
    }
    public void ChooseNormalBattle()
    {
        IncreaseTA();
        difeculty = 1;
        int enemyAmount = Mathf.RoundToInt(difeculty * turnAmount*1.3f);
        int limet = enemyAmount;
        if (limet > 4)
        {
            limet = 4;
        }
        int rand = Random.Range(1, limet);
        switch (rand)
        {
            case 1:
                Instantiate(ChoseEnemy(enemyAmount), EnemyplaceF1Or3.transform.position, Quaternion.identity, EnemyplaceF1Or3.transform);
                break;
            case 2:
                int enemyHalf = Mathf.RoundToInt(enemyAmount / 1.3f);
                Instantiate(ChoseEnemy(enemyHalf), EnemyplaceFF2.transform.position, Quaternion.identity, EnemyplaceFF2.transform);
                enemyHalf = Mathf.RoundToInt(enemyHalf-1.3f);
                Instantiate(ChoseEnemy(enemyHalf), EnemyplaceFL2.transform.position, Quaternion.identity, EnemyplaceFL2.transform);
                break;
            case 3:
                int enemyTurd = Mathf.RoundToInt(enemyAmount / 2);
                Instantiate(ChoseEnemy(enemyTurd), EnemyplaceFF3.transform.position, Quaternion.identity, EnemyplaceFF3.transform);
                enemyTurd = Mathf.RoundToInt(enemyTurd - 1.3f);
                Instantiate(ChoseEnemy(enemyTurd), EnemyplaceF1Or3.transform.position, Quaternion.identity, EnemyplaceF1Or3.transform);
                enemyTurd =Mathf.RoundToInt(enemyTurd - 1.3f);
                Instantiate(ChoseEnemy(enemyTurd), EnemyplaceFL3.transform.position, Quaternion.identity, EnemyplaceFL3.transform);
                break;
            default:
                break;
        }
        StartCoroutine(NewBattle());
    }
    public GameObject ChoseEnemy(int enemyStrenght)
    {
        if(enemyStrenght > 8)
        {
            return Enemy4;
        }
        else if(enemyStrenght > 5)
        {
            return Enemy3;
        }
        else if (enemyStrenght > 2)
        {
            
            return Enemy2;
        }
        else
        {
            
            return Enemy1;
        }
        return null;
    }
    public void ChooseHardBattle()
    {
        IncreaseTA();
        difeculty = 2;
        float hard = 1.4f;
        int enemyAmount = Mathf.RoundToInt(hard * (turnAmount * 1.5f));
        int limet = enemyAmount;
        if (limet > 4)
        {
            limet = 4;
        }
        int rand = Random.Range(1, limet);
        switch (rand)
        {
            case 1:
                Instantiate(ChoseEnemy(enemyAmount), EnemyplaceF1Or3.transform.position, Quaternion.identity, EnemyplaceF1Or3.transform);
                break;
            case 2:
                int enemyHalf = Mathf.RoundToInt(enemyAmount / 1.7f);
                Instantiate(ChoseEnemy(enemyHalf), EnemyplaceFF2.transform.position, Quaternion.identity, EnemyplaceFF2.transform);
                enemyHalf = Mathf.RoundToInt(enemyHalf - 2);
                Instantiate(ChoseEnemy(enemyHalf), EnemyplaceFL2.transform.position, Quaternion.identity, EnemyplaceFL2.transform);
                break;
            case 3:
                int enemyTurd = Mathf.RoundToInt(enemyAmount / 2);
                Instantiate(ChoseEnemy(enemyTurd), EnemyplaceFF3.transform.position, Quaternion.identity, EnemyplaceFF3.transform);
                enemyTurd = Mathf.RoundToInt(enemyTurd - 2);
                Instantiate(ChoseEnemy(enemyTurd), EnemyplaceF1Or3.transform.position, Quaternion.identity, EnemyplaceF1Or3.transform);
                enemyTurd = Mathf.RoundToInt(enemyTurd - 2);
                Instantiate(ChoseEnemy(enemyTurd), EnemyplaceFL3.transform.position, Quaternion.identity, EnemyplaceFL3.transform);
                break;
            default:
                break;
        }
        StartCoroutine(NewBattle());
    }
    public void ChooseBossBattle()
    {
        bossFight = true;
        Map.SetActive(false);   
        Shop.SetActive(false);
        Instantiate(Enemy5, EnemyplaceFF3.transform.position, Quaternion.identity, EnemyplaceFF3.transform);
        Instantiate(Boss, EnemyplaceF1Or3.transform.position, Quaternion.identity, EnemyplaceF1Or3.transform);
        Instantiate(Enemy5, EnemyplaceFL3.transform.position, Quaternion.identity, EnemyplaceFL3.transform);
        StartCoroutine(NewBattle());
    }
}
