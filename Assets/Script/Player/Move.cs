using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float movespeed;

    [SerializeField]public Attack atk;
    private Vector3 moveDirection;
    private Vector3 velocity;


    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    [SerializeField] private float jumpheight;

    private CharacterController controller;
    private Animator anim;

    float timer;
    HealthBar HB;
    bool healling;

    private void Start()
    {
        controller= GetComponent<CharacterController>();
        anim=GetComponentInChildren<Animator>();
        timer = 0;
        HB = GetComponent<HealthBar>();
        healling=false;
    }

    private void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        isGrounded =Physics.CheckSphere(transform.position,groundCheckDistance,groundMask);
        
//remove gravity
        if(isGrounded &&velocity.y<0)
        {
            velocity.y=-2f;
        }
        float moveZ= Input.GetAxis("Vertical");
        float MoveX=Input.GetAxis("Horizontal");

        moveDirection = new Vector3 (MoveX,0,moveZ).normalized;
        moveDirection = transform.TransformDirection(moveDirection);

        if(isGrounded){            

            if (moveDirection!=Vector3.zero)
            {   
                healling=false;
                timer-=timer;
                if(moveZ!=0 && MoveX==0|| moveZ==0 && MoveX!=0){
                    anim.SetFloat("Chosen",0f,0.1f,Time.deltaTime);
                    if(moveZ!=0){
                        anim.SetFloat("Chosen1",0f,0.1f,Time.deltaTime);
                        if(moveZ<0)
                        {
                            anim.SetFloat("FB",1f,0.1f,Time.deltaTime);
                        }
                        else if(moveZ>0)
                        {
                            anim.SetFloat("FB",0f,0.1f,Time.deltaTime);
                        }
                    }
                }
                    if(MoveX!=0){
                        anim.SetFloat("Chosen1",1f,0.1f,Time.deltaTime);
                        if(MoveX<0){
                            anim.SetFloat("LR",0f,0.1f,Time.deltaTime);
                        }
                        else if(MoveX>0)
                        {
                            anim.SetFloat("LR",1f,0.1f,Time.deltaTime);
                        }
                    }
                if(MoveX!=0 && moveZ!=0)
                {
                    anim.SetFloat("Chosen",1f,0.1f,Time.deltaTime);
                    if(MoveX<0)
                    {
                        anim.SetFloat("Chosen2",1f,0.1f,Time.deltaTime);
                        if(moveZ<0)
                        {
                            anim.SetFloat("LFB",1f,0.1f,Time.deltaTime);
                        }
                        else if(moveZ>0)
                        {
                            anim.SetFloat("LFB",0f,0.1f,Time.deltaTime);
                        }
                    }
                    if(MoveX>0)
                    {
                        anim.SetFloat("Chosen2",0f,0.1f,Time.deltaTime);
                        if(moveZ<0)
                        {
                            anim.SetFloat("RFB",1f,0.1f,Time.deltaTime);
                        }
                        else if(moveZ>0)
                        {
                            anim.SetFloat("RFB",0f,0.1f,Time.deltaTime);
                        }
                    }
                }
            }
            else if (moveDirection==Vector3.zero)
            {
                Idle();
                timer+=Time.deltaTime;
                if(timer>10)
                    healling=true;
                if(healling)
                    HB.Relax();
                
            }
            moveDirection*=movespeed;
            if( Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

//update speed

        controller.Move(moveDirection* Time.deltaTime);


//update gravity
        velocity.y+= gravity * Time.deltaTime;
        controller.Move(velocity*Time.deltaTime);

    }
    private void Idle()
    {
        anim.SetFloat("Chosen",0.5f,0.1f,Time.deltaTime);
    }
    private void Jump()
    {   
        velocity.y=Mathf.Sqrt(jumpheight*-2*gravity);
        anim.SetTrigger("IsJumping");
    }
}
