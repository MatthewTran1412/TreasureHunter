using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkinCircle : MonoBehaviour
{
    [SerializeField]private float moveSpeed;
    [SerializeField]float counting;
    [SerializeField]int temp;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        transform.Rotate(0f,3f,0f);
    }
}
