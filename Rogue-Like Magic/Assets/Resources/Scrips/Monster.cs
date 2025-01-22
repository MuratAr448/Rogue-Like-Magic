using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public List<bool> elementW;
    public bool isSummond;
    private int timeOfDeath;

    CastSlot castSlot;
    public TurnSystem turnSystem;
    public Player player;
    public GameManager gameManager;

    public GameObject damageText;
    private Text takedamageText;
    private float damagetextY = 1;
    [SerializeField] private UnityEngine.Transform textTransform;

    public int moves;
    public bool secondMoveActive = false;
    public bool secondMoveAble;
    public int secondMoveCooldown = 0;

    [SerializeField] private TMP_Text health;
    public float healthMax;
    public float healthPoints;
    private float healthDisplay;
    private float transitionSpeed = 10;

    public int attackD;
    private int critChance = 15;

    TargetMonster TargetMonster;
    private int attackBoostCooldown = -1;
    private int defenceBoostCooldown = -1;

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
        gameManager = FindObjectOfType<GameManager>();
        turnSystem.enemyList.Add(this);
        transitionSpeed = healthMax;
        healthPoints = healthMax;
        SpriteRenderer = GetComponent<SpriteRenderer>();
        timeOfDeath = turnSystem.turns + 3;
        secondMoveAble = true;
    }
    public void OnMouseOver()
    {
        if(castSlot.spellSlot.transform.childCount != 0)
        {
            if (Input.GetMouseButtonDown(0) && castSlot.spellSlot.transform.GetChild(0).GetComponent<OffenseSpell>())
            {
                TargetMonster.target = gameObject;
                TargetMonster.SetTarget();//able to be targeted
                Debug.Log("good");
            }
        }
    }

    void Update()
    {
        if (isSummond)
        {
            if (timeOfDeath < turnSystem.turns)
            {
                turnSystem.enemyList.Remove(this);
                Destroy(gameObject,1);
            }
        }
        Stats();
    }

    private void Stats()
    {
        if (healthPoints >= healthMax)
        {
            healthPoints = healthMax;
        }//hp gaat niet boven max 
        healthDisplay = Mathf.MoveTowards(healthDisplay, healthPoints, transitionSpeed * Time.deltaTime);
        int displayH = Mathf.RoundToInt(healthDisplay);
        health.text = displayH + "/" + healthMax;
        //animated hp going up or down
    }

    public int Attack()
    {
        StartCoroutine(AttackTime());
        if (secondMoveCooldown<=turnSystem.turns)
        {
            secondMoveActive = false;
        }
        int dealdamage = attackD;
        if (attackBoostCooldown>=turnSystem.turns)
        {
            dealdamage = Mathf.RoundToInt(dealdamage * 1.5f);
        }
        
        int crit = Random.Range(0, 16);
        if (crit >= critChance)
        {
            dealdamage = Mathf.RoundToInt(dealdamage * 1.5f);
            Debug.Log(dealdamage);//chance to crit
        }
        return dealdamage;//deel damage
    }
    public void SecondMove()
    {
        DoSecondMoveAble();
        if (secondMoveAble)
        {
            secondMoveActive = true;
            secondMoveCooldown = 3 + turnSystem.turns;//second move cooldown
            DoSecondMove();
        }
        else
        {
            if (player.skeletonActive)
            {
                Skeleton skeleton = FindObjectOfType<Skeleton>();
                StartCoroutine(skeleton.GetSkeletonHurt(Attack()));
            }
            else
            if (player.shieldOn)
            {
                StartCoroutine(player.GetShieldHurt(Attack()));
            }
            else
            {
                StartCoroutine(player.GetHurt(Attack()));
            }
        }
    }
    protected virtual void DoSecondMove()
    {

    }
    protected virtual void DoSecondMoveAble()
    {

    }
    public void GetAttackBoost()
    {
        attackBoostCooldown = turnSystem.turns+3;
    }
    public void GetDefenceBoost()
    {
        defenceBoostCooldown = turnSystem.turns+3;
    }
    public void TakeDamageSkeleton(int takendamage)
    {
        int damage = takendamage;
        int crit = Random.Range(0, 16);
        if (crit >= critChance)
        {
            damage = Mathf.RoundToInt(damage * 1.5f);
            Debug.Log(damage);//chance to crit
        }
        StartCoroutine(DamageCount(damage));
    }
    public void TakeDamage(int takendamage)
    {
        int damage;
        if (WeaknessCheck()) 
        {
            damage = Mathf.RoundToInt(takendamage * 1.5f);//do more damage if super effectif
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
        if (castSlot.spellSlot.transform.GetChild(0).GetComponent<OffenseSpell>() == true)
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
        else
        {
            return false;
        }
    }
    private IEnumerator DamageCount(int takendamage)
    {
        if (defenceBoostCooldown>=turnSystem.turns)
        {
            takendamage /= 2;
        }
        healthPoints -= takendamage;
        if (healthPoints < 0)
        {
            healthPoints = 0;
        }
        
        GameObject temp = Instantiate(damageText, new Vector3(transform.position.x, transform.position.y + damagetextY, transform.position.z), Quaternion.identity, textTransform);
        takedamageText = temp.GetComponentInChildren<Text>();
        takedamageText.text = "-" + takendamage;//show how much hp it lost

        SpriteRenderer.color = Color.red;

        yield return new WaitForSeconds(delay);

        Destroy(temp);
        SpriteRenderer.color = Color.white;
        
        if (healthPoints <= 0)
        {
            player.coins += dropCoins;//loot
            turnSystem.enemyList.Remove(this);//dead
            yield return new WaitForSeconds(delay);
            Destroy(gameObject,1f);
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
