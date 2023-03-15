using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditChasing : StateMachineBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    GameObject player;
    GameObject Special;
    [SerializeField]float chaseRange;
    [SerializeField]float Specialdistance;
    [SerializeField]float distance;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
        player =GameObject.FindGameObjectWithTag("Player");
        Special=GameObject.FindGameObjectWithTag("Special");
        agent.speed=6f;
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distance = Vector3.Distance(player.transform.position,animator.transform.position);
        Specialdistance = Vector3.Distance(Special.transform.position,animator.transform.position);
        if(distance< chaseRange)
        {
            agent.SetDestination(player.transform.position);
            if(distance > chaseRange)
                animator.SetBool("IsChasing",false);
            if(distance<=2f&& distance>=1.5f)
                animator.SetBool("IsPunching",true);
        }
        else if(Specialdistance< chaseRange)
        {
            agent.SetDestination(Special.transform.position);
            if( Specialdistance > chaseRange)
                animator.SetBool("IsChasing",false);
            if(Specialdistance<=20f)
                animator.SetBool("IsPunching",true);
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
