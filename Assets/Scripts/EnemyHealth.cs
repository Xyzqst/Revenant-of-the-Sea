using UnityEngine;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour
{

    public int currentHealth;
    public int maxHealth = 5;
   
    
     public Slider enemyHealthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        currentHealth = maxHealth;

        enemyHealthBar.gameObject.SetActive(currentHealth < maxHealth);
        enemyHealthBar.value = currentHealth;
        enemyHealthBar.maxValue = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        enemyHealthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.1f);


    }

    public void damageHealth(int amount)
    {
        
        currentHealth -= amount;
        enemyHealthBar.gameObject.SetActive(true);
        enemyHealthBar.value = currentHealth;

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
