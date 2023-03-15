using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Golem : StateMachineBehaviour
{
    [SerializeField]GameObject[] Bandit;
    [SerializeField] float chaseRange;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Bandit=GameObject.FindGameObjectsWithTag("Bandit");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int i=0;
        while (i<Bandit.Length)
        {
            float Banditdistance=Vector3.Distance(Bandit[i].transform.position,animator.transform.position);
            if(Banditdistance<=chaseRange)
                animator.SetBool("IsChasing",true);
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
