using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]QuestManager QM;
    [SerializeField]PlayerStatus PS;
    [SerializeField]RightHandAtk WP;
    public GameObject damagetextPrefabs;
    [SerializeField] public int HP;
    [SerializeField]public int maxhp;
    public Slider healthbar;
    public GameObject Canvas;
    [SerializeField]GameObject ShowGain;
    [SerializeField]Text ShowGained;
    [SerializeField]GameObject ShowFollowQuest;
    [SerializeField]Text FollowQuest;
    public Animator anim;
    [SerializeField]public int expgain;
    [SerializeField]public float Goldgain;
    [SerializeField]public bool IsBeingAttack;
    [SerializeField] Vector3 Spawnpoint;
    void Start()
    {
        ShowGain.SetActive(false);
        IsBeingAttack=false;
        PS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        HP=maxhp;
        Spawnpoint=transform.position;
    }
    void Update()
    {
        Goldgain=Random.Range(20,40);
        healthbar.value= HP;
        if(PS.WorldUp)
        {
            maxhp+=30;
            WP.damage+=10;
            expgain+=20;
        }
    }
    public void TakeDMG(int damageAmount)
    {
        HP -= damageAmount;
        IsBeingAttack=true;
        Canvas.SetActive(true);
        StartCoroutine(CloseCanvas());
        if(damagetextPrefabs)
            ShowDamageText(damageAmount);
        if(HP<=0)
        {
            ShowGain.SetActive(true);
            ShowGained.text="Exp gained:"+expgain+"\nGold gained:"+Goldgain;
            StartCoroutine(CloseText(ShowGain));
            anim.SetTrigger("Die");
            GetComponent<Collider>().enabled=false;
            IsBeingAttack=false;
            PS.AddExp(expgain);
            PS.AddGold(Goldgain);
            WP.GetComponent<Collider>().enabled=false;
            if(gameObject.tag=="Enemy")
                StartCoroutine(TimetoRespawn());
            else if(gameObject.tag=="Bandit")
                StartCoroutine(Dead());
            if(QM.Quest1==true&&QM.DoingQuest==true&& gameObject.name=="Insect")
            {
                QM.numberhave++;
                if(QM.numberhave<=QM.numberneed)
                {
                    ShowFollowQuest.SetActive(true);
                    FollowQuest.text=QM.numberhave+"/"+QM.numberneed;
                    StartCoroutine(CloseText(ShowFollowQuest));
                }            
            }
            if(QM.Quest2==true&&QM.DoingQuest==true&& gameObject.name=="Mutant")
            {
                QM.numberhave++;
                if(QM.numberhave<=QM.numberneed)
                {
                    ShowFollowQuest.SetActive(true);
                    FollowQuest.text=QM.numberhave+"/"+QM.numberneed;
                    StartCoroutine(CloseText(ShowFollowQuest));
                }
            }
        }
        else if(damageAmount>=30)
        {
            anim.SetTrigger("BigHit");
        }
        else if(damageAmount>=50)
        {
            anim.SetTrigger("Stun");
        }
        else if(damageAmount>10)
        {
            anim.SetTrigger("GetHit");
        }
    }
    void ShowDamageText(int damageAmount)
    {
        var go = Instantiate(damagetextPrefabs,transform.position,Quaternion.identity,transform);
        go.GetComponent<TextMesh>().text=damageAmount.ToString();
    }
    IEnumerator CloseCanvas()
    {
        yield return new WaitForSeconds(10);
        Canvas.SetActive(false);
    }
    IEnumerator DisappearObject()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
    IEnumerator TimetoRespawn()
    {
        yield return new WaitForSeconds(2);
        HP=maxhp;
        WP.GetComponent<Collider>().enabled=true;
        GetComponent<Collider>().enabled=true;
        anim.SetTrigger("IsRespawn");
        Canvas.SetActive(false);
        transform.position=Spawnpoint;
    }
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        Canvas.SetActive(false);
    }
    IEnumerator CloseText(GameObject gameObject)
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
