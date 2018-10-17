using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.SystemControls;
using UnityEngine.UI;

public class Player : Character25D
{
    bool flipedRight = true;
    bool flipedLeft = false;

    public GameObject HealthBarGO;
    public Image HealthBar;
    

    
    private float healthValue;
    private float invulRemaining;
    [SerializeField, Range(0,2)]
    private float invulTime;
    [SerializeField]
    private MeshRenderer bodyRenderer;
    [SerializeField, Range(0, 5)]
    private float gravityMultiplier;

    protected override void Awake()
    {
        base.Awake();
        HealthBarGO.SetActive(true);
        healthValue = 100f;
        Physics.gravity = new Vector3(0f, -gravityMultiplier * 9.8f, 0f);
    }

    protected override void Move()
    {
        base.Move();
        //anim.SetFloat("Move", Mathf.Abs(Axis.y));
        anim.SetBool("Walking", (Controllers.GetJoystick(1, 1).x != 0f));
    }
    

    protected override void Jump()
    {
        anim.SetBool("Jumping", !isGrounding);
        if (Controllers.GetButton(1,"A",1) & isGrounding & (Controllers.GetJoystick(1,1).y >= -.5))
        {
            rb.velocity = Vector3.zero;
            base.Jump();
            //insertar aqui la animacion de brinco
            //anim.SetTrigger("Jump");
        }
    }

    protected override void StartSlide()
    {
        if(Controllers.GetButton(1, "A", 1) & (Controllers.GetJoystick(1, 1).y < -.5) 
            & isGrounding & !isSliding)
        {
            base.StartSlide();
            //insertar aqui la animacion de slide
            //anim.SetTrigger("Jump");
            anim.SetBool("Sliding", true);
        }
    }

    protected override void RotateY()
    {
        //cuando se mueva por primera vez la palanca o algo ya no se no tengo ganas de segur pensando sin la libreria de los controles correcta.
        
        if (Controllers.GetJoystick(1,1).x > 0 & !flipedRight )
        {
            flipedRight = true;
            flipedLeft = false;
            base.RotateY();
            //Debug.Log("Right Rotation");
        }
        else if(Controllers.GetJoystick(1,1).x < 0 & !flipedLeft)
        {
            flipedRight = false;
            flipedLeft = true;
            base.RotateY();
            //Debug.Log("Left Rotation");
        }
        
    }

    protected override void EndSlide()
    {
        if(slideTimer >= maxSlideTime & isSliding || !isGrounding)
        {
            base.EndSlide();
            //insertar aqui la animacion de slide (fin)
            //anim.SetTrigger("Jump");
            anim.SetBool("Sliding", false);
        }

    }

    protected override void HealthRefresher()
    {
        /*
        if (Controllers.GetButton(1, "B", 0))
        {
            healthValue -= 1f;
        }
        */
        HealthBar.fillAmount = healthValue / 100f;
        if(healthValue <= 0)
        {
            MenuController.deadScreen = true;
            this.gameObject.SetActive(false);
        }
    }

    protected override void InvulRefresher()
    {
        if(invulRemaining > 0)
        {
            invulRemaining -= Time.deltaTime;
        }
        
    }
    //siguiente codigo no es mio
    private IEnumerator Blink(float waitTime)
    {
        //print("Coorutine with times: " + Time.time + " -- " );
        float endTime = Time.time + waitTime;
        while (Time.time < endTime)
        {
            bodyRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            bodyRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.collider.tag == "Enemy" & invulRemaining <= 0)
        if ((collision.collider.tag == "Enemy" & invulRemaining <= 0 ) || (Controllers.GetButton(1,"Y",0) & invulRemaining <= 0))
        {
            //print(invulRemaining);
            healthValue -= 25f;
            invulRemaining = invulTime;
            StartCoroutine(Blink(invulRemaining));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pit")
        {
            healthValue = 0;
            HealthRefresher();
        }
    }
}
