using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checktoopenrock : MonoBehaviour
{
    Animator anim;
    [SerializeField]GameObject ShowMessage;
    public bool Action=false;
    [SerializeField]GameObject CostBoard;
    void Start()
    {
        ShowMessage.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ShowMessage.SetActive(true);
            Action=true;
        }        
    }
    private void OnTriggerExit(Collider other)
    {
        ShowMessage.SetActive(false);
        Action=false;     
    }
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.F))
        {
            if(Action==true)
            {
                CostBoard.SetActive(true);
                ShowMessage.SetActive(false);
            }
        }
    }
    public void Animate()
    {
        anim.SetBool("IsFalling",true);
        StartCoroutine(Restart());
    }
    IEnumerator Restart()
    {
        yield return new WaitForSeconds(10);
        anim.SetBool("IsFalling",false);
    }
}
