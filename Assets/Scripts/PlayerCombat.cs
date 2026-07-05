
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator anim;
    public Rigidbody2D rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
          if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("CLICKED");
            attack();
             
        }
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
            Debug.Log("Attack Right!");
        }

        else
        {
            Debug.Log("Attack Left!");
        }

    }
    else
    {
       
        if (direction.y > 0)
        {
            Debug.Log("Attack Up!");
        }
        else
        {
            Debug.Log("Attack Down!");
        }
    }
    }
    
}
