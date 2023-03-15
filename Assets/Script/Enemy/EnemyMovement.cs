using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]private GameObject Player;
    [SerializeField]private float Distance;

    [SerializeField]private bool isAngered;

    [SerializeField]public NavMeshAgent agent;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        Distance=Vector3.Distance(Player.transform.position,this.transform.position);

        if(Distance<=10)
        {
            isAngered=true;
        }
        if(Distance>10)
        {
            isAngered=false;
        }

        if(isAngered)
        {
            agent.isStopped=false;
            agent.SetDestination(Player.transform.position);
            anim.SetFloat("Status",1f,0.1f,Time.deltaTime);
            //if(Distance<=1f)
            //{
            //    anim.SetTrigger("Attack");
            //}
        }
        if(!isAngered)
        {
            agent.isStopped=true;
            anim.SetFloat("Status",0f,0.1f,Time.deltaTime);
        }
    }   
}
