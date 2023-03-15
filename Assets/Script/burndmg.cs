using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burndmg : MonoBehaviour
{
    public bool beInfire;
    public bool stopdealdmg;
    [SerializeField] HealthBar hb;

    void Update()
    {
        if(beInfire == true)
        {
            if(stopdealdmg == false)
            {
                stopdealdmg=true;
                StartCoroutine(DamageFromFire());
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Fire")
            beInfire = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Fire")
            beInfire = false;
    }
    IEnumerator DamageFromFire()
    {
        yield return new WaitForSeconds(1);
        hb.health-=3;
        stopdealdmg = false;
    }
}
