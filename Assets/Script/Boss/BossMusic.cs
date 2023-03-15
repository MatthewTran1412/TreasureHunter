using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusic : MonoBehaviour
{
    [SerializeField]Transform Player;
    public float distance;
    public bool CanAttack=true;
    // Start is called before the first frame update
    void Start()
    {
        Player=GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        if(CanAttack==false)
            StartCoroutine(ResetAttack());
        distance = Vector3.Distance(Player.transform.position,transform.position);
        if(distance<50)
        {
            GetComponent<AudioSource>().enabled=true;
            if(distance<=20)
            {
                GetComponent<AudioSource>().enabled=true;

            }
        }
        else
            GetComponent<AudioSource>().enabled=false;
    }
    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(4.0f);
        CanAttack=true;
    }
}
