
using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator anim;
    public Rigidbody2D rb;

    public float attackCooldown = 0.3f;
    private float timer;
    public Vector2 lastattackDirection;
    Vector2 direction = new Vector2();




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {   
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

          if (Input.GetMouseButtonDown(0) && timer <=0 && !anim.GetBool("isAttacking"))
        {   
           
            anim.SetBool("isAttacking", true);
          
            timer = attackCooldown;
            attack();

             
        }
    }   

    public void finishAttack()
    {
        anim.SetBool("isAttacking", false);
    }

    //player attack method
    void attack()
    {
     
    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    direction = mousePosition - (Vector2)transform.position;

    PlayerMovement lastmove = GetComponent<PlayerMovement>();

    if (Mathf.RoundToInt(Mathf.Abs(direction.x)) > Mathf.Abs(direction.y))
    {
       
        if (direction.x > 0)
        {
            anim.SetFloat("horizontal", 1f);
            anim.SetFloat("vertical", 0f);
           lastmove.lastMoveDirection = new Vector2(1f , 0f);
            Debug.Log("attack RIGHT");
        }

        else
        {
            anim.SetFloat("horizontal", -1f);
            anim.SetFloat("vertical", 0f);
            lastmove.lastMoveDirection = new Vector2(-1f , 0f);
            Debug.Log("attack LEFT");
        }

    }
    else
    {
       
        if (direction.y > 0)
        {
            anim.SetFloat("horizontal", 0f);
            anim.SetFloat("vertical", 1f);
            lastmove.lastMoveDirection = new Vector2(0f , 1f);
            Debug.Log("attack up");
        }
        else
        {
            anim.SetFloat("horizontal", 0f);
            anim.SetFloat("vertical", -1f);
            lastmove.lastMoveDirection = new Vector2(0f , -1f);
            Debug.Log("attack down");

        }


        
    }
    

    
    }
    
}
