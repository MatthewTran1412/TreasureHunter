using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDisPlayer : MonoBehaviour
{
    [SerializeField]Transform Player;
    float distance;
    // Start is called before the first frame update

    // Update is called once per frame
    void Start()
    {
        Player=GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        float distance = Vector3.Distance(Player.transform.position,transform.position);
        if(distance<30)
        {
            GetComponent<AudioSource>().enabled=true;
            //do{
            //GetComponent<AudioSource>().Volume+=0.1f;
            //}while(distance<0)
        }
        else
            GetComponent<AudioSource>().enabled=false;
    }
}
