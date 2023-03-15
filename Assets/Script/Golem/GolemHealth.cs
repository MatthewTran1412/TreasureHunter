using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolemHealth : MonoBehaviour
{
    [SerializeField]PlayerStatus PS;
    [SerializeField]GolemAtk Fist;
    public GameObject damagetextPrefabs;
    [SerializeField] public int HP;
    public Slider healthbar;
    public GameObject Canvas;
    public Animator anim;
    [SerializeField]public int expgain;
    [SerializeField]public bool IsBeingAttack=false;
    void Start()
    {
        PS=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }
    void Update()
    {
        healthbar.value= HP;
        if(PS.WorldUp)
        {
            HP+=30;
            Fist.damage+=10;
            expgain+=20;
        }
    }
    public void TakeDMG(int damageAmount)
    {
        HP -= damageAmount;
        IsBeingAttack=true;
        Canvas.SetActive(true);
        if(damagetextPrefabs)
            ShowDamageText(damageAmount);
        if(HP<=0)
        {
            anim.SetTrigger("Die");
            GetComponent<Collider>().enabled=false;
            PS.AddExp(expgain);

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
        yield return new WaitForSeconds(2);
        Canvas.SetActive(false);
    }
}
