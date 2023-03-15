using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMentwithoutAgent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]GameObject Player;
    [SerializeField]float speed;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance =  Vector3.Distance(transform.position,Player.transform.position);
        
        if(distance>1.5f)
            speed=1f;
        else if( distance>4)
            speed=6f;
        else
            speed=0;
        transform.position=Vector3.MoveTowards(transform.position,Player.transform.position,speed*Time.deltaTime);
    }
}
