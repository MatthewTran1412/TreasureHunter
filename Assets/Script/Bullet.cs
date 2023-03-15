using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;
    public int damage;
    public GameObject normaleffect;
    public GameObject explosioneffect;
    public Attack atk;
    void Awake(){
        Destroy(gameObject,life);
    }
    void Start()
    {
        atk= GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            if(atk.isBuff)
            {
                other.GetComponent<EnemyHealth>().TakeDMG(damage+damage*60/100);
                normaleffect.SetActive(false);
                explosioneffect.SetActive(true);
                transform.GetComponent<Rigidbody>().velocity=Vector3.zero;
                StartCoroutine(WaitforDestroy());
            }

            else
            {
                other.GetComponent<EnemyHealth>().TakeDMG(damage);
                normaleffect.SetActive(false);
                explosioneffect.SetActive(true);
                transform.GetComponent<Rigidbody>().velocity=Vector3.zero;
                StartCoroutine(WaitforDestroy());
            }
        }
        else if(other.tag == "Final")
        {
            if(atk.isBuff)
            {
                other.GetComponent<BossHealth>().TakeDMG(damage+damage*30/100);
                normaleffect.SetActive(false);
                explosioneffect.SetActive(true);
                transform.GetComponent<Rigidbody>().velocity=Vector3.zero;
                StartCoroutine(WaitforDestroy());
            }
            else
            {
                other.GetComponent<BossHealth>().TakeDMG(damage);
                normaleffect.SetActive(false);
                explosioneffect.SetActive(true);
                transform.GetComponent<Rigidbody>().velocity=Vector3.zero;
                StartCoroutine(WaitforDestroy());
            }
           // Instantiate(HitParticle,new Vector3(other.transform.position.x,transform.position.y,other.transform.position.z),other.transform.rotation);
        }
        else if(other.tag == "Bandit")
        {
            if(atk.isBuff)
            {
                other.GetComponent<EnemyHealth>().TakeDMG(damage+damage*60/100);
                normaleffect.SetActive(false);
                explosioneffect.SetActive(true);
                transform.GetComponent<Rigidbody>().velocity=Vector3.zero;
                StartCoroutine(WaitforDestroy());
            }
            else
            {
                other.GetComponent<EnemyHealth>().TakeDMG(damage);
                normaleffect.SetActive(false);
                explosioneffect.SetActive(true);
                transform.GetComponent<Rigidbody>().velocity=Vector3.zero;
                StartCoroutine(WaitforDestroy());
            }
           // Instantiate(HitParticle,new Vector3(other.transform.position.x,transform.position.y,other.transform.position.z),other.transform.rotation);
        }
        else if(other.tag =="Ground")
        {
            normaleffect.SetActive(false);
            explosioneffect.SetActive(true);
            transform.GetComponent<Rigidbody>().velocity=Vector3.zero;
            StartCoroutine(WaitforDestroy());
        }
    }
    IEnumerator WaitforDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
