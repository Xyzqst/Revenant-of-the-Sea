using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
  public int maxHealth;
  public int currentHealth;


    public void changeHealth(int amount)
    {
        currentHealth += amount;

        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
