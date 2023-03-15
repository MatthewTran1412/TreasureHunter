using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadRun : StateMachineBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    Transform player;
    [SerializeField]BossMusic BS;
    HealthBar Playerhb;
    Transform Anubis;
    Vector3 respawn;
    [SerializeField] float checkprotect;
    [SerializeField]bool Target=true;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Playerhb= GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBar>();
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
        Anubis=GameObject.FindGameObjectWithTag("BossPoint").transform;
        player =GameObject.FindGameObjectWithTag("Player").transform;
        BS=GameObject.FindGameObjectWithTag("Final").GetComponent<BossMusic>();
        agent.speed=3f;
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(player.position,animator.transform.position);
        checkprotect = Vector3.Distance(Anubis.position,animator.transform.position);
        if(distance<=5.0f&& BS.CanAttack==true){
            animator.SetBool("IsPunching",true);
        }
        if(Target==true)
            agent.SetDestination(player.position);
        else
        {
            animator.SetBool("IsRoar",false);
            agent.SetDestination(Anubis.position);
        }
        if(checkprotect>40)
            Target=false;
        else if(Playerhb.health<=0)
            Target=false;
        if(checkprotect<=10)
            Target=true;
        if(distance>40)
            animator.SetBool("IsChasing",false);

        //if(checkprotect<=10)
        //    animator.SetBool("IsChasing",false);
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
