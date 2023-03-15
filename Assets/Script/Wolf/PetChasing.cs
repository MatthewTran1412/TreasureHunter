using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetChasing : StateMachineBehaviour
{
    HealthBar Playerhb;
    UnityEngine.AI.NavMeshAgent agent;
    Transform player;
    [SerializeField] float warningzone;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
        player =GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed=6f;
        Playerhb=GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBar>();
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Playerhb.health<=0)
            animator.SetBool("PlayerDead",true);
        agent.SetDestination(player.position);
        float distance = Vector3.Distance(player.position,animator.transform.position);
        if( distance <= warningzone)
        {
            animator.SetBool("IsChasing",false);
        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);
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
