using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{

//Variables

public float speed = 5f;

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

        rb.linearVelocity = movement;
        Debug.Log(x);
        Debug.Log(y);
    }
}
