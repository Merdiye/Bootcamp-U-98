using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NpcHealth : MonoBehaviour
{
    public UnityEvent missionAddKill;
    public bool isDead;
    public bool isHit;
    public float maxHealth;
    private float currentHealth;
    public Animator animator;
    public Collider colliderBeforeDeath; // De�i�tirildi: SphereCollider yerine Collider kullan�ld�
    public Collider colliderAfterDeath;  // De�i�tirildi: CapsuleCollider yerine Collider kullan�ld�
    NpcAI npc;
    public HealthBar healthBar;
    public float offsetAfterDeath = 0f;

    private void Awake()
    {
        npc = GetComponent<NpcAI>();
        isDead = false;
        isHit = false;
        animator = GetComponent<Animator>();
        // Bu �ekilde FindObjectOfType yerine GetComponent kullan�lmas� tavsiye edilir
        colliderBeforeDeath = GetComponent<SphereCollider>();
        colliderAfterDeath = GetComponent<CapsuleCollider>();
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
        healthBar.SetMaxHealth((float)maxHealth);
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
            npc._agent.baseOffset = offsetAfterDeath;

            if (colliderBeforeDeath != null)
            {
                colliderBeforeDeath.enabled = false;
            }
            if (colliderAfterDeath != null)
            {
                colliderAfterDeath.enabled = true;
            }
            Invoke(nameof(DestroyEnemy), 5f);
            if (Mission.Instance != null)
            {
                Debug.Log("Kill eklendi");
                Mission.Instance.addKill();
            }
        }
        healthBar.SetHealth(currentHealth);
    }

    private IEnumerator HitAnimation()
    {
        animator.SetBool("isHit", true);
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("isHit", false);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}