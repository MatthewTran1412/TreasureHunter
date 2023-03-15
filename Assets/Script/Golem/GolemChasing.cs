using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemChasing : StateMachineBehaviour
{
    [SerializeField]GameObject[] Bandit;
    [SerializeField] float chaseRange;
    NavMeshAgent agent;
    [SerializeField]float Banditdistance;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Bandit=GameObject.FindGameObjectsWithTag("Bandit");
        agent=animator.GetComponent<NavMeshAgent>();
        agent.speed=6f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int i=0;
        if(Bandit.Length<=0)
            animator.SetBool("IsChasing",false);
        while(i<Bandit.Length)
        {
            Banditdistance=Vector3.Distance(Bandit[i].transform.position,animator.transform.position);
            if(Banditdistance<=chaseRange)
            {
                agent.SetDestination(Bandit[i].transform.position);
                if(Banditdistance<=2)
                    animator.SetBool("IsPunching",true);
                else if(Bandit[i].GetComponent<EnemyHealth>().HP<=0)
                    animator.SetBool("IsChasing",false);
            }
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
