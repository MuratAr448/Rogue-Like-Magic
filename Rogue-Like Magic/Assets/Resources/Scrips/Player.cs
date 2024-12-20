using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Player : MonoBehaviour
{
    //public Slider health;
    //public Slider mana;

    TurnSystem turnSystem;


    public TMP_Text health;
    public float healthMax;
    public float healthPoints;
    private float healthDisplay;

    public TMP_Text mana;
    public float manaMax;
    public float manaPoints;
    private float manaDisplay;

    public float coins;
    public List<GameObject> items;
    private float transitionSpeed = 10;
    private float delay = 1.0f;

    SpriteRenderer spriteRenderer;
    public GameObject damageText;
    private Text takedamageText;
    [SerializeField] private UnityEngine.Transform textTransform;
    private void Start()
    {
        turnSystem = FindObjectOfType<TurnSystem>();
        transitionSpeed = healthMax;
        healthPoints = healthMax;
    }
    /*health = GetComponent<Slider>();
        mana = GetComponent<Slider>();

        health.maxValue = healthMax;
        mana.maxValue = manaMax;
        mana.minValue = -5;
     */
    private void Update()
    {
        Stats();
    }
    private void Stats()
    {
        if (healthPoints>=healthMax)
        {
            healthPoints = healthMax;
        }

        healthDisplay = Mathf.MoveTowards(healthDisplay, healthPoints, transitionSpeed * Time.deltaTime);
        int displayH = Mathf.RoundToInt(healthDisplay);
        health.text = displayH + "/" + healthMax;

        if (manaPoints>=manaMax)
        {
            manaPoints = manaMax;
        }
        manaDisplay = Mathf.MoveTowards(manaDisplay, manaPoints, transitionSpeed * Time.deltaTime);
        int displayM = Mathf.RoundToInt(manaDisplay);
        mana.text = displayM + "/" + manaMax;

    }
    public IEnumerator GetHurt(int damageTaken)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.green;
        healthPoints -= damageTaken;
        float damagetextY = 2;
        GameObject temp = Instantiate(damageText,new Vector3(transform.position.x, transform.position.y + damagetextY, transform.position.z),Quaternion.identity, textTransform);
        takedamageText = temp.GetComponentInChildren<Text>();
        takedamageText.text = "-" + damageTaken;
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(delay);
        Destroy(temp);
        if (healthPoints <= 0)
        {
            //dead
        }
    }
}
