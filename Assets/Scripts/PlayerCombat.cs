
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator anim;
    public Rigidbody2D rb;

    public float attackCooldown = 1;
    private float timer;
    private Vector2 lastMoveDirection = Vector2.down;




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
            rb.linearVelocity = Vector2.zero;
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
    Vector2 direction = mousePosition - (Vector2)transform.position;

   
    if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
    {
       
        if (direction.x > 0)
        {
            anim.SetFloat("horizontal", 1f);
            anim.SetFloat("vertical", 0f);
        }

        else
        {
            anim.SetFloat("horizontal", -1f);
            anim.SetFloat("vertical", 0f);
        }

    }
    else
    {
       
        if (direction.y > 0)
        {
            anim.SetFloat("horizontal", 0f);
            anim.SetFloat("vertical", 1f);
        }
        else
        {
            anim.SetFloat("horizontal", 0f);
            anim.SetFloat("vertical", -1f);

        }

        
    }

        lastMoveDirection = new Vector2(anim.GetFloat("horizontal"),anim.GetFloat("vertical"));
    
    }
    
}
