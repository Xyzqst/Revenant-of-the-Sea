using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
  public int maxHealth;
  private int currentHealth;

  public Slider healthBar;


    void Start()
    {
        currentHealth = maxHealth;

        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }


    public void changeHealth(int amount)
    {
        currentHealth += amount;
        healthBar.value = currentHealth;
       

        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
