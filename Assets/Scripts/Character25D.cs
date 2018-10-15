using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.SystemControls;
using GameCore.SystemGround;
using GameCore.SystemMovements;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]


public abstract class Character25D : MonoBehaviour
{
    [SerializeField, Range(0, 10)]
    protected float moveSpeed;
    [SerializeField, Range(0, 15)]
    protected float jumpForce;
    [SerializeField, Range(0, 10)]
    protected float dashForce;


    protected Animator anim;
    protected Rigidbody rb;
    [SerializeField]
    protected GameObject standingCollider;
    [SerializeField]
    protected GameObject slidingCollider;

    [SerializeField]
    Ground ground;

    protected bool isGrounding;
    protected bool isSliding;
    protected float slideTimer;
    [SerializeField, Range(0,4)]
    protected float maxSlideTime;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        slidingCollider.SetActive(false);
        isSliding = false;
        slideTimer = 0f;
    }

    private void Update()
    {
        Move();
        RotateY();
        StartSlide();
        EndSlide();
        Jump();
        HealthRefresher();
        InvulRefresher();


        isGrounding = ground.isGrounding(transform);
        if (isSliding)
            slideTimer += Time.deltaTime;
    }

    protected virtual void Move()
    {
        Movement.Move2D(transform, moveSpeed);
    }

    protected virtual void Jump()
    {
        Movement.JumpUp(rb, jumpForce);
        isSliding = false;
    }

    protected virtual void StartSlide()
    {
        Movement.StartSlide(rb, standingCollider, slidingCollider
            , new Vector3(0f,0f,transform.forward.z), dashForce);
        isSliding = true;
        slideTimer = 0f;
    }
    protected virtual void EndSlide()
    {
        Movement.EndSlide(rb, standingCollider, slidingCollider, transform.forward, dashForce);
        isSliding = false;
    }

    protected virtual void RotateY()
    {
        Movement.RotateY(transform);
    }
    

    private void OnDrawGizmosSelected()
    {
        ground.DrawRay(transform);
    }

    protected Vector2 Axis
    {
        get
        {
            return Controllers.GetJoystick(1,1);
        }
    }

    protected virtual void HealthRefresher()
    {

    }

    protected virtual void InvulRefresher()
    {

    }
}
