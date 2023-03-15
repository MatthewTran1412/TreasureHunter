using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPC : MonoBehaviour
{
//normal setting
    public CharacterController Cc;
    public Transform cam;
    public Animator anim;
    [SerializeField]public Attack atk;
    [SerializeField]MainMenu Mainmenu;
    [SerializeField]AllwayShow allway;
    public AudioClip relaxsound;
    public bool IsMusic=true;

    [SerializeField]public float speed=6f;
    [SerializeField]private Vector3 velocity;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
   
    [SerializeField]GameObject FlyEffect;
    [SerializeField] public bool isGrounded;
    [SerializeField] public float groundCheckDistance;
    [SerializeField] public LayerMask groundMask;
    [SerializeField] public float gravity;
    [SerializeField] public float jumpheight;
    public float timer;
    HealthBar HB;
    public bool Falling;
    public bool Jumping;
    public bool IsFlying;
    public GameObject Recoverimage;
    public AudioClip RocketSound;

    private void Start()
    {
        Cc= GetComponent<CharacterController>();
        anim=GetComponentInChildren<Animator>();
        timer = 0;
        HB = GetComponent<HealthBar>();
        FlyEffect.SetActive(false);
        IsFlying=false;
    }

// Update is called once per frame
    void Update()
    {
        if(atk.CanMove&&allway.Istransform==false&&Mainmenu.StartGame==false&&HB.health>0)
            Movement();
        if(IsFlying)
            FlyEffect.SetActive(true);
        else
            FlyEffect.SetActive(false);
        if(isGrounded)
            FlyEffect.SetActive(false);
    }
    void Movement(){

        isGrounded =Physics.CheckSphere(transform.position,groundCheckDistance,groundMask);
        if(!isGrounded)
        {
            anim.SetBool("IsFalling",true);
            if(Jumping==true)
                anim.SetBool("IsJumping",false);
            Falling=true;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(atk.HB.currentstamina>20)
                {
                    AudioSource ac = GetComponent<AudioSource>();
                    ac.PlayOneShot(RocketSound);
                    gravity=2;
                    IsFlying=true;
                    StartCoroutine(CloseEffect());
                    atk.HB.CostStamina(20);
                }
            }
        }
        if(isGrounded &&velocity.y<0)
        {
            velocity.y=-2f;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal,0f,vertical).normalized;
        if(direction != Vector3.zero && allway.Istransform==false&&Mainmenu.StartGame==false)
        {
            anim.SetFloat("State",1f,0.1f,Time.deltaTime);
            HB.healling=false;
            Recoverimage.SetActive(false);
            timer-=timer;
            anim.SetBool("Relax",false);
            IsMusic=true;
        }
        else if(direction == Vector3.zero)
        {
            Idle();
            if(allway.Istransform==false&&Mainmenu.StartGame==false)
                timer+=Time.deltaTime;
            if(timer>10)
            {
                if(IsMusic==true)
                {
                    AudioSource ac = GetComponent<AudioSource>();
                    ac.PlayOneShot(relaxsound);
                    IsMusic=false;
                }
                Recoverimage.SetActive(true);
                HB.healling=true;
                HB.Relax();
            }
        }
        if(atk.IsShootingMode==true)
        {
            Vector3 moveDirection = new Vector3(horizontal,0,vertical);
            moveDirection=transform.TransformDirection(moveDirection);
            Cc.Move(moveDirection*speed*Time.deltaTime);
        }
        else if(atk.IsShootingMode==false)
        {
            if(direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnSmoothVelocity,turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f,angle,0f);
                Vector3 moveDirection = Quaternion.Euler(0f,targetAngle,0f) * Vector3.forward;
                //update movement
                Cc.Move(moveDirection* speed * Time.deltaTime);
            }
        }
        if(atk.CanMove==false)
        {
            isGrounded=false;
            StartCoroutine(ResetGrounded());
        }
        if(isGrounded)
        {
            if(Falling==true)
            {
                anim.SetBool("IsGround",true);
                anim.SetBool("IsFalling",false);
                Falling=false;
            }
            if( Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
                timer-=timer;
                IsMusic=true;
                isGrounded=false;
                anim.SetBool("IsGround",false);
                anim.SetBool("IsFalling",false);
            }
        }
        //update gravity
        velocity.y+= gravity * Time.deltaTime;
        Cc.Move(velocity*Time.deltaTime);
        
    }
    private void Idle()
    {
        anim.SetFloat("State",0f,0.1f,Time.deltaTime);
    }
    private void Jump()
    {   
        velocity.y=Mathf.Sqrt(jumpheight*-2*gravity);
        anim.SetBool("IsJumping",true);
        Jumping=true;
    }
    IEnumerator ResetGrounded(){
        yield return new WaitForSeconds(2.5f);
        isGrounded=true;
    }
    IEnumerator CloseEffect(){
    yield return new WaitForSeconds(2f);
    IsFlying=false;
    gravity=-9.81f;
}
}
