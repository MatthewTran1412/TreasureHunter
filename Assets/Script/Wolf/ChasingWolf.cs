using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingWolf : StateMachineBehaviour
{
    Transform enemy;
    NavMeshAgent agent;
    bool isAttacking;
    EnemyHealth enemyhb;
    [SerializeField] float warnsight;
    [SerializeField]float distance;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isAttacking=false;
        agent=animator.GetComponent<NavMeshAgent>();
        agent.speed=6f;
        enemyhb=GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyHealth>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distance=Vector3.Distance(animator.transform.position,enemy.position);
        //if(agent.remainingDistance <= agent.stoppingDistance)
        //    agent.SetDestination(enemy.position);
        if(isAttacking==false)
            agent.SetDestination(enemy.position);
        if(distance>warnsight)
            animator.SetBool("IsChasing",false);
        if(distance<=1.5f&& distance>=1f)  
            isAttacking=true;
        else    isAttacking=false;
        if(isAttacking==true)
            animator.SetTrigger("IsAttack");
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
