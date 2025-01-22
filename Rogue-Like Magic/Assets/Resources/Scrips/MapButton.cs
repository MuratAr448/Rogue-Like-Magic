using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    private Button Button;
    GameManager gameManager;
    [SerializeField] private int number;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Button = GetComponent<Button>();
        switch (number)
        {
            case 0:
                Button.onClick.AddListener(gameManager.ChooseShop);
                break;
            case 1:
                Button.onClick.AddListener(gameManager.ChooseNormalBattle);
                break;
            case 2:
                Button.onClick.AddListener(gameManager.ChooseHardBattle);
                break;
            case 3:
                Button.onClick.AddListener(gameManager.ChooseBossBattle);
                break;
            default:
                break;
        }
    }
}
