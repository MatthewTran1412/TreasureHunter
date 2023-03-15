using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAtk : MonoBehaviour
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
        if(other.tag == "Enemy"&& candeal==true && other.GetComponent<EnemyHealth>().IsBeingAttack==true)
        {
            other.GetComponent<EnemyHealth>().TakeDMG(damage);
        }
        else if(other.tag == "Final"&& candeal==true && other.GetComponent<BossHealth>().IsBeingAttack==true)
        {
            other.GetComponent<BossHealth>().TakeDMG(damage-damage*20/100);
        }
    }
    IEnumerator Resetdealdmg()
    {
        yield return new WaitForSeconds(1);
        candeal=true;
    }
}
