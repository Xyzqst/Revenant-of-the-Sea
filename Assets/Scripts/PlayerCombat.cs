
using System;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCombat : MonoBehaviour
{

    public Animator anim;
    public Rigidbody2D rb;

    public float attackCooldown = 0.3f;
    private float timer;

    Vector2 direction = new Vector2();

    public Transform attackpoint;
    public float attackdistance = 0.2f;

    public Vector2 attackArea = new Vector2(1.7f,1.6f);
    public LayerMask enemyLayer;
    public int damage = 1;




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
            //attack right
            anim.SetFloat("horizontal", 1f);
            anim.SetFloat("vertical", 0f);
            lastmove.lastMoveDirection = new Vector2(1f , 0f);
            attackpoint.localPosition = Vector2.right * attackdistance;
            attackpoint.localRotation = Quaternion.Euler(0, 0, 0);
           
            Debug.Log("attack RIGHT");
        }

        else
        {   
            //attack left
            anim.SetFloat("horizontal", -1f);
            anim.SetFloat("vertical", 0f);
            lastmove.lastMoveDirection = new Vector2(-1f , 0f);
            Debug.Log("attack LEFT");
           attackpoint.localPosition = Vector2.left * attackdistance;
            attackpoint.localRotation = Quaternion.Euler(0, 0, 0);

        }

    }
    else
    {
       
        if (direction.y > 0)
        {
            //attack up
            anim.SetFloat("horizontal", 0f);
            anim.SetFloat("vertical", 1f);
            lastmove.lastMoveDirection = new Vector2(0f , 1f);
            Debug.Log("attack up");
            attackpoint.localPosition = Vector2.up * attackdistance;
            attackpoint.localRotation = Quaternion.Euler(0, 0, -90);
         

        }
        else
        {   
            //attack down
            anim.SetFloat("horizontal", 0f);
            anim.SetFloat("vertical", -1f);
            lastmove.lastMoveDirection = new Vector2(0f , -1f);
            attackpoint.localPosition = Vector2.down * attackdistance;
            attackpoint.localRotation = Quaternion.Euler(0, 0, 90);

            Debug.Log("attack down");

        }

        
    }
    

    
    }
    
    public void dealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(attackpoint.position , attackArea , attackpoint.eulerAngles.z, enemyLayer);

        foreach(Collider2D enemy in enemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

            if(enemy.isTrigger) continue;

            if (enemyHealth != null)
            {
                enemyHealth.damageHealth(damage);
            }
        }

    }

  
    
}
