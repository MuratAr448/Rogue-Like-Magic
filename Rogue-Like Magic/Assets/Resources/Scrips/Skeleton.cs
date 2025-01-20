using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour
{
    Player player;
    public int damage;

    [SerializeField] private TMP_Text health;
    public float healthMax;
    public float healthPoints;
    private float healthDisplay;

    private float delay = 0.5f;
    private float transitionSpeed;
    public GameObject damageText;
    private Text takedamageText;
    [SerializeField] private UnityEngine.Transform textTransform;
    void Start()
    {
        player = FindObjectOfType<Player>();
        transitionSpeed = healthMax;
        healthPoints = healthMax;
    }

    void Update()
    {
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
    public IEnumerator GetSkeletonHurt(int damageTaken)
    {
        healthPoints -= damageTaken;

        GameObject temp = Instantiate(damageText, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity, textTransform);
        takedamageText = temp.GetComponentInChildren<Text>();
        takedamageText.text = "-" + damageTaken;
        yield return new WaitForSeconds(delay);
        Destroy(temp);
        if (healthPoints <= 0)
        {
            player.skeletonActive = false;
            Destroy(gameObject,1);
        }
    }
}
