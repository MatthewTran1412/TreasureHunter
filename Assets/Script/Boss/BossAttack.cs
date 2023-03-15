using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float damage;
    [SerializeField]BossMusic BS;
    //public GameObject HitParticle;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && BS.CanAttack)
        {
            other.GetComponent<HealthBar>().TakeDamage(damage);
            BS.CanAttack=false;
           // Instantiate(HitParticle,new Vector3(other.transform.position.x,transform.position.y,other.transform.position.z),other.transform.rotation)
        }
    }
}
