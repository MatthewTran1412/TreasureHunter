using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoar : MonoBehaviour
{
    [SerializeField]Transform Player;
    public float distance;
    // Start is called before the first frame update

    // Update is called once per frame
    void Start()
    {
        Player=GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        distance = Vector3.Distance(Player.transform.position,transform.position);
        if(distance<=20)
        {
            StartCoroutine(WaitforRoarSound());
        }
        else   
            GetComponent<AudioSource>().enabled=false;
    }
    IEnumerator WaitforRoarSound()
    {
        yield return new WaitForSeconds(1.3f);
        GetComponent<AudioSource>().enabled=true;
    }
}
