using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestCheckarea : StateMachineBehaviour
{
    Transform enemy;
    [SerializeField] float atkarea;
    NavMeshAgent agent;
    [SerializeField] float distance;
    List<Transform> checkenemy = new List<Transform>();
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy=GameObject.FindGameObjectWithTag("Enemy").transform;
        agent=animator.GetComponent<NavMeshAgent>();
        foreach(Transform i in enemy)
            checkenemy.Add(i);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach(Transform i in checkenemy)
        {
            distance=Vector3.Distance(animator.transform.position,i.position);
            if(distance<=atkarea)
                animator.SetBool("IsChasing",true);
            //else if(distance>atkarea)
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
