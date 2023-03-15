using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP= 100;
    public Animator anim;

    public void TakeDMG(int damageAmount)
    {
        HP -= damageAmount;
        if(HP<=0)
        {
            anim.SetTrigger("Die");
        }
        else
        {
            anim.SetTrigger("GetHit");
        }
    }


}
