using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAtk : MonoBehaviour
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
        if(other.tag == "Enemy"&& candeal==true)
        {
            other.GetComponent<EnemyHealth>().TakeDMG(damage*10/100);
           // Instantiate(HitParticle,new Vector3(other.transform.position.x,transform.position.y,other.transform.position.z),other.transform.rotation)
        }
        else if(other.tag == "Bandit"&& candeal==true)
        {
            other.GetComponent<EnemyHealth>().TakeDMG(damage*10/100);
           // Instantiate(HitParticle,new Vector3(other.transform.position.x,transform.position.y,other.transform.position.z),other.transform.rotation)
        }
    }
    IEnumerator Resetdealdmg()
    {
        yield return new WaitForSeconds(1);
        candeal=true;
    }
}
