using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

//Variables
public float speed = 4f;
public Animator anim;
private Rigidbody2D rb;

public Vector2 lastMoveDirection = Vector2.down;

public float dashSpeed = 15f;
public float dashCooldown = 1f;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   

     

    
        if (Input.GetKeyDown(KeyCode.Space) )
        {
           
        }

        //Movement 
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;

        rb.linearVelocity = new Vector2(movement.x,movement.y) * speed;
        anim.SetFloat("speed",movement.magnitude);

        if (!anim.GetBool("isAttacking"))
        {
            if(movement != Vector2.zero)
            {
                lastMoveDirection = movement;
            }
            
            anim.SetFloat("horizontal",  lastMoveDirection.x);
            anim.SetFloat("vertical",  lastMoveDirection.y);
     
        }
    

       
    }



void dash()
    {
        
    }
}
