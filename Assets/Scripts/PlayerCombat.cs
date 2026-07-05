using NUnit.Framework.Internal;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator anim;
    public Test = 

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
             Debug.Log(mouseposition);
        }
    }

    //player attack method
    void attack()
    {
        Vector3 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
