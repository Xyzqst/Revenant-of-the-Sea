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


    
    public int damage = 1;


    public Transform attackPoint;
    public Vector2 attackArea = new Vector2(0.8f, 1.8f);
    public float attackDistance = 0.8f;
    public float attackRange = 2f;


    private float attackCooldownTimer;
    public float attackCooldown = 2;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        changeState(EnemyState.Idle);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(attackCooldownTimer > 0)
        {
            attackCooldownTimer -=Time.deltaTime;
        }

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

        if (player == null)
        {
            changeState(EnemyState.Idle);
            return;
        }

        Vector2 moveDirection = (player.position - transform.position).normalized;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Player is within attack range
        if (distanceToPlayer <= attackRange)
        {
            // Stop moving while inside attack range
            rb.linearVelocity = Vector2.zero;

            // Only attack when cooldown is finished
            if (attackCooldownTimer <= 0)
            {
                attacking(moveDirection);

                attackCooldownTimer = attackCooldown;

                changeState(EnemyState.Attacking);
            }

            return;
        }

        // Player is outside attack range, so chase
        anim.SetFloat("Speed", moveDirection.magnitude);
        anim.SetFloat("Horizontal", moveDirection.x);
        anim.SetFloat("Vertical", moveDirection.y);

        rb.linearVelocity = moveDirection * speed;
        
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
        Collider2D[] hit = Physics2D.OverlapBoxAll(attackPoint.position,attackArea,attackPoint.eulerAngles.z,playerLayer);

        foreach (Collider2D collider in hit)
        {
            PlayerHealth playerHealth = collider.GetComponentInParent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.changeHealth(-damage);
            }
        }
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
