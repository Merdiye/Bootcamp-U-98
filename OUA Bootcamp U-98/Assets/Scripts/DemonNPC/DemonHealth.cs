using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonHealth : MonoBehaviour
{
    public float maxHealth = 5;
    private float currentHealth;
    public Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
    }

  public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        StartCoroutine(HitAnimation());
        if(currentHealth <= 0)
        {
             Die();
        }
            
        
    }


    private IEnumerator HitAnimation()
    {
        animator.SetBool("isHitted", true);
        yield return new WaitForSeconds(1.0f); 
        animator.SetBool("isHitted", false);
    }

    void Die()
    {
        animator.SetBool("isDead", true);
        Debug.Log("Demon is dead!");


    }
    
}
