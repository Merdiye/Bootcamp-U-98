using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 10f;
    public bool isDead;
    public bool isHit;
    AnimatorManager animatorManager;
    Animator animator;
    public GameObject healthBar; // Sa�l�k �ubu�u GameObject referans�
    private HealthBar healthBarScript; // HealthBar script referans�

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animatorManager = GetComponent<AnimatorManager>();
        isDead = false;
        isHit = false;
        currentHealth = maxHealth;

        // HealthBar scriptine eri�im sa�la
        healthBarScript = healthBar.GetComponent<HealthBar>();
        healthBarScript.SetMaxHealth(maxHealth); // Maksimum sa�l�k de�erini ayarla
    }

    private void Update()
    {
        isHit = animatorManager.animator.GetBool("isHit");
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBarScript.SetHealth(currentHealth); // Sa�l�k �ubu�unu g�ncelle
        StartCoroutine(HitAnimation());

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            animator.SetBool("isDead", true);
            Debug.Log("Player is dead!");
            animatorManager.PlayTargetAnimation("Death", true);
        }
    }

    private IEnumerator HitAnimation()
    {
        animator.SetBool("isHit", true);
        animatorManager.PlayTargetAnimation("GetHit", true);
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("isHit", false);
    }
}

