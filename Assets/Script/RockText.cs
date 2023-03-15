using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockText : MonoBehaviour
{
    [SerializeField]public string M1;
    [SerializeField]public string M2;
    // Start is called before the first frame update

    // Update is called once per frame
    void Start()
    {
        GetComponent<TextMesh>().text=M1;
    }
    void Update()
    {
    
    
    }
}
