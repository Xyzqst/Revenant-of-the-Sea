using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int currentHealth;
    public int maxHealth = 5;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damageHealth(int amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
