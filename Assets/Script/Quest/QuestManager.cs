using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public bool TakeQuest;
    public bool DoingQuest;
    public bool PayQuest;
    public bool Quest1;
    public bool Quest2;
    public bool ChangeAnother;
    public bool TalkWithSpecial;
    public bool GetbackTreasure;
    public int numberneed;
    public int numberhave;
    // Start is called before the first frame update
    void Start()
    {
        Quest1=true;
        TakeQuest=true;
        DoingQuest=false;
        PayQuest=false;
        Quest2=false;
        ChangeAnother=false;
        TalkWithSpecial=false;
        GetbackTreasure=false;
    }
    // Update is called once per frame
    void Update()
    {
        checkPayQuest();
    }
    public void TookQuest()
    {
        TakeQuest=false;
        DoingQuest=true;
    }
    public void IsDoneQuest()
    {
        PayQuest=false;
        numberhave=0;
    }
    public void checkPayQuest()
    {
        if(numberhave>=numberneed)
        {
            DoingQuest=false;
            PayQuest=true;
        }
    }
}
