using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class MainMenu : MonoBehaviour
{
    public float loading;
    public bool StartGame;
    public Slider Menuslider;
    public Text MenuText;
    public GameObject MenuTxt;
    public GameObject MenuImage;
    public GameObject StartBtn;
    public GameObject MenuTitle;
    public GameObject statictext;
    public GameObject Menuslide;
    public GameObject Player;
    [SerializeField]bool logodone=false;
    [SerializeField] bool titledone=false;
    public bool StartupIsDone=false;
    [SerializeField] public Animator ImageMenuanim;
    [SerializeField]Animator StartBtnanim;
    Attack Atk;
    // Start is called before the first frame update
    void Start()
    {
      Time.timeScale=1f;
      StartGame=true;
      Player=GameObject.FindGameObjectWithTag("Player");
      Atk=Player.GetComponent<Attack>();
    }

    // Update is called once per frame
    void Update()
    {
      StartCoroutine(LogoIsdone());
        if(StartGame==true&& logodone==true)
        {
          MenuImage.GetComponent<AudioSource>().enabled=true;
          //ImageMenuanim.SetBool("IsCompleted",false);
          MenuImage.SetActive(true);
          if(titledone==true)
          {
            Menuslide.SetActive(true);
            MenuTxt.SetActive(true);
            statictext.SetActive(true);
            MenuTitle.SetActive(true);
            loading+=Time.deltaTime*10;
            Menuslider.value=loading;
          }
        }
          MenuText.text=(float)Mathf.Round(loading*100f)/100f+"%";
          if(loading>=99.9f)
          { 
            Menuslide.SetActive(false);
            MenuTxt.SetActive(false);
            statictext.SetActive(false);
            StartBtn.SetActive(true);
          }
        }
    public void StartButton()
    {
      StartGame=false;
      ImageMenuanim.SetBool("IsCompleted",true);
      MenuImage.SetActive(false);
      StartBtn.SetActive(false);
      MenuTitle.SetActive(false);
      StartupIsDone=true;
      StartCoroutine(Reset());
      Player.GetComponent<AudioSource>().enabled=true;
    }
    IEnumerator WaittoShow()
    {
        yield return new WaitForSeconds(6);
        Menuslide.SetActive(true);
        MenuTxt.SetActive(true);
        statictext.SetActive(true);
        MenuTitle.SetActive(true);
    }
    IEnumerator WaitForStart()
    {
      yield return new WaitForSeconds(5);
      MenuImage.GetComponent<AudioSource>().enabled=true;
      ImageMenuanim.SetBool("IsCompleted",false);
      MenuImage.SetActive(true);
    }
    IEnumerator WaitforLoad()
    {
      yield return new WaitForSeconds(6.5f);
      loading+=Time.deltaTime*10;
      Menuslider.value=loading;
    }
    IEnumerator LogoIsdone()
    {
      yield return new WaitForSeconds(5);
      logodone=true;
      StartCoroutine(TitleIsdone());
    }
    IEnumerator TitleIsdone()
    {
      yield return new WaitForSeconds(2);
      titledone=true;
    }
    public void RunNormal()
    {
      Time.timeScale=1f;
      Atk.TPCCamera.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed=1f;
      Atk.TPCCamera.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed=100f;
      Atk.cannotatk=false;
      Atk.CanMove=true;
    }
    IEnumerator Reset()
    {
      yield return new WaitForSeconds(60);
      StartupIsDone=false;
    }
}
