using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class checktoactive : MonoBehaviour
{
    [SerializeField]GameObject FinalBoss;
    [SerializeField]GameObject ShowMessage;
    [SerializeField]public GameObject ShowGain;
    [SerializeField]public Text ShowGained;
    public bool BossKilled=false;
    public bool Action=false;
    [SerializeField]QuestManager QM;
    [SerializeField]Attack atk;
    // Start is called before the first frame update
    void Start()
    {
        FinalBoss=GameObject.FindGameObjectWithTag("Final");
        ShowMessage.SetActive(false);
        atk=GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
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
        if(FinalBoss.GetComponent<BossHealth>().HP<=0)
            BossKilled=true;
        if(Input.GetKeyUp(KeyCode.F))
        {
            if(BossKilled==true && Action==true)
            {
                ShowMessage.SetActive(false);
                if(QM.GetbackTreasure==true)
                    QM.numberneed++;
                gameObject.SetActive(false);
            }
        }
    }
}
