using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Cinemachine;
public class DialogManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text dialogueText;
    [Header("Choice UI")]
    [SerializeField] private GameObject[] choices;
    [SerializeField] public GameObject Continuosbtn;
    private Text[] choicesText;
    private Story currentStory;
    public bool dialoguePlaying {get ; private set; }
    public bool IsChoosing=false;
    [SerializeField]public GameObject ShowGain;
    [SerializeField]public Text ShowGained;
    PlayerStatus PS;
    Attack Atk;
    private static DialogManager instance;
    public QuestManager QM;
    int rewardexp;
    float rewardgold;
    [SerializeField]GameObject Portal;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Found more than one Dialog");
        }
        instance = this;
    }

    public static DialogManager GetInstance()
    {
        return instance;
    }
    void Start()
    {
        Portal.SetActive(false);
        PS=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        Atk=GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
        dialoguePlaying=false;
        dialoguePanel.SetActive(false);
        choicesText = new Text[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<Text>();
            index++;
        }
    }
    void Update()
    {
        if(!dialoguePlaying)
        {
            return;
        }
        if(Input.GetKeyUp(KeyCode.F))
        {
            if(IsChoosing==false && dialoguePlaying)
                CountinueStory();
        }
        if(IsChoosing==false)
            Continuosbtn.SetActive(true);
        else if(IsChoosing==true)
            Continuosbtn.SetActive(false);
        if(currentStory.currentChoices.Count<=0)
            IsChoosing=false;
        else 
            IsChoosing=true;
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialoguePlaying=true;
        dialoguePanel.SetActive(true);

        CountinueStory();
    }
    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);
        dialoguePlaying=false;
        dialoguePanel.SetActive(false);
        dialogueText.text= "";
        Atk.cannotatk=false;
        Atk.CanMove=true;
        Atk.islockmouse=true;
        Atk.TPCCamera.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed=2f;
        Atk.TPCCamera.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed=200f;
    }
    public void CountinueStory()
    {
        if(currentStory.canContinue)
        {
            dialogueText.text= currentStory.Continue();
            DisplayChoices();
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }
    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if(currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI had created" + currentChoices.Count);
        }

        int index =0;
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index ++;
        }
        for( int i= index ; i< choices.Length;i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        StartCoroutine(SelecFirstChoice());
    }
    private IEnumerator SelecFirstChoice()
    {
        IsChoosing=false;
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }
    public void MakeChoice( int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        IsChoosing=false;
        if(choicesText[choiceIndex].text=="Accept")
        {
            QM.TookQuest();
            QM.numberhave=0;
        }
        if(choicesText[choiceIndex].text=="Thanks for all."&& QM.ChangeAnother==true)
        {
            QM.TakeQuest=false;
            QM.PayQuest=true;
            QM.ChangeAnother=true;
        }
        if(choicesText[choiceIndex].text=="Thanks"&& QM.ChangeAnother==true)
        {
            QM.Quest2=true;
            QM.IsDoneQuest();
            QM.TakeQuest=true;
            QM.ChangeAnother=false;
            rewardexp=500;
            rewardgold=4000;
            PS.currentexp+=rewardexp;
            PS.currentGold+=rewardgold;
            ShowGain.SetActive(true);
            ShowGained.text="Exp gained:"+rewardexp+"\nGold gained:"+rewardgold;
            StartCoroutine(CloseText());
        }
        if(choicesText[choiceIndex].text=="Easy game"&& QM.Quest2==true)
        {
            QM.TookQuest();
            QM.numberhave=0;
            QM.numberneed=5;
        }
        if(choicesText[choiceIndex].text=="Complete"&& QM.Quest1==true)
        {
            QM.IsDoneQuest();
            QM.TakeQuest=true;
            QM.ChangeAnother=true;
            QM.Quest1=false;
            rewardexp=100;
            rewardgold=2000;
            PS.currentexp+=rewardexp;
            PS.currentGold+=rewardgold;
            ShowGain.SetActive(true);
            ShowGained.text="Exp gained:"+rewardexp+"\nGold gained:"+rewardgold;
            StartCoroutine(CloseText());
        }
        if(choicesText[choiceIndex].text=="OK"&& QM.Quest2 == true)
        {
            QM.IsDoneQuest();
            QM.Quest2=false;
            rewardexp=1000;
            rewardgold=5000;
            PS.currentexp+=rewardexp;
            PS.currentGold+=rewardgold;
            ShowGain.SetActive(true);
            ShowGained.text="Exp gained:"+rewardexp+"\nGold gained:"+rewardgold;
            StartCoroutine(CloseText());
        }
        else if(choicesText[choiceIndex].text=="Very pleased.")
        {
            QM.TookQuest();
            QM.GetbackTreasure=true;
            QM.TalkWithSpecial=false;
            QM.numberhave=0;
            QM.numberneed=1;
            Portal.SetActive(true);
        }
        else if (choicesText[choiceIndex].text=="I will take care of it.")
        {
            QM.IsDoneQuest();
            QM.GetbackTreasure=false;
            rewardexp=10000;
            rewardgold=50000;
            PS.currentexp+=rewardexp;
            PS.currentGold+=rewardgold;
            ShowGain.SetActive(true);
            ShowGained.text="Exp gained:"+rewardexp+"\nGold gained:"+rewardgold+"\n You owned new Anubis statue";
            StartCoroutine(CloseText());
            PS.WorldUp=true;
        }
    }
    public IEnumerator CloseText()
    {
        yield return new WaitForSeconds(2);
        ShowGain.SetActive(false);
    }
}
