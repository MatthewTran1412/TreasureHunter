using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField]public Transform RespawnPoint;
    public float health;
    private float lerpTimer;
    public float maxHealth = 100;
    public float chipSpeed = 2f;
    public Image frontHB;
    public Image BackHB;
    public Image frontSta;
    public Image BackSta;
    public Image frontMana;
    public Image BackMana;
    [SerializeField]MainMenu Mainmenu;
    [SerializeField]AllwayShow allway;
    [SerializeField]TPC tpc;
    [SerializeField]GameObject DeathScreen;
    [SerializeField]GameObject BeHitEffect;
    
    [SerializeField]Animator anim;
    [SerializeField]Attack atk;
    public int medkid;
    [SerializeField]float heal;
    public bool healling=false;
    public float currentstamina=0;
    public float maxstamina=100;
    public bool recharge=true;
    public float currentMana=0;
    public float maxMana=100;
    [SerializeField]public float percentofheal;
    
    // Start is called before the first frame update
    void Start()
    {
        health=maxHealth;
        medkid=2;
        currentstamina=maxstamina;
        currentMana=maxMana;
    }

    // Update is called once per frame
    void Update()
    {
        percentofheal=health*100/maxHealth;
        if(percentofheal<=30)
            BeHitEffect.GetComponent<Animator>().SetTrigger("LowHealth");
        else if(percentofheal>30)
            BeHitEffect.GetComponent<Animator>().SetTrigger("FullHealth");
        if(allway.Istransform==false||Mainmenu.StartGame==false)
        {
            health = Mathf.Clamp(health,0,maxHealth);
            UpdateHealthUI();
            UpdateStaminaUI();
            UpdateManaUI();
            if(Input.GetKeyDown(KeyCode.H))
            {
                if(medkid>0)
                    AidBag(heal);
            }
        }

    }
    public void UpdateHealthUI()
    {
        float fillF = frontHB.fillAmount;
        float fillB = BackHB.fillAmount;
        float hFrantion=health/maxHealth;
        if(fillB>hFrantion)
        {
            frontHB.fillAmount = hFrantion;
            BackHB.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer/chipSpeed;
            percentComplete=percentComplete*percentComplete;
            BackHB.fillAmount = Mathf.Lerp(fillB,hFrantion,percentComplete);
        }
        if(fillF<hFrantion)
        {
            BackHB.color = Color.green;
            BackHB.fillAmount=hFrantion;
            lerpTimer+=Time.deltaTime;
            float percentComplete=lerpTimer/chipSpeed;
            percentComplete=percentComplete*percentComplete;
            frontHB.fillAmount = Mathf.Lerp(fillF,BackHB.fillAmount,percentComplete);
        }
    }
    public void UpdateStaminaUI()
    {
        float fillF = frontSta.fillAmount;
        float fillB = BackSta.fillAmount;
        float hFrantion=currentstamina/maxstamina;
        if(fillB>hFrantion)
        {
            frontSta.fillAmount = hFrantion;
            BackSta.color = Color.white;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer/chipSpeed;
            percentComplete=percentComplete*percentComplete;
            BackSta.fillAmount = Mathf.Lerp(fillB,hFrantion,percentComplete);
        }
        if(fillF<hFrantion)
        {
            BackSta.color = Color.blue;
            BackSta.fillAmount=hFrantion;
            lerpTimer+=Time.deltaTime;
            float percentComplete=lerpTimer/chipSpeed;
            percentComplete=percentComplete*percentComplete;
            frontSta.fillAmount = Mathf.Lerp(fillF,BackSta.fillAmount,percentComplete);
        }
    }
    public void UpdateManaUI()
    {
        float fillF = frontMana.fillAmount;
        float fillB = BackMana.fillAmount;
        float hFrantion=currentMana/maxMana;
        if(fillB>hFrantion)
        {
            frontMana.fillAmount = hFrantion;
            BackMana.color = Color.white;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer/chipSpeed;
            percentComplete=percentComplete*percentComplete;
            BackMana.fillAmount = Mathf.Lerp(fillB,hFrantion,percentComplete);
        }
        if(fillF<hFrantion)
        {
            BackMana.color = Color.blue;
            BackMana.fillAmount=hFrantion;
            lerpTimer+=Time.deltaTime;
            float percentComplete=lerpTimer/chipSpeed;
            percentComplete=percentComplete*percentComplete;
            frontMana.fillAmount = Mathf.Lerp(fillF,BackMana.fillAmount,percentComplete);
        }
    }
    public void TakeDamage(float damage)
    {
        if(atk.OPMenu.GameIsPaused==false && atk.allway.Istransform==false&&atk.Mainmenu.StartGame==false)
       { 
        tpc.timer-=tpc.timer;
        if(atk.isBuff)
            health-= damage - damage *30/100;
        else
            health -= damage;
        lerpTimer=0f;
        if(health<=0)
        {
            anim.SetBool("Die",true);
            GetComponent<BoxCollider>().enabled=false;
            DeathScreen.SetActive(true);
            atk.islockmouse=false;
            atk.cannotatk=true;
            atk.CanMove=false;
        }
        else if(damage >=30)
            anim.SetTrigger("BigHit");
       }
    }
    public void AidBag(float heal)
    {
        if(percentofheal>=60/100)
            health+=heal;
        else if(percentofheal>=40/100)
            health+=heal*2;
        else if((percentofheal)<40/100)
            health+=heal*3;
        medkid--;
        lerpTimer = 0f;
    }
    public void Relax()
    {
        if(health<maxHealth && healling==true)
            health+=Time.deltaTime*2;
        anim.SetBool("Relax",true);
    }
    public void CostStamina(float CostSta)
    {
        recharge=false;
        currentstamina-=CostSta;
        StartCoroutine(WaitForRecharge());
    }
    public void CostMana(float CostMan)
    {
        recharge=false;
        currentMana-=CostMan;
        StartCoroutine(WaitForRecharge());
    }
    public void RechargeStamina()
    {
        if(currentstamina<maxstamina && recharge==true)
            currentstamina+=Time.deltaTime*3;
        if(currentMana<maxMana && recharge==true)
            currentMana+=Time.deltaTime*3;
    }
    IEnumerator WaitForRecharge()
    {
        yield return new WaitForSeconds(5.0f);
        recharge=true;
    }
    public void Respawn()
    {
        StartCoroutine(WaitforRespawn());
    }
    IEnumerator WaitforRespawn()
    {
        yield return new WaitForSeconds(2);
        allway.LoadScreen();
        transform.position=RespawnPoint.position;
        health=maxHealth;
        medkid=2;
        currentstamina=maxstamina;
        GetComponent<BoxCollider>().enabled=true;
        atk.cannotatk=false;
        atk.CanMove=true;
        anim.SetBool("Die",true);
    }
}
