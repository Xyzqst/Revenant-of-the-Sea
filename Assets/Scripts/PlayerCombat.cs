
using System;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCombat : MonoBehaviour
{
        //youtibe references when making the script:
        //https://www.youtube.com/watch?v=hlZGeyQjhJI&list=PLSR2vNOypvs5yLsbqZc0e6RdqNnP1eGIc&index=14
        //https://www.youtube.com/watch?v=6WyQEhXq57I&list=PLSR2vNOypvs5yLsbqZc0e6RdqNnP1eGIc&index=17
        //https://www.youtube.com/watch?v=MUO7_CaHHbc&list=PLSR2vNOypvs5yLsbqZc0e6RdqNnP1eGIc&index=16
       

        //https://www.youtube.com/watch?v=VgUYCOLFgBk&list=PLkeulJG4vyDcbMMo1mTBnjBm_GSlMvAFO&index=10

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

        //this code checks if the player has clicked the left mouse button, and the attack cooldown has finished, and the player is not already attacking.
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

          if (Input.GetMouseButtonDown(0) && timer <=0 && !anim.GetBool("isAttacking"))
        {   
           
           //afterwards i tell the animator that the player is attacking so it can play the attack animation as well as run the attack method as well as reset the attack cooldwon timer
            anim.SetBool("isAttacking", true);
          
            timer = attackCooldown;
            attack();

             
        }
    }   

    //a method to used in the animation as a event to tell the animmator that the attack is finished to play the approporiate animation when the player is not attacking anymroe 
    public void finishAttack()
    {
        anim.SetBool("isAttacking", false);
    }

    //player attack method
    void attack()
    {
     
     //this code converts the mouse position from screen into the games world space
    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    // this alculates the direction from the player towards the mouse cursor.
    direction = mousePosition - (Vector2)transform.position;

      // Get the PlayerMovement component so the player's last movement direction can be updated to match the attack direction in the later code.
    PlayerMovement lastmove = GetComponent<PlayerMovement>();



    //the basic logic of my combat script is that when the player clciks on the scrren , it has a X and Y value which can be positive or negtaive .
    //Finding and comparing  the value of which axis is larger helps me run my attack script in the 4 different direction 
    //This is because when the player clicks on either up or right , it will be a positive value , and the down and left would be a negative vali.
    //I then compare which of the axis is larger so i can tell the scrit to attack in the desired location , for example if the player clciks on the coordinates (5,3)
    //this means the pplayers mouse position is closer towards the X rather than the Y , which in terms allows me to attack right instead of up.

    //absoulte value the x and y axis so i can clearly tell which of the axis are larger so that the engative values wont intefer
    if (Mathf.RoundToInt(Mathf.Abs(direction.x)) > Mathf.Abs(direction.y))
    {
       
       
        if (direction.x > 0)
        {   
            //attack right
            //these tell the animator whcih direcction the animation shoudl be facing to play the according animation in the blend tree
            anim.SetFloat("horizontal", 1f);
            anim.SetFloat("vertical", 0f);
            //based on the players last movment direction , i make them face the curernt attack position which in this case right 
            lastmove.lastMoveDirection = new Vector2(1f , 0f);
            attackpoint.localPosition = Vector2.right * attackdistance; //sets the attack point to the right and apply a distacne for the hitbox detection 
            attackpoint.localRotation = Quaternion.Euler(0, 0, 0); // not really needed for left and right attack but needed for up and down so that the attack box is horizontal and not vertical
           
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
    
    //this method is ussed in the animtion event to ddeel damage based on a frame
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
