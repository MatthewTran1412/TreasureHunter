using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField]PlayerStatus PS;
    [SerializeField]BossAttack WP;
    public GameObject damagetextPrefabs;
    [SerializeField] public int HP;
    [SerializeField] public int maxhp;
    public int currentblock=0;
    [SerializeField] private int maxblock;
    public Slider healthbar;
    public Slider blockbar;
    public GameObject Canvas;
    public Animator anim;
    [SerializeField]public int expgain;
    [SerializeField]public float Goldgain;
    [SerializeField]public bool IsBeingAttack;
    [SerializeField]GameObject ShowGain;
    [SerializeField]Text ShowGained;
    [SerializeField] Vector3 Spawnpoint;

    void Start(){
        currentblock=maxblock;
        PS=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        HP=maxhp;
        Spawnpoint=transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        healthbar.value= HP;
        blockbar.value=currentblock;
        if(PS.WorldUp)
        {
            HP*=2;
            maxblock*=2;
            WP.damage+=10;
            expgain*=2;
        }
    }
    public void TakeDMG(int damageAmount)
    {
        IsBeingAttack=true;
        Canvas.SetActive(true);
        StartCoroutine(CloseCanvas());
        if(currentblock>0)
        {
            currentblock-=damageAmount*70/100;
            HP -= damageAmount*30/100;
        }
        else
            HP-=damageAmount;
        if(currentblock<=0){
            anim.SetTrigger("IsStun");
            StartCoroutine(ResetStatus());
        }
        if(damagetextPrefabs)
            ShowDamageText(damageAmount);
        if(HP<=0)
        {
            ShowGain.SetActive(true);
            ShowGained.text="Exp gained:"+expgain+"\nGold gained:"+Goldgain;
            StartCoroutine(CloseText());
            anim.SetTrigger("Die");
            GetComponent<Collider>().enabled=false;
            IsBeingAttack=false;
            PS.AddExp(expgain);
            PS.AddGold(Goldgain);
            WP.GetComponent<Collider>().enabled=false;
            StartCoroutine(TimetoRespawn());
        }
    }
    void ShowDamageText(int damageAmount)
    {
        var go = Instantiate(damagetextPrefabs,transform.position,Quaternion.identity,transform);
        go.GetComponent<TextMesh>().text=damageAmount.ToString();
    }
    IEnumerator ResetStatus(){
        yield return new WaitForSeconds(5.0f);
        anim.SetTrigger("IsAngry");
        currentblock=maxblock;
    }
    IEnumerator CloseCanvas()
    {
        yield return new WaitForSeconds(2);
        Canvas.SetActive(false);
    }
    IEnumerator TimetoRespawn()
    {
        yield return new WaitForSeconds(180);
        HP=maxhp;
        WP.GetComponent<Collider>().enabled=true;
        GetComponent<Collider>().enabled=true;
        anim.SetTrigger("IsRespawn");
        Canvas.SetActive(false);
        transform.position=Spawnpoint;
    }
    IEnumerator CloseText()
    {
        yield return new WaitForSeconds(2);
        ShowGain.SetActive(false);
    }
}
