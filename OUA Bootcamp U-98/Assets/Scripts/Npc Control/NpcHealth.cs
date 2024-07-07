using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHealth : MonoBehaviour
{
    public bool isDead;
    public bool isHit;
    public float maxHealth;
    private float currentHealth;
    public Animator animator;
    public Collider colliderBeforeDeath; // De�i�tirildi: SphereCollider yerine Collider kullan�ld�
    public Collider colliderAfterDeath;  // De�i�tirildi: CapsuleCollider yerine Collider kullan�ld�
    NpcAI npc;
    public healthBar healthBar;

    private void Awake()
    {
        npc = GetComponent<NpcAI>();
        isDead = false;
        isHit = false;
        animator = GetComponent<Animator>();
        // Bu �ekilde FindObjectOfType yerine GetComponent kullan�lmas� tavsiye edilir
        colliderBeforeDeath = GetComponent<SphereCollider>();
        colliderAfterDeath = GetComponent<CapsuleCollider>();

        // Ba�lang��ta, �l�mden sonraki collider'� devre d��� b�rak�yoruz
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
        healthBar.SetMaxHealth((int)maxHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        StartCoroutine(HitAnimation());
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Debug.Log("NPC is dead!");
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
        healthBar.SetHealth((int)currentHealth);
    }

    private IEnumerator HitAnimation()
    {
        animator.SetBool("isHit", true);
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("isHit", false);
    }
}