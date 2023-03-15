using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int HP;
    //public Slider healthbar;
    public Animator anim;
    public Attack atk;

    public RectTransform healthTransform;
    private float cacheY;
    private float minValue;
    private float maxValue;
    private int currentHealth;
    private int CurrentHealth
    {
        get{return currentHealth;}
        set{
            currentHealth = value;
            HandleHealth();
        }
    }
    public int maxHealth;
    public Text healthText;
    public Image visualHealth;

    void Start()
    {
        cacheY = healthTransform.position.y;
        maxValue =healthTransform.position.x;
        minValue = healthTransform.position.x - healthTransform.rect.width;
        currentHealth = maxHealth;
    }
    void Update()
    {
        //healthbar.value= HP;
    }
    public void TakeDMG(int damageAmount)
    {
        if(atk.isBuff)
            currentHealth-= (damageAmount-damageAmount*40/100);
        else
            currentHealth -= damageAmount;
        if(HP<=0)
        {
            anim.SetTrigger("Die");
        }
        else if(damageAmount>=30)
        {
            anim.SetTrigger("BigHit");
        }
    }
    private void HandleHealth()
    {
        healthText.text = "Health" + currentHealth;

        float currentXValue = MapValues(currentHealth,0,maxHealth,minValue,maxValue);

        healthTransform.position = new Vector3(currentXValue,cacheY);

        if(currentHealth >maxHealth/2)
        {
            visualHealth.color= new Color32((byte)MapValues(currentHealth,maxHealth/2,maxHealth,255,0),255,0,255);
        }
        else
        {
            visualHealth.color= new Color32(255,(byte)MapValues(currentHealth,0,maxHealth/2,0,255),0,255);
        }
    }
    
    private float MapValues(float x , float inMin,float inMax,float outMin, float outMax)
    {
        return ( x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
