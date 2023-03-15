using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followenemy : StateMachineBehaviour
{
    HealthBar Playerhb;
    GameObject[] enemy;
    Transform player;
    [SerializeField] float timer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Playerhb=GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBar>();
        player= GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        timer=0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float returns= Vector3.Distance(player.position,animator.transform.position);
        int i=0;
        timer+=Time.deltaTime;
        if(Playerhb.health<=0)
            animator.SetBool("PlayerDead",true);
        while (i<enemy.Length)
        {
            float distance=Vector3.Distance(enemy[i].transform.position,animator.transform.position);
            if(enemy[i].GetComponent<EnemyHealth>().IsBeingAttack==true&&distance<=1.5f)
            {
                if(timer>=5)
                    animator.SetTrigger("IsHeavy");
            }
            if(enemy[i].GetComponent<EnemyHealth>().IsBeingAttack==true&&distance>1.5f)
                animator.SetBool("IsAttack",false);
            else if(enemy[i].GetComponent<EnemyHealth>().HP<=0)
                animator.SetBool("IsAttack",false);
            else if(returns>6)
                animator.SetBool("IsAttack",false);
            i++;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
