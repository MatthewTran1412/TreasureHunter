using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBoard : MonoBehaviour
{
    [SerializeField]Attack Atk;
    public GameObject StatusB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CloseBtn()
    {
        StatusB.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible=false;
        Atk.CloseAll--;
        Atk.cannotatk=false;
    }
}
