using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float lookRadius = 10f;
    [SerializeField ]GameObject player;
    Transform target;
    [SerializeField]UnityEngine.AI.NavMeshAgent agent;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        target= player.transform;
        agent= GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {   
        float distance = Vector3.Distance(target.position,transform.position);
        if(distance<=lookRadius)
        {
            agent.SetDestination(target.position);
            anim.SetBool("Target",true);
        }
        else 
        {
            anim.SetBool("Target",false);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lookRadius);
    }
}
