using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject BulletPrefab;
    public float bulletspeed=10;
    public Attack Atk;
    [SerializeField]public float ManaCost;
    public AudioClip shootsound;
    //[SerializeField]public Animator GunAnimator;

    void Update()
    {
        if(Atk.cannotatk==false && Atk.IsShootingMode==true && Atk.OPMenu.GameIsPaused==false && Atk.allway.Istransform==false&& Atk.Mainmenu.StartGame==false&&DialogManager.GetInstance().dialoguePlaying==false)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(Atk.HB.currentMana>0)
                {
                    Atk.Tpc.timer-=Atk.Tpc.timer;
                    Shoot();
                }
            }
        }
    }
    public void Shoot()
    {
        //GunAnimator.SetTrigger("IsShoot");
        Atk.HB.CostMana(ManaCost);
        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(shootsound);
        var bullet = Instantiate(BulletPrefab,bulletSpawnPoint.position,bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletspeed;
    }
}
