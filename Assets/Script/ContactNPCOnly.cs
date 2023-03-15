using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ContactNPCOnly : MonoBehaviour
{
    [SerializeField]GameObject ShowMessage;
    public GameObject GiveQuest;
    public bool Action=false;
    [SerializeField]Attack Atk;
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        ShowMessage.SetActive(false);
        Player=GameObject.FindGameObjectWithTag("Player");
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
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.F))
        {
            if(Action==true)
            {
                Atk.islockmouse=false;
                Atk.cannotatk=true;
                Atk.CanMove=false;
                Atk.TPCCamera.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed=0f;
                Atk.TPCCamera.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed=0f;
                GiveQuest.SetActive(true);
                ShowMessage.SetActive(false);
                transform.LookAt(Player.transform);
            }
        }
    }
}
