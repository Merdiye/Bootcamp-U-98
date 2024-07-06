using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonHealth : MonoBehaviour
{
    public bool isDead;
    public bool isHit;
    public float maxHealth;
    private float currentHealth;
    public Animator animator;
    public Collider colliderBeforeDeath; // Deðiþtirildi: SphereCollider yerine Collider kullanýldý
    public Collider colliderAfterDeath;  // Deðiþtirildi: CapsuleCollider yerine Collider kullanýldý
    DemonNPC npc;

    private void Awake()
    {
        npc = GetComponent<DemonNPC>();
        isDead = false;
        isHit = false;
        animator = GetComponent<Animator>();
        // Bu þekilde FindObjectOfType yerine GetComponent kullanýlmasý tavsiye edilir
        colliderBeforeDeath = GetComponent<SphereCollider>();
        colliderAfterDeath = GetComponent<CapsuleCollider>();

        // Baþlangýçta, ölümden sonraki collider'ý devre dýþý býrakýyoruz
        if (colliderAfterDeath != null)
        {
            colliderAfterDeath.enabled = false;
        }
    }

    private void Update()
    {
        isHit = animator.GetBool("isHit");
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        StartCoroutine(HitAnimation());
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Debug.Log("Demon is dead!");
            animator.CrossFade("Death", 0.2f);
            npc._agent.baseOffset = 0f;
            
            if (colliderBeforeDeath != null)
            {
                colliderBeforeDeath.enabled = false;
            }
            if (colliderAfterDeath != null)
            {
                colliderAfterDeath.enabled = true;
            }
            
        }
    }

    private IEnumerator HitAnimation()
    {
        animator.SetBool("isHit", true);
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("isHit", false);
    }
}

