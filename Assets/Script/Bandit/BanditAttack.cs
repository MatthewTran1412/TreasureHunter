using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditAttack : StateMachineBehaviour
{
    GameObject player;
    [SerializeField]GameObject Golem;
    [SerializeField]float Banditdistance;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player=GameObject.FindGameObjectWithTag("Player");
        Golem=GameObject.FindGameObjectWithTag("Special");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance=Vector3.Distance(player.transform.position,animator.transform.position);
        float Golemdistance=Vector3.Distance(Golem.transform.position,animator.transform.position);
        if(Golemdistance<=2000)
        {
            animator.transform.LookAt(Golem.transform);
            if(Golem.GetComponent<GolemHealth>().HP<=0)
                animator.SetBool("IsPunching",false);
            else if(Golemdistance>20f)
                animator.SetBool("IsPunching",false);
        }
        else if(distance <=2)
        {
            animator.transform.LookAt(player.transform);
            if(player.GetComponent<HealthBar>().health<=0)
                animator.SetBool("IsPunching",false);
            else if(distance >2f)
                animator.SetBool("IsPunching",false);
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
