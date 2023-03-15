using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ContactNPC : MonoBehaviour
{
    [Header("CueMark")]
    [SerializeField]public GameObject TakeQuestMark;
    [SerializeField]public GameObject DoingQuestMark;
    [SerializeField]public GameObject DoneQuestMark;
    [SerializeField]GameObject ShowMessage;
    [SerializeField] QuestManager QM;
    [SerializeField]Attack Atk;
    public bool Action=false;

    GameObject Player;
    [Header("Ink JSON")]
    public TextAsset[] QuestText;
    [SerializeField] GameObject[] Bandit;
    private void Awake()
    {
        TakeQuestMark.SetActive(false);
        DoingQuestMark.SetActive(false);
        DoneQuestMark.SetActive(false);
    }
    void Start()
    {
        ShowMessage.SetActive(false);
        Player=GameObject.FindGameObjectWithTag("Player");
        Atk=Player.GetComponent<Attack>();
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
        GameObject[] Bandit= GameObject.FindGameObjectsWithTag("Bandit");
        int count=Bandit.Length;
        for(int i=0;i<Bandit.Length;i++)
        {
            if(Bandit[i].GetComponent<EnemyHealth>().HP<=0)
                count--;
            i++;
        }
        if(count<=0 && QM.Quest1==false && QM.Quest2==false && QM.ChangeAnother==false&& QM.GetbackTreasure==false)
        {
            QM.TalkWithSpecial=true;
            QM.TakeQuest=true;
        }
        if(gameObject.name=="FirstNPC")
        {
            if(QM.TakeQuest==true && QM.Quest1==true)
            {
                TakeQuestMark.SetActive(true);
                DoingQuestMark.SetActive(false);
                DoneQuestMark.SetActive(false);
            }
            else if(QM.TakeQuest==true && QM.ChangeAnother==true)
            {
                TakeQuestMark.SetActive(true);
                DoingQuestMark.SetActive(false);
                DoneQuestMark.SetActive(false);
            }
            else if(QM.DoingQuest==true && QM.Quest1==true)
            {
                TakeQuestMark.SetActive(false);
                DoingQuestMark.SetActive(true);
                DoneQuestMark.SetActive(false);
            }
            else if(QM.PayQuest==true && QM.Quest1==true)
            {
                TakeQuestMark.SetActive(false);
                DoingQuestMark.SetActive(false);
                DoneQuestMark.SetActive(true);
            }    
            else
            {
                TakeQuestMark.SetActive(false);
                DoingQuestMark.SetActive(false);
                DoneQuestMark.SetActive(false);
            }
        }
        else if(gameObject.name=="Minotaur")
        {
            if(gameObject.tag=="Special")
            {
                if(QM.TalkWithSpecial==true && QM.TakeQuest==true && Bandit.Length<=0)
                {
                    TakeQuestMark.SetActive(true);
                    DoingQuestMark.SetActive(false);
                    DoneQuestMark.SetActive(false);
                }
                else if(QM.GetbackTreasure==true && QM.DoingQuest==true && Bandit.Length<=0)
                {
                    TakeQuestMark.SetActive(false);
                    DoingQuestMark.SetActive(true);
                    DoneQuestMark.SetActive(false);
                }
                else
                {
                    TakeQuestMark.SetActive(false);
                    DoingQuestMark.SetActive(false);
                    DoneQuestMark.SetActive(false);
                }
            }
            else 
            {
                if(QM.TakeQuest==true&&QM.Quest2==true)
                {
                    TakeQuestMark.SetActive(true);
                    DoingQuestMark.SetActive(false);
                    DoneQuestMark.SetActive(false);
                }
                else if(QM.DoingQuest==true&&QM.Quest2==true)
                {
                    TakeQuestMark.SetActive(false);
                    DoingQuestMark.SetActive(true);
                    DoneQuestMark.SetActive(false);
                }
                else if(QM.PayQuest==true&&QM.Quest2==true)
                {
                    TakeQuestMark.SetActive(false);
                    DoingQuestMark.SetActive(false);
                    DoneQuestMark.SetActive(true);
                }    
                else if(QM.ChangeAnother==true && QM.PayQuest==true)
                {
                    TakeQuestMark.SetActive(false);
                    DoingQuestMark.SetActive(false);
                    DoneQuestMark.SetActive(true);
                }
                else if(QM.GetbackTreasure==true && QM.PayQuest)
                {
                    TakeQuestMark.SetActive(false);
                    DoingQuestMark.SetActive(false);
                    DoneQuestMark.SetActive(true);
                }
                else
                {
                    TakeQuestMark.SetActive(false);
                    DoingQuestMark.SetActive(false);
                    DoneQuestMark.SetActive(false);
                }
            }
        }
        if(Input.GetKeyUp(KeyCode.F))
        {
            if(Action==true && !DialogManager.GetInstance().dialoguePlaying)
            {
                Atk.cannotatk=true;
                Atk.CanMove=false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible=true;
                Atk.TPCCamera.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed=0f;
                Atk.TPCCamera.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed=0f;
                if(gameObject.name=="FirstNPC")
                {
                    if(QM.TakeQuest==true && QM.Quest1==true)
                        DialogManager.GetInstance().EnterDialogueMode(QuestText[0]);
                    else if(QM.PayQuest==true && QM.Quest1==true)
                        DialogManager.GetInstance().EnterDialogueMode(QuestText[2]);
                    else if(QM.DoingQuest==true && QM.Quest1==true)
                        DialogManager.GetInstance().EnterDialogueMode(QuestText[1]);
                    else if(QM.ChangeAnother==true && QM.TakeQuest==true)
                        DialogManager.GetInstance().EnterDialogueMode(QuestText[3]);
                    else if(QM.Quest1==false)
                        DialogManager.GetInstance().EnterDialogueMode(QuestText[9]);
                }
                else if(gameObject.name=="Minotaur")
                {
                    if(gameObject.tag=="Special")
                    {
                        if(QM.TalkWithSpecial==true && QM.TakeQuest==true && count==0)
                            DialogManager.GetInstance().EnterDialogueMode(QuestText[7]);
                        else if(QM.TalkWithSpecial==true && QM.DoingQuest==true)
                            DialogManager.GetInstance().EnterDialogueMode(QuestText[1]);
                        else if(QM.TalkWithSpecial==false)
                            DialogManager.GetInstance().EnterDialogueMode(QuestText[9]);
                    }
                    else
                    {
                        if(QM.TakeQuest==true&&QM.Quest2==true)
                            DialogManager.GetInstance().EnterDialogueMode(QuestText[5]);
                        else if(QM.PayQuest==true &&QM.Quest2==true)
                            DialogManager.GetInstance().EnterDialogueMode(QuestText[6]);
                        //else if(QM.ChangeAnother==true &&QM.Quest2==true)
                        //    DialogManager.GetInstance().EnterDialogueMode(QuestText[6]);
                        else if(QM.ChangeAnother==true && QM.PayQuest==true)
                            DialogManager.GetInstance().EnterDialogueMode(QuestText[4]);
                        else if(QM.DoingQuest==true&&QM.Quest2==true)
                            DialogManager.GetInstance().EnterDialogueMode(QuestText[1]);
                        else if(QM.PayQuest==true && QM.GetbackTreasure==true)
                            DialogManager.GetInstance().EnterDialogueMode(QuestText[8]);
                        else if(QM.ChangeAnother==false&&QM.GetbackTreasure==false&&QM.Quest2==false)
                            DialogManager.GetInstance().EnterDialogueMode(QuestText[9]);
                    }
                }
                
                //if(QM.DoingQuest==true)
                //    DialogManager.GetInstance().EnterDialogueMode(QuestText[1]);
                //if(QM.Quest1==true&& gameObject.name=="FirstNPC")
                //{
                //    //
                //    //
                //    //if(QM.ChangeAnother==true && QM.DoingQuest==true&& gameObject.name="Minotaurus")
                //    //    DialogManager.GetInstance().EnterDialogueMode(QuestText[7]);
                //}
                //if(QM.Quest2==true)
                //{
                //    if(QM.TakeQuest==true)
                //        DialogManager.GetInstance().EnterDialogueMode(QuestText[4]);
                //    if(QM.PayQuest==true)
                //        DialogManager.GetInstance().EnterDialogueMode(QuestText[5]);
                //    if(QM.ChangeAnother==true)
                //        DialogManager.GetInstance().EnterDialogueMode(QuestText[6]);
                //}
                //if(QM.DoingQuest==true)
                //    DialogManager.GetInstance().EnterDialogueMode(QuestText[1]);
            }
        }
    }
}
