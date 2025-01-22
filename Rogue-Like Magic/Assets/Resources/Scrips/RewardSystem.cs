using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RewardSystem : MonoBehaviour
{
    private int rarity;
    private int chose;

    public List<GameObject> rewardR;
    public List<GameObject> rewardE;

    public List<GameObject> rewardsCommon;
    public List<GameObject> rewardsRare;
    public List<GameObject> rewardsEpic;
    public List<GameObject> showListReward;
    [SerializeField] private List<GameObject> DestroyRewardList;
    GameManager gameManager;
    [SerializeField] private Canvas canvas;
    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        for (int i = 0; i<rewardE.Count; i++)
        {
            rewardsEpic.Add(rewardE[i]);
        }
        for (int i = 0; i <  rewardR.Count; i++)
        {
            rewardsRare.Add(rewardR[i]);
        }
    }
    public IEnumerator Chose()
    {
        yield return new WaitForSeconds(0.1f);
        switch (gameManager.difeculty)
        {
            case 1:
                switch (Random.Range(0, 2))
                {
                    case 0:
                        showListReward.Add(rewardsCommon[0]);
                        break;
                    case 1:
                        showListReward.Add(rewardsCommon[1]);
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                showListReward.Add(rewardsRare[Random.Range(0,rewardsRare.Count)]);
                break;
            case 3:
                showListReward.Add(rewardsEpic[Random.Range(0, rewardsEpic.Count)]);
                break;
            default:
                break;
        }
        for (int i = 0; i < 2; i++)
        {
            RewardCheck();
        }
        float distX = 0;
        float distY = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y * 100; ;
        for (int i = 0;i < showListReward.Count;i++)
        {
            distX += Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0.0f)).x;
            DestroyRewardList.Add(Instantiate(showListReward[i],new Vector3(distX*55, distY, 0),Quaternion.identity, canvas.transform));
        }
    }
    private void RewardCheck()
    {
        rarity = Random.Range(0, rewardsRare.Count + rewardsEpic.Count);
        if (rarity < Mathf.RoundToInt((rewardsRare.Count + rewardsEpic.Count) / 3))
        {
            chose = Random.Range(0, rewardsEpic.Count);
            if (!showListReward.Contains(rewardsEpic[chose]))
            {
                showListReward.Add(rewardsEpic[chose]);
            }
            else
            {
                RewardCheck();
            }
        }
        else
        {
            chose = Random.Range(0, rewardsRare.Count);
            if (!showListReward.Contains(rewardsRare[chose]))
            {
                showListReward.Add(rewardsRare[chose]);
            }
            else
            {
                RewardCheck();
            }
        }
    }
    public void RemoveIt(bool isRare, GameObject rewardChosen)
    {
        if (rewardChosen.GetComponent<StatReward>())
        {

        }
        else
        {
            if (isRare)
            {
                for (int i = 0; i < rewardsRare.Count; i++)
                {
                    if (rewardChosen == rewardsRare[i])
                    {
                        rewardsRare.Remove(rewardsRare[i]);
                    }
                }
            }
            else if (!isRare)
            {
                for (int i = 0; i < rewardsEpic.Count; i++)
                {
                    if (rewardChosen == rewardsEpic[i])
                    {
                        rewardsEpic.Remove(rewardsEpic[i]);
                    }
                }
            }
        }
        showListReward.Remove(rewardChosen);
    }
    public void Noshow()
    {
        for(int i = 0;i < showListReward.Count; i++)
        {
            Destroy(DestroyRewardList[i]);
        }
        DestroyRewardList.Clear();
        showListReward.Clear();
    }
}
