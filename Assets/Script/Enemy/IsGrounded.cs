using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    private Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        isGrounded =Physics.CheckSphere(transform.position,groundCheckDistance,groundMask);
        if(isGrounded &&velocity.y<0)
        {
            velocity.y=-2f;
        }
        velocity.y+= gravity * Time.deltaTime;
    }
}
