using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;
using Cinemachine;

public class Attack : MonoBehaviour
{
    public GameObject Player;
    public PlayerStatus PS;
    [SerializeField]public Rig rig;
    [SerializeField]public HealthBar HB;
    [SerializeField]public TPC Tpc;
    [SerializeField]public OpenMenu OPMenu;
    [SerializeField]public MainMenu Mainmenu;
    [SerializeField]public AllwayShow allway;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public bool IsAttacking = false;

    public bool Canheav = true;
    public float HeavCD = 1.0f;
    public bool IsHeavAttacking = false;

    public bool Canroar = true;
    public float BuffCooldown = 1.0f;
    public bool isBuff = false;

    public bool LaserisActive=false;
    public bool ManualActive=false;

    public AudioClip punchsound;
    public AudioClip roarsound;
    public AudioClip slashsound;
    public bool CanMove = true;
    public bool IsShootingMode;

    public GameObject textPrefabs;
    public GameObject BuffPrefabs;
    public GameObject StatusBoard;
    public GameObject Playerstatus;
    public GameObject PetStatus;
    public GameObject Buffimage;
    public GameObject UserManual;
    public GameObject GiveQuest;
    public GameObject laserPoint;
    public GameObject laserPoint1;
    public GameObject laserPoint2;
    public GameObject TPCCamera;
    public GameObject ShootingCamera;

    public PetAtk Bite;
    public PetAtk Heavy;
    public Text text;
    public Text text1;
    public float Staminacost=25f;
    public GameObject StaminaBar;
    [SerializeField]public int CloseAll;
    [SerializeField]public bool islockmouse;
    [SerializeField]public bool cannotatk;
    
    void Start()
    {
        rig.weight=0f;
        CloseAll=0;
        islockmouse=true;
        cannotatk=true;
        IsShootingMode=false;
    }
    void Update()
    {
        if(LaserisActive==true)
            Tpc.timer-=Tpc.timer;
        if(OPMenu.GameIsPaused==false && allway.Istransform==false&&Mainmenu.StartGame==false&&DialogManager.GetInstance().dialoguePlaying==false)
        {
            if(islockmouse==true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible=false;
            }
            else if(islockmouse==false)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible=true;
            }
            HB.RechargeStamina();
            if(Input.GetKeyDown(KeyCode.R))
            {
                if(LaserisActive==false)
                {
                   laserPoint.GetComponent<LineRenderer>().enabled=true;
                   laserPoint1.GetComponent<LineRenderer>().enabled=true;
                   laserPoint2.GetComponent<LineRenderer>().enabled=true;
                   LaserisActive=true;
                   IsShootingMode=true;
                   TPCCamera.SetActive(false);
                   ShootingCamera.SetActive(true);
                   rig.weight=1f;
                }
                else if(LaserisActive==true)
                {
                    laserPoint.GetComponent<LineRenderer>().enabled=false;
                    laserPoint1.GetComponent<LineRenderer>().enabled=false;
                    laserPoint2.GetComponent<LineRenderer>().enabled=false;
                    LaserisActive=false;
                    IsShootingMode=false;
                    ShootingCamera.SetActive(false);
                    TPCCamera.SetActive(true);
                    rig.weight=0f;
                }
            }
            if(Input.GetKeyDown(KeyCode.Z))
            {
                 if(ManualActive==false)
                 {
                    islockmouse=false;
                    UserManual.SetActive(true);
                    ManualActive=true;
                    Time.timeScale = 0f;
                    CloseAll++;
                    cannotatk=true;
                 }
                 else if(ManualActive==true)
                 {
                    UserManual.SetActive(false);
                    ManualActive=false;
                    Time.timeScale=1f;
                    CloseAll--;
                    islockmouse=true;
                    cannotatk=false;
                 }
             }
             if(Input.GetKeyUp(KeyCode.K))
             {
                CloseAll++;
                islockmouse=false;
                StatusBoard.SetActive(true);
                PetStatus.SetActive(false);
                Playerstatus.SetActive(true);
                Time.timeScale = 0f;
                cannotatk=true;
             }
             if(Input.GetKeyUp(KeyCode.P))
             {
                CloseAll++;
                islockmouse=false;
                StatusBoard.SetActive(true);
                PetStatus.SetActive(true);
                Playerstatus.SetActive(false);
                Time.timeScale = 0f;
                cannotatk=true;
             }
             if(Input.GetKeyUp(KeyCode.Escape))
             {
                if(CloseAll>0)
                {   
                    islockmouse=true;
                    cannotatk=false;
                    StatusBoard.SetActive(false);
                    Playerstatus.SetActive(false);
                    PetStatus.SetActive(false);
                    UserManual.SetActive(false);
                    GiveQuest.SetActive(false);
                    ManualActive=false;
                    CloseAll=0;
                    Time.timeScale = 1f;
                }
             }
            if(cannotatk==false && IsShootingMode==false)
            {   
                if(Input.GetMouseButtonDown(0))
                {
                    if(CanAttack)
                    {
                        Punch();
                    }
                }
                  if( Input.GetMouseButtonDown(1))
                {
                     if(Canheav  && Tpc.isGrounded &&HB.currentstamina>=Staminacost)
                     {
                         Slash();
                     }
                }
                  if( Input.GetKeyDown(KeyCode.LeftShift))
                {
                     if(Canroar && Tpc.isGrounded)
                     {
                         Roar();
                     }
                }
            }
        }
    }
    public void Punch()
    {
       IsAttacking = true;
       CanAttack=false;
       Animator anim = Player.GetComponent<Animator>();
       anim.SetTrigger("Attack");
       AudioSource ac = GetComponent<AudioSource>();
       ac.PlayOneShot(punchsound);
       StartCoroutine(ResetAttackCooldown());
    }
    IEnumerator ResetAttackCooldown()
    {
       StartCoroutine(ResetAttackBool());
       yield return new WaitForSeconds(AttackCooldown);
       CanAttack=true;
    }
    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(0.3f);
        IsAttacking=false;
    }

    public void Slash()
    {
        HB.CostStamina(Staminacost);
        Canheav=false;
        CanMove=false;
        Animator anim = Player.GetComponent<Animator>();
        anim.SetTrigger("HeavyAtk");
        StartCoroutine(WaitforSlashsound());
        if(!StaminaBar)
            ShowStaminaBar();
        StartCoroutine(ResetHeavAttackCooldown());
        StartCoroutine(ResetMovement());
    }
    IEnumerator ResetHeavAttackCooldown()
    {
       StartCoroutine(ResetHeavAttackBool());
       yield return new WaitForSeconds(HeavCD);
       Canheav=true;
    }
    IEnumerator ResetHeavAttackBool()
    {
        yield return new WaitForSeconds(1.5f);
        IsHeavAttacking=false;
    }

    public void Roar()
    {
        Buffimage.SetActive(true);
       isBuff = true;
       Canroar=false;
       CanMove=false;
       Animator anim = Player.GetComponent<Animator>();
       anim.SetTrigger("Roar");
        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(roarsound);
        if(BuffPrefabs&&CanMove==false)
            ShowBuffEffect();
       StartCoroutine(ResetBuffCooldown());
       StartCoroutine(ResetMovement());
    }
    IEnumerator ResetMovement(){
        yield return new WaitForSeconds(2.5f);
        CanMove=true;
    }
    IEnumerator ResetBuffCooldown()
    {
        StartCoroutine(ResetBuffBool());
        yield return new WaitForSeconds(BuffCooldown);
        Canroar=true;
    }
    //buff duration
    IEnumerator ResetBuffBool()
    {
        yield return new WaitForSeconds(60.0f);
        isBuff=false;
        Buffimage.SetActive(false);
    }
    void ShowBuffEffect()
    {
        var go = Instantiate(BuffPrefabs,transform.position,Quaternion.identity,transform);
    }
    IEnumerator WaitforSlashsound()
    {
        yield return new WaitForSeconds(0.75f);
        IsHeavAttacking = true;
        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(slashsound);
    }
    void ShowStaminaBar()
    {
        var go = Instantiate(StaminaBar,transform.position,Quaternion.identity,transform);
        go.GetComponent<Slider>().value=HB.currentstamina;
    }
    public void Disableallboard()
    {
        ManualActive=false;
        cannotatk=false;
        CloseAll--;
        islockmouse=true;
    }
    IEnumerator ResetCloseall()
    {
        yield return new WaitForSeconds(1);
        CloseAll++;
    }
    public void lockmouse()
    {
        islockmouse=true;
    }
    public void unlockmouse()
    {
        islockmouse=false;
    }
    IEnumerator RunNormal()
    {
        yield return new WaitForSeconds(0.1f);
        Time.timeScale=1f;
        TPCCamera.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed=1f;
        TPCCamera.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed=100f;
        cannotatk=false;
        CanMove=true;
        islockmouse=true;
    }
}
