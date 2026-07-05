using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{

//Variables
public float speed = 4f;
public Animator anim;
private Rigidbody2D rb;

private Vector2 lastMoveDirection = Vector2.down;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

        if(movement != Vector2.zero)
        {
            lastMoveDirection = movement;
        }
          
        anim.SetFloat("horizontal",  lastMoveDirection.x);
        anim.SetFloat("vertical",  lastMoveDirection.y);
        anim.SetFloat("speed",movement.magnitude);

        rb.linearVelocity = new Vector2(movement.x,movement.y) * speed;
       
    }
}
