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
    public GameObject healthBar; // Saðlýk çubuðu GameObject referansý
    private HealthBar healthBarScript; // HealthBar script referansý

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animatorManager = GetComponent<AnimatorManager>();
        isDead = false;
        isHit = false;
        currentHealth = maxHealth;

        // HealthBar scriptine eriþim saðla
        healthBarScript = healthBar.GetComponent<HealthBar>();
        healthBarScript.SetMaxHealth(maxHealth); // Maksimum saðlýk deðerini ayarla
    }

    private void Update()
    {
        isHit = animatorManager.animator.GetBool("isHit");
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBarScript.SetHealth(currentHealth); // Saðlýk çubuðunu güncelle
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

