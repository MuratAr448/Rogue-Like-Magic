using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //public Slider health;
    //public Slider mana;

    TurnSystem turnSystem;

    [SerializeField] private TMP_Text health;
    public float healthMax;
    public float healthPoints;
    private float healthDisplay;

    [SerializeField] private TMP_Text mana;
    public float manaMax;
    public float manaPoints;
    private float manaDisplay;

    public float coins;
    private float transitionSpeed = 10;
    private float delay = 0.5f;

    SpriteRenderer spriteRenderer;
    public GameObject damageText;
    private Text takedamageText;
    [SerializeField] private UnityEngine.Transform textTransform;

    [SerializeField] private GameObject Shield;
    [SerializeField] private TMP_Text shield;
    public float ShieldhealthMax;
    public float ShieldhealthPoints;
    private float ShieldhealthDisplay;

    public bool shieldOn = false;
    public bool skeletonActive = false;

    private void Start()
    {
        turnSystem = FindObjectOfType<TurnSystem>();
        transitionSpeed = healthMax;
        healthPoints = healthMax;
    }
    private void Update()
    {
        Stats();
    }
    private void Stats()
    {
        if (healthPoints>=healthMax)
        {
            healthPoints = healthMax;
        }//hp gaat niet boven max
        healthDisplay = Mathf.MoveTowards(healthDisplay, healthPoints, transitionSpeed * Time.deltaTime);
        int displayH = Mathf.RoundToInt(healthDisplay);
        health.text = displayH + "/" + healthMax;
        //animated hp going up or down
        if (manaPoints>=manaMax)
        {
            manaPoints = manaMax;
        }//mp gaat niet boven max
        manaDisplay = Mathf.MoveTowards(manaDisplay, manaPoints, transitionSpeed * Time.deltaTime);
        int displayM = Mathf.RoundToInt(manaDisplay);
        mana.text = displayM + "/" + manaMax;
        //animated mp going up or down
        if (shieldOn)
        {
            Shield.SetActive(true);

            ShieldhealthDisplay = Mathf.MoveTowards(ShieldhealthDisplay, ShieldhealthPoints, transitionSpeed * Time.deltaTime);
            int displayS = Mathf.RoundToInt(ShieldhealthDisplay);
            shield.text = displayS + "/" + ShieldhealthMax;
            //animated Shield going up or down
        }
        else
        {
            Shield.SetActive(false);
        }
        if (skeletonActive)
        {
            transform.position = new Vector3(-7,0,0);
        }
        else
        {
            transform.position = new Vector3(-5, 0, 0);
        }
    }
    public IEnumerator GetShieldHurt(int damageTaken)
    {
        ShieldhealthPoints -= damageTaken;

        GameObject temp = Instantiate(damageText, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity, textTransform);
        takedamageText = temp.GetComponentInChildren<Text>();
        takedamageText.text = "-" + damageTaken;
        yield return new WaitForSeconds(delay);
        Destroy(temp);
        if (ShieldhealthPoints<=0)
        {
            shieldOn = false;
        }
    }
    public IEnumerator GetHurt(int damageTaken)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.green;

        healthPoints -= damageTaken; //take damage
        
        GameObject temp = Instantiate(damageText,new Vector3(transform.position.x, transform.position.y + 2, transform.position.z),Quaternion.identity, textTransform);
        takedamageText = temp.GetComponentInChildren<Text>();
        takedamageText.text = "-" + damageTaken;//show how much hp you lost

        yield return new WaitForSeconds(0.3f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(delay);
        Destroy(temp);
        if (healthPoints <= 0)
        {
            SceneManager.LoadScene("Start");
        }
    }
}
