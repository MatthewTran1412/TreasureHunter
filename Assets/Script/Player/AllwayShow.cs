using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class AllwayShow : MonoBehaviour
{
    public Text CostMeatText;
    [SerializeField]GameObject Player;
    public Attack atk;
    public PlayerStatus PS;
    public Text slidertext;
    public HealthBar HB;
    //public Text BoardContent;
    public Text textmeat;
    public Text USMTitle;
    public Text USMLeft;
    public Text USMRight;
    public Slider loadslider;
    public GameObject Nextbtn;
    public GameObject Previousbtn;
    public GameObject Closebtn;
    public GameObject Q1;
    public GameObject Q2;
    public GameObject Q3;
    public GameObject Boss;
    public GameObject loadsreen;
    public GameObject LoadImage;
    public GameObject statictext;
    public GameObject slidertxt;
    public GameObject Loadslider;
    public GameObject StartupText;
    bool PlayVideo=true;
    public Text NumberMedkid;
    public bool Istransform=false;
    public bool LoadIsDone=false;
    [SerializeField]public GameObject PlayVideoClip;
    [SerializeField]public GameObject VideoPlayer;
    [SerializeField]GameObject Wolf;

    public float loading;
    public float distance;
    public int page;
    bool check=true;
    bool Bossing=true;
    [SerializeField]Animator anim;
    [SerializeField]public GameObject Portal1;

    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
              atk.text.text="\tWorld Level:"+atk.PS.LevelWord+"\n"+
          "Level:\t"+atk.PS.currentlevel+"\n"+
          "HP:\t"+atk.PS.HB.maxHealth+"\n"+
          "Left Hand:\t"+atk.PS.LF.damage+"\n"+
          "Right Hand:\t"+atk.PS.RH.damage+"\n"+
          "EXP:\t"+atk.PS.currentexp+"/"+atk.PS.maxexp+"\n";
        atk.text1.text="\nNormal Attack:\t"+atk.Bite.damage+"\n"+
            "Heavy Attack:\t"+(atk.Bite.damage+atk.Heavy.damage*2)+"\n";
        textmeat.text=PS.currentmeat+"/"+PS.maxmeat;
        USMTitle.text="USER MANUAL";
        if(Bossing==true)
          CostMeatText.text="Are you sure you want to cost 30 Meat to enter the dungeon?";
        else if (Bossing==false)
          CostMeatText.text="Do you want to exit the dungeon?";
        NumberMedkid.text=HB.medkid.ToString();

      //distance = Vector3.Distance(transform.position,Boss.transform.position);
      //if(distance<=50f)
      //  Boss.SetActive(true);
      //else
      //  Boss.SetActive(false);
      switch(page) 
        {
          case 1:
            Nextbtn.SetActive(true);
            Previousbtn.SetActive(false);
            Closebtn.SetActive(false);
            USMLeft.text="\tCLICK Z TO OPEN AGAIN THE USER MANUAL(THIS BOOK)\n\n If you are newbie, please read content of this book carefully to know about tutorial of this game.\n If you are oldbie, you can skip this and enjoy the game.";
            USMRight.text="\tFirst, you need to know about HEALTH, STAMINA, ENERGY in turn from top to bottom in the upper left conner.\n+Health or HP: you can use Medkid to recovery ( H button) and if you wanna know how many medkid you have look at the center right, there has the mushroom rice icon and number of it.\n+Stamina: it will cost when you use Heavy Attack and just recover when you are in relax mode.\n+Energy: as the same with Stamina but cost when you use laser gun.";
            break;
          case 2:
            Nextbtn.SetActive(true);
            Previousbtn.SetActive(true);
            Closebtn.SetActive(false);
            USMLeft.text="\t\t\t\t\t\tBasic Tutorial\nMovement: Use the usual movement buttons (WSAD or arrow key)\nThere have two mode to attack in this game(Click R to change between these mode):\n+Close combat:\n+ Light punch(left click with mouse): fast but weak \n+ Heavy attack(right click with mouse): deal big damage but slot and cost stamina ";
            USMRight.text="+Shooting mode: Only use laser gun to attack, strong and fast, this mode is suitable for snipe.\n\n There is also another button with a different use like:\n+Left shift: Boost Abasic attack and basic deffence.\n+Z button: Open User Manual book.\n+F button: contact with NPC or item.\n+K and P button: Open status board of Player and pet";
            break;
          case 3:
            Nextbtn.SetActive(false);
            Previousbtn.SetActive(true);
            Closebtn.SetActive(true);
            USMLeft.text="Pet:your partner help you deal damage to enemy when you attack it and do not worry about him will die cause he is immortal.\nYou can buy some item form the seller (Shop NPC) to prevent\nThere is some advice: Follow the misson to take more exp and gold to upgrade your character and kill/farming to uplevel is another solution to upgrade.";
            USMRight.text="Prepare enought number of first aid before the big thing.\n\n\n\n\n\n\n\t\tFinal,thanks for your downloaded and enjoy.\nIf there have suggestions, please send mail to **********. ";
            break;
          default:
            break;
        }
        if(Istransform==true)
        {
          slidertext.text=(float)Mathf.Round(loading*100f)/100f+"%";
          loading+=Time.deltaTime*10;
          loadslider.value=loading;
          //StartCoroutine(Teleport());
          if(loading>=99.9f)
          {
            LoadImage.SetActive(false);
            Loadslider.SetActive(false);
            slidertxt.SetActive(false);
            statictext.SetActive(false);
            anim.SetTrigger("IsComplete");
            Istransform=false;
            StartCoroutine(WaitSetDisable());
            StartupText.SetActive(true);
            if(check==false)
            {
              atk.islockmouse=true;
              atk.TPCCamera.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed=1f;
              atk.TPCCamera.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed=100f;
            }
            if(PlayVideo==true)
              StartCoroutine(WaitVideo());
            check=false;
          }
        }
    }
    public void NextPage(int Page)
    {
        page+=Page;
    }
    public void PreviousPage(int Page)
    {
        page-=Page;
    }
    public void LoadScreen()
    {
      loading=0;
      Istransform=true;
      StartCoroutine(WaitShow());
    }
    IEnumerator WaitShow()
    {
      yield return new WaitForSeconds(1);
      LoadImage.SetActive(true);
      Loadslider.SetActive(true);
      slidertxt.SetActive(true);
      statictext.SetActive(true);
    }
    IEnumerator WaitSetDisable()
    {
      yield return new WaitForSeconds(1);
      loadsreen.SetActive(false);
    }
    IEnumerator WaitVideo()
    {
      yield return new WaitForSeconds(12);
      StartupText.SetActive(false);
      LoadIsDone=true;
      StartCoroutine(Reset());
      PlayVideoClip.SetActive(true);
      VideoPlayer.SetActive(true);
      PlayVideo=false;
    }
    IEnumerator Reset()
    {
      yield return new WaitForSeconds(60);
      LoadIsDone=false;
    }
    public void Teleport()
    {
      if(Bossing==true)
        StartCoroutine(WaittoTele());
      else
        StartCoroutine(WaitforReturn());
    }
    IEnumerator WaittoTele()
    {
      yield return new WaitForSeconds(2);
      transform.position=Portal1.transform.position;
      Wolf.transform.position=Portal1.transform.position;
      Bossing=false;
    }
    IEnumerator WaitforReturn()
    {
      yield return new WaitForSeconds(2);
      transform.position=HB.RespawnPoint.position;
      Wolf.transform.position=Portal1.transform.position;
      Bossing=true;
    }
}
