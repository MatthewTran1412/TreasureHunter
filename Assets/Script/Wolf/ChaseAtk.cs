using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAtk : StateMachineBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    Transform enemy;
    [SerializeField] float onsight;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemy =GameObject.FindGameObjectWithTag("Enemy").transform;
        agent.speed=6f;
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(enemy.position);
        float distance = Vector3.Distance(enemy.position,animator.transform.position);
        if( distance > onsight)
            animator.SetBool("IsWarn",false);
        if(distance<=2.5f&& distance>=1)
            animator.SetBool("IsPunching",true);
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
