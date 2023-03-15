using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAtkCD : MonoBehaviour
{
    [SerializeField]public bool NorAtk=true;
    [SerializeField]public bool HeavyAtk=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(NorAtk==false)
            StartCoroutine(NorAtkCD());
        if(HeavyAtk==false)
            StartCoroutine(HeavyAtkCD());
    }
    IEnumerator NorAtkCD()
    {
        yield return new WaitForSeconds(2);
        NorAtk=true;
    }
    IEnumerator HeavyAtkCD()
    {
        yield return new WaitForSeconds(5);
        HeavyAtk=true;
    }
}
