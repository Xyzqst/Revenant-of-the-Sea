using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{

//Variables

public float speed = 4f;
public Animator anim;

private Rigidbody2D rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(x,y) * speed;

        
        anim.SetFloat("horizontal", x);
        anim.SetFloat("vertical", y);

        rb.linearVelocity = movement;
       
    }
}
