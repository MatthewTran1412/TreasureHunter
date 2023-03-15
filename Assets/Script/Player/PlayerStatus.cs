using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public GameObject levelupPrefabs;
    public GameObject levelboard;
    public GameObject Portal;
    public Text Content;
    [SerializeField]public HealthBar HB;
    [SerializeField]public LeftHandHit LF;
    [SerializeField]public PlayerATKDMG RH;
    Attack Atk;
    public PetAtk Bite;
    public PetAtk Foot1;
    public PetAtk Foot2;
    public float currentexp;
    public float maxexp;
    public int LevelWord;
    public int currentlevel;
    public int maxlevel;
    public int currentmeat;
    public int maxmeat;
    public bool WorldUp=false;
    public Slider ExpBar;
    public Text ExpText;
    public Text LevelText;
    public AudioClip levelupsound;
    public float timer;
    [SerializeField]public float currentGold;

    void Start()
    {
        Atk=GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
    }
    void Update()
    {
        ExpBar.value= currentexp*100/maxexp;
        ExpText.text=(float)Mathf.Round((float)currentexp*100f)/(float)maxexp+"%";
        LevelText.text = "Level\n" + currentlevel.ToString();
        if(currentlevel<maxlevel&&currentexp>=maxexp)
        {
            int oldlvl = currentlevel;
            float oldmaxHB = HB.maxHealth;
            int oldLH=LF.damage;
            int oldRH=RH.damage;
            AudioSource ac = GetComponent<AudioSource>();
            ac.PlayOneShot(levelupsound);
            //if(levelupPrefabs)
            //    LevelEffect();
            levelboard.SetActive(true);
            currentlevel+=1;
            currentexp=currentexp-maxexp;
            maxexp*=1.5f;
            HB.maxHealth+=30;
            LF.damage+=10;
            RH.damage+=5;
            Bite.damage+=2;
            Foot1.damage+=2;
            Foot2.damage+=2;
            Content.text="Level\t"+oldlvl +"\t->\t"+currentlevel+"\n"+
              "HP:\t"+oldmaxHB +"\t->\t"+HB.maxHealth+"\n"+
              "LeftHand:\t"+oldLH+"\t->\t"+LF.damage+"\n"+
              "RightHand:\t"+oldRH +"\t->\t"+RH.damage+"\n";
            StartCoroutine(Closeboard());
        }
        if(WorldUp==true)
        {
            maxlevel+=10;
            LevelWord+=1;
            HB.maxstamina+=100;
            WorldUp=false;
        }
    }
    public void AddExp(int expgain)
    {
        currentexp+=expgain;
    }
    public void AddGold(float Goldgain)
    {
        currentGold+=Goldgain;
    }
    public void UseMeat(int meat)
    {
        currentmeat-=meat;
    }
    public void AddMeat(int meat)
    {
        currentmeat+=meat;
    }
    public void ReCoverMeat()
    {
        timer+=Time.deltaTime;
        if(timer==60)
        {
            AddMeat(1);
            timer-=timer;
        }
    }
    //void LevelEffect()
    //{
    //    var go = Instantiate(levelupPrefabs,transform.position,Quaternion.identity,transform);
    //}
    IEnumerator Closeboard()
    {
        yield return new WaitForSeconds(3);
        levelboard.SetActive(false);
    }
    void Transport()
    {
        transform.position=Portal.transform.position;
    }
    void Return()
    {
        transform.position=HB.RespawnPoint.transform.position;
        Atk.islockmouse=true;
    }
}
