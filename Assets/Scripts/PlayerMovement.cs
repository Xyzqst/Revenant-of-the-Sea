using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;


public class PlayerMovement : MonoBehaviour
{

//https://www.youtube.com/watch?v=MeRdj89Oetc - reference for animation 


//Variables 
public float speed = 4f;
public Animator anim;
private Rigidbody2D rb;

public Vector2 lastMoveDirection = Vector2.down;

public int gemCounter = 0;

public int gemtotal = 5;

public TMP_Text counterText;

public GameObject doorPuzzle;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        counterText.text = "Gem collected:" + gemCounter + "/" + gemtotal ;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   

        if (Input.GetKeyDown(KeyCode.Space) )
        {
           Debug.Log("dash");
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

    //this on trigger is how the 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gem") && collision.gameObject.activeSelf == true)
        {
            collision.gameObject.SetActive(false);
            gemCounter += 1;
            counterText.text = "Gem collected:" + gemCounter + "/" + gemtotal ;
        }

        if(gemCounter == gemtotal)
        {
            counterText.text = "All Gems collected";
            doorPuzzle.gameObject.SetActive(false);
            
        }
    }


}
