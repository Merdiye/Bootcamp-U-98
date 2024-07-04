using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonHealth : MonoBehaviour
{
    public float maxHealth = 5;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

  public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Demon is dead!");
     

    }
}
