using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floattingtext : MonoBehaviour
{
    public float Destroytime=2f;
    public Vector3 offset = new Vector3(0,2,0);
    public Vector3 RandomizeIntesity = new Vector3(1,0,0);
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,Destroytime);
        transform.localPosition+=offset;
        transform.localPosition+= new Vector3(Random.Range(-RandomizeIntesity.x,RandomizeIntesity.x),Random.Range(-RandomizeIntesity.y,RandomizeIntesity.y),Random.Range(-RandomizeIntesity.z,RandomizeIntesity.z));

    }
}
