using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public List<bool> elementW;

    CastSlot castSlot;
    TurnSystem turnSystem;
    Player player;

    public GameObject damageText;
    private Text takedamageText;
    [SerializeField] private UnityEngine.Transform textTransform;

    public int moves;
    public bool secondMoveActive = false;
    public int secondMoveCooldown = 0;

    public TMP_Text health;
    public float healthMax;
    public float healthPoints;
    private float healthDisplay;
    private float transitionSpeed = 10;

    public float defense;
    public int attackD;
    private int critChance = 15;

    TargetMonster TargetMonster;

    private float delay = 1.5f;
    SpriteRenderer SpriteRenderer;
    public int dropCoins;
    void Start()
    {
        textTransform = GetComponentInChildren<RectTransform>();
        TargetMonster = FindObjectOfType<TargetMonster>();
        turnSystem = FindObjectOfType<TurnSystem>();
        castSlot = FindObjectOfType<CastSlot>();
        player = FindObjectOfType<Player>();
        turnSystem.enemyList.Add(this);
        transitionSpeed = healthMax;
        healthPoints = healthMax;
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void OnMouseOver()
    {
        if(castSlot.spellSlot.transform.childCount != 0)
        {
            if (Input.GetMouseButtonDown(0) && castSlot.spellSlot.transform.GetChild(0).GetComponent<OffenseSpell>())
            {
                TargetMonster.target = gameObject;
                TargetMonster.SetTarget();
                Debug.Log("good");
            }
        }
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
        if (secondMoveCooldown<=turnSystem.turns)
        {
            secondMoveActive = false;
        }
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
        secondMoveCooldown = 3 + turnSystem.turns;
        Debug.Log(secondMoveActive);
    }

    public void TakeDamage(int takendamage)
    {
        int damage;
        if (WeaknessCheck()) 
        {
            damage = Mathf.RoundToInt(takendamage * 1.5f);
        }
        else
        {
            damage = takendamage;
        }
        Debug.Log("My damage:"+ damage);
        StartCoroutine(DamageCount(damage));
    }
    private bool WeaknessCheck()
    {

        int check = (int)castSlot.OffenseSpell.elements;
        if (elementW[check] == true)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    private IEnumerator DamageCount(int takendamage)
    {
        healthPoints -= takendamage;
        if (healthPoints < 0)
        {
            healthPoints = 0;
        }
        float damagetextY = 1;
        GameObject temp = Instantiate(damageText, new Vector3(transform.position.x, transform.position.y + damagetextY, transform.position.z), Quaternion.identity, textTransform);
        takedamageText = temp.GetComponentInChildren<Text>();
        takedamageText.text = "-" + takendamage;
        
        SpriteRenderer.color = Color.red;

        yield return new WaitForSeconds(delay);

        Destroy(temp);
        SpriteRenderer.color = Color.white;

        yield return new WaitForSeconds(delay);
        
        if (healthPoints <= 0)
        {
            player.coins += dropCoins;
            turnSystem.enemyList.Remove(this);
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }
    }
    private IEnumerator AttackTime()
    {
        SpriteRenderer.color = Color.green;
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
