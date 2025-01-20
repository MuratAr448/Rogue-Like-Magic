using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RewardSystem : MonoBehaviour
{
    private int rarity;
    private int chose;
    public List<GameObject> rewardsCommon;
    public List<GameObject> rewardsRare;
    public List<GameObject> rewardsEpic;
    public List<GameObject> showListReward;
    GameManager gameManager;
    [SerializeField] private Canvas canvas;
    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        StartCoroutine(Chose());
    }
    public IEnumerator Chose()
    {
        yield return new WaitForSeconds(0.1f);
        switch (gameManager.difeculty)
        {
            case 0:
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
            case 1:
                showListReward.Add(rewardsRare[Random.Range(0,rewardsRare.Count)]);
                break;
            case 2:
                showListReward.Add(rewardsEpic[Random.Range(0, rewardsEpic.Count)]);
                break;
            default:
                break;
        }

        for (int i = 0; i < 2; i++)
        {
            rarity = Random.Range(0, rewardsRare.Count + rewardsEpic.Count);
            if (rarity < Mathf.RoundToInt((rewardsRare.Count + rewardsEpic.Count) / 3))
            {
                chose = Random.Range(0, rewardsEpic.Count);
                showListReward.Add(rewardsEpic[chose]);
            }
            else
            {
                chose = Random.Range(0, rewardsRare.Count);
                showListReward.Add(rewardsRare[chose]);
            }
        }
        float distX = 0;
        for (int i = 0;i < showListReward.Count;i++)
        {
            distX += Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0.0f)).x;
            Debug.Log(distX);
            Instantiate(showListReward[i],new Vector3(distX*33,70,0),Quaternion.identity, canvas.transform);
        }
    }
    public void RemoveIt(GameObject rewardChosen)
    {
        bool isRare = false;
        for (int i = 0; i < rewardsRare.Count; i++)
        {
            if (rewardChosen == rewardsRare[i])
            {
                rewardsRare.Remove(rewardsRare[chose]);
                isRare = true;
            }
        }
        if (isRare)
        {
            for (int i = 0;i < rewardsEpic.Count;i++)
            {
                if (rewardChosen == rewardsEpic[i])
                {
                    rewardsEpic.Remove(rewardsEpic[chose]);
                }
            }
        }
    }
    public void Noshow()
    {
        int limet = 2;
        for(int i = 0;i<limet ; i++)
        {
            ChosenReward reward = showListReward[i].GetComponent<ChosenReward>();
            if (reward.isRare)
            {
                rewardsRare.Add(reward.gameObject);
            }
            else if (!reward.isRare)
            {
                rewardsEpic.Add(reward.gameObject);
            }
            showListReward.Remove(showListReward[i]);
        }
    }
}
