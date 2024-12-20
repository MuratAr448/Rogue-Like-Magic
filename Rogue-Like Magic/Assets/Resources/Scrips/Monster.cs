using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    TurnSystem turnSystem;
    Player player;
    public int moves;
    public bool secondMoveActive = false;

    public TMP_Text health;
    public float healthMax;
    public float healthPoints;
    private float healthDisplay;
    private float transitionSpeed = 10;

    private float defense;
    public int attackD;
    private int critChance = 15;

    private float delay;
    SpriteRenderer SpriteRenderer;
    public int dropCoins;
    void Start()
    {
        turnSystem = FindObjectOfType<TurnSystem>();
        player = FindObjectOfType<Player>();
        turnSystem.enemyList.Add(this);
        transitionSpeed = healthMax;
        healthPoints = healthMax;
    }

    void Update()
    {
        Stats();
    }

    private void Stats()
    {
        healthDisplay = Mathf.MoveTowards(healthDisplay, healthPoints, transitionSpeed * Time.deltaTime);
        int displayH = Mathf.RoundToInt(healthDisplay);
        health.text = displayH + "/" + healthMax;
    }

    public int Attack()
    {
        StartCoroutine(AttackTime());
        int dealdamage = attackD;
        int crit = Random.Range(0, 16);
        if (crit >= critChance)
        {
            dealdamage = Mathf.RoundToInt(dealdamage * 1.5f);
            Debug.Log(dealdamage);
        }
        return dealdamage;
    }
    public void SecondMove()
    {
        secondMoveActive = true;
        Debug.Log(secondMoveActive);
    }

    public void TakeDamage()
    {
        StartCoroutine(DamageCount());
    }
    private IEnumerator DamageCount()
    {
        yield return new WaitForSeconds(delay);
        
        yield return new WaitForSeconds(delay);

        if (healthPoints<=0)
        {
            player.coins += dropCoins;
            turnSystem.enemyList.Remove(this);
        }
    }
    private IEnumerator AttackTime()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.color = Color.red;
        for (int i = 0; i < 5; i++)
        {
            transform.position = new Vector2(transform.position.x - Time.deltaTime, transform.position.y);
        }
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < 5; i++)
        {
            transform.position = new Vector2(transform.position.x + Time.deltaTime, transform.position.y);
        }
        SpriteRenderer.color = Color.white;
    }
}
