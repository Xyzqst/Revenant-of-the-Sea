using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Transform player;

    public Animator anim;

    public EnemyState currentState;

    public LayerMask playerLayer;


    public Transform attackPoint;
    public Vector2 attackArea = new Vector2(0.7f, 1.6f);
    public float attackDistance = 0.7f;
    public float attackRange = 1.7f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        changeState(EnemyState.Idle);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   

        switch (currentState)
        {
            case EnemyState.Idle:
                Idle();
                break;

            case EnemyState.Chasing:
                Chase();
                break;

            case EnemyState.Attacking:
                break;
        }

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {   
            if(player == null)
            {
                player = collision.transform;
            }

            changeState(EnemyState.Chasing);
        }
    }


    void changeState(EnemyState newstate)
    {
        currentState = newstate;
    }

    void Idle()
    {
        rb.linearVelocity = Vector2.zero;
        anim.SetFloat("Speed", 0f);
    }

    void Chase()
    {   
        Vector2 moveDirection = (player.position - transform.position).normalized;

        if(Vector2.Distance(transform.position, player.transform.position) < attackRange)
        {   
           
            rb.linearVelocity = Vector2.zero;
            attacking(moveDirection);
            changeState(EnemyState.Attacking);
        }

        else
        {

            anim.SetFloat("Speed", moveDirection.magnitude);
            anim.SetFloat("Horizontal",moveDirection.x);
            anim.SetFloat("Vertical", moveDirection.y);
            rb.linearVelocity = moveDirection * speed;
        }

        
    }

    void attacking(Vector2 direction)
    {   
        anim.SetBool("isAttacking", true);

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                // Attack Right
                anim.SetFloat("Horizontal", 1f);
                anim.SetFloat("Vertical", 0f);
                attackPoint.localPosition = Vector2.right * attackDistance;
                attackPoint.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                // Attack Left
                anim.SetFloat("Horizontal", -1f);
                anim.SetFloat("Vertical", 0f);
                attackPoint.localPosition = Vector2.left * attackDistance;
                attackPoint.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            if (direction.y > 0)
            {
                // Attack Up
                anim.SetFloat("Horizontal", 0f);
                anim.SetFloat("Vertical", 1f);
                attackPoint.localPosition = Vector2.up * attackDistance;
                attackPoint.localRotation = Quaternion.Euler(0, 0, -90);
            }
            else
            {
                // Attack Down
                anim.SetFloat("Horizontal", 0f);
                anim.SetFloat("Vertical", -1f);
                attackPoint.localPosition = Vector2.down * attackDistance;
                attackPoint.localRotation = Quaternion.Euler(0, 0, 90);
            }
        }

    }

    public void dealDamage()
    {
        
    }

    public void finishAttacking()
    {
        anim.SetBool("isAttacking", false);
        changeState(EnemyState.Chasing);
    }

  

 
}

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
}
