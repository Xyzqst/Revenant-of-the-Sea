using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    //Youtubbe videos i refered to when making the scripts
    //https://www.youtube.com/watch?v=cmh73c3cnBo&list=PLSR2vNOypvs5yLsbqZc0e6RdqNnP1eGIc&index=10 for health system 

    //https://www.youtube.com/watch?v=uR3hFjvyZYw&t=123s for slider value



  public int maxHealth;
  private int currentHealth;

  public Slider healthBar;


    void Start()
    {
        //initililises the players starting health to their maximum health so that the players starts with full health 
        currentHealth = maxHealth;
        
        // setting the values for the sldier bar in my unity canvas so the slider can be affected based on the players current health 
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    //method to update the health of the player based on the amount given in its paramters 
    public void changeHealth(int amount)
    {
        //this script allows for the player health to increase when its a postive value but a negative value makes it decreses the current health 
        
        currentHealth += amount;
        healthBar.value = currentHealth; //updates the slider bar value to match and display the current player health 
       

        //if the player helath reaches 0 , make the player dissapear to simulate the plaeyr dying 
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
