using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetPatrol : StateMachineBehaviour
{
    Transform player;
    HealthBar Playerhb;
    [SerializeField]float safezone;
    [SerializeField] float warningzone;
    UnityEngine.AI.NavMeshAgent agent;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = 1f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Playerhb=GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBar>();
        agent.SetDestination(player.position);
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Playerhb.health<=0)
            animator.SetBool("PlayerDead",true);
        float distance = Vector3.Distance(player.position,animator.transform.position);
        if(agent.remainingDistance <= agent.stoppingDistance)
            agent.SetDestination(player.position);
        if(distance < safezone)
            animator.SetBool("isPatrolling",false);
        if( distance > warningzone)
            animator.SetBool("IsChasing",true);
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }


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
