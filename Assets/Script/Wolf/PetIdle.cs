using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetIdle : StateMachineBehaviour
{
    float timer;
    GameObject player;
    GameObject[] enemy;
    [SerializeField]float safezone;
    [SerializeField] float atkarea;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer=0;
        player = GameObject.FindGameObjectWithTag("Player");
        enemy=GameObject.FindGameObjectsWithTag("Enemy");
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int i=0;
        if(player.GetComponent<HealthBar>().health<=0)
            animator.SetBool("PlayerDead",true);
        timer+= Time.deltaTime;
        //counting+=Time.deltaTime;
        float distance = Vector3.Distance(player.transform.position,animator.transform.position);
        while (i<enemy.Length)
        {
            float enemydistance=Vector3.Distance(enemy[i].transform.position,animator.transform.position);
            if(enemy[i].GetComponent<EnemyHealth>().IsBeingAttack==true&&enemydistance<atkarea)
                animator.SetBool("IsWarn",true);
            i++;
        }
        if(timer>5)
        {
            timer-=timer;
            animator.SetTrigger("IsRelax");
        }
        if( distance > safezone)
            animator.SetBool("isPatrolling",true);
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
