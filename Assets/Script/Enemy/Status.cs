using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public bool CanAttack;
    public float AttackCooldown = 1.0f;
    public bool IsAttacking = false;

    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        float distance = Vector3.Distance(player.position,transform.position);
       if(distance<5)
       {
            CanAttack=true;
           if(CanAttack)
           {
               Punch();
           }
       }
       else 
            CanAttack=false;
    }

    public void Punch()
    {
       IsAttacking = true;
       CanAttack=false;
       StartCoroutine(ResetAttackCooldown());
    }
    IEnumerator ResetAttackCooldown()
    {
       StartCoroutine(ResetAttackBool());
       yield return new WaitForSeconds(AttackCooldown);
       CanAttack=true;
    }
    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        IsAttacking=false;
    }
}
