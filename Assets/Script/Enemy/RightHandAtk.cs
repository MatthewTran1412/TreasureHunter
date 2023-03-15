using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandAtk : MonoBehaviour
{
    public int damage;
    public bool candeal=true;
    //public GameObject HitParticle;
    void Update()
    {
        if(candeal==false)
            StartCoroutine(Resetdealdmg());
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"&& candeal==true)
        {
            other.GetComponent<HealthBar>().TakeDamage(damage);
           // Instantiate(HitParticle,new Vector3(other.transform.position.x,transform.position.y,other.transform.position.z),other.transform.rotation)
        }
        if(other.tag == "Special"&& candeal==true)
        {
            other.GetComponent<GolemHealth>().TakeDMG(damage*10/100);
           // Instantiate(HitParticle,new Vector3(other.transform.position.x,transform.position.y,other.transform.position.z),other.transform.rotation)
        }
    }
    IEnumerator Resetdealdmg()
    {
        yield return new WaitForSeconds(1);
        candeal=true;
    }
}
