using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandHit : MonoBehaviour
{
    public Attack atk;
    public int damage;
    //public GameObject HitParticle;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && atk.IsHeavAttacking)
        {
            if(atk.isBuff)
            {
                other.GetComponent<EnemyHealth>().TakeDMG(damage+damage*60/100);
                //atk.IsHeavAttacking=false;
            }
                
            else
            {
                other.GetComponent<EnemyHealth>().TakeDMG(damage);
                //atk.IsHeavAttacking=false;
            }
        }
        else if(other.tag == "Final" && atk.IsHeavAttacking)
        {
            if(atk.isBuff)
            {
                other.GetComponent<BossHealth>().TakeDMG(damage+damage*30/100);
                //atk.IsHeavAttacking=false;
            }

            else
            {
                other.GetComponent<BossHealth>().TakeDMG(damage);
                //atk.IsHeavAttacking=false;
            }
           // Instantiate(HitParticle,new Vector3(other.transform.position.x,transform.position.y,other.transform.position.z),other.transform.rotation);
        }
        else if(other.tag == "Bandit" && atk.IsHeavAttacking)
        {
            if(atk.isBuff)
            {
                other.GetComponent<EnemyHealth>().TakeDMG(damage+damage*60/100);
                //atk.IsHeavAttacking=false;
            }
            else
            {
                other.GetComponent<EnemyHealth>().TakeDMG(damage);
                //atk.IsHeavAttacking=false;
            }
           // Instantiate(HitParticle,new Vector3(other.transform.position.x,transform.position.y,other.transform.position.z),other.transform.rotation);
        }
    }
}
