using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.Shapes;

public class NpcAI : MonoBehaviour
{
    public NavMeshAgent _agent; // NPC'nin hareketini kontrol edecek NavMeshAgent bileþeni
    [SerializeField] public GameObject playerGameObj;
    public PlayerHealth playerHealth;
    [SerializeField] public Transform _player; // Player objesinin referansý
    public LayerMask ground, player; // Zemin ve player katmanlarýný belirtmek için kullanýlan layer maskeleri
    public Vector3 destinationPoint; // NPC'nin devriye sýrasýnda gideceði hedef noktayý tutan deðiþken
    private bool destinationPointSet; // Hedef noktanýn belirlenip belirlenmediðini kontrol eden bayrak
    public float walkPointRange; // NPC'nin rastgele yürüme noktasý belirlerken kullanacaðý mesafe aralýðý
    public float timeBetweenAttacks; // NPC'nin saldýrýlarý arasýnda bekleyeceði süre
    public bool isCanAttack; // NPC'nin saldýrýp saldýrmadýðýný kontrol eden bayrak
    public GameObject projectile; // Saldýrý menzili görselleþtirmesi için kullanýlabilecek bir nesne
    public float sightRange = 15f, attackRange = 4f; // Görüþ ve saldýrý menzilleri
    public bool playerInSightRange, playerInAttackRange; // Player'ýn görüþ veya saldýrý menzilinde olup olmadýðýný kontrol eden bayraklar
    NpcAnimator npcAnimator;
    NpcHealth npcHealth;
    public bool isRunningAway;
    public float projectileThrowSpeed = 20f; // Sphere fýrlatma hýzý için public deðiþken


    public float attackDamage = 1f;

    private void Awake()
    {
        isRunningAway = false;
        playerHealth = playerGameObj.GetComponent<PlayerHealth>();
        isCanAttack = true;
        npcHealth = GetComponent<NpcHealth>();
        npcAnimator = GetComponent<NpcAnimator>(); // DemonNPCAnimator bileþenini al
        _agent = GetComponent<NavMeshAgent>(); // NavMeshAgent bileþenini al
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, player); // Player'ýn NPC'nin görüþ menzilinde olup olmadýðýný kontrol et
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, player); // Player'ýn NPC'nin saldýrý menzilinde olup olmadýðýný kontrol et

        if (npcHealth.isDead)
        {
            return;
        }
        else if (isRunningAway)
        {
            RunAway();
            return;
        }

        else
        {
            if (playerHealth.isDead || (!playerInSightRange && !playerInAttackRange && !npcAnimator.isPunchingBool)) // Eðer player görüþ menzilinde ve saldýrý menzilinde deðilse, devriye gezer
            {
                Patroling();
            }
            else if (playerInSightRange && !playerInAttackRange && !npcAnimator.isPunchingBool) // Eðer player görüþ menzilinde ve saldýrý menzilinde deðilse, player'ý takip eder
            {
                ChasePlayer();
            }
            else if (playerInSightRange && playerInAttackRange && npcAnimator.isPunchingBool) // Eðer player hem görüþ hem de saldýrý menzilindeyse, player'a saldýrýr
            {
                AttackPlayer();
            }
        }
    }

    void Patroling()
    {
        if (!destinationPointSet) SearchWalkPoint(); // Eðer hedef nokta belirlenmemiþse, yeni bir yürüyüþ noktasý bul
        if (destinationPointSet) _agent.SetDestination(destinationPoint); // Eðer hedef nokta belirlenmiþse, NPC'yi oraya doðru hareket ettir

        Vector3 distanceToDestinationPoint = transform.position - destinationPoint; // Hedef noktaya olan mesafeyi hesapla

        if (distanceToDestinationPoint.magnitude < 1.0f) destinationPointSet = false; // Eðer NPC hedef noktaya yeterince yakýnsa, bayraðý sýfýrla

        // NPC'nin hedefine bakmasýný saðla
        LookTarget(destinationPoint);
    }

    void RunAway()
    {
        float distance = Vector3.Distance(transform.position, _player.position);

        if(distance < npcAnimator.minRange)
        {
            Vector3 dirToPlayer = transform.position - _player.position;

            Vector3 newPos = transform.position + dirToPlayer;

            _agent.SetDestination(newPos);


        }
    }


    void SearchWalkPoint()
    {
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange); // Yürüyüþ noktasý aralýðýnda rastgele X koordinatý üret
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange); // Yürüyüþ noktasý aralýðýnda rastgele Z koordinatý üret

        destinationPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ); // Rastgele koordinatlarla hedef noktayý belirle

        if (Physics.Raycast(destinationPoint, -transform.up, 2.0f, ground)) destinationPointSet = true; // Hedef noktanýn zeminde olup olmadýðýný kontrol et
    }

    void ChasePlayer()
    {
        if (!npcAnimator.isPunchingBool) // Sadece saldýrý animasyonu oynanmýyorsa hareket et
        {
            _agent.SetDestination(_player.position); // Player'ýn pozisyonunu NPC'nin hedef noktasý olarak ayarla
            LookTarget(_player);
        }
    }


    void AttackPlayer()
    {
        npcAnimator.animator.SetBool("isCanAttack", isCanAttack);
        _agent.SetDestination(transform.position);
        if (isCanAttack && !npcHealth.isHit && !playerHealth.isDead) // Eðer NPC daha önce saldýrmadýysa
        {
            npcAnimator.animator.CrossFade("Attack", 0.2f);
            Debug.Log("npc saldýrý yaptý\n");

            if (projectile != null)
            {
                Vector3 spawnPosition = transform.position + transform.forward * 1.5f + new Vector3(0, 1.5f, 0); // NPC'nin biraz önünde ve yukarýsýnda bir pozisyon
                GameObject thrownSphere = Instantiate(projectile, spawnPosition, Quaternion.identity); // Sphere nesnesini oluþtur
                Rigidbody rb = thrownSphere.AddComponent<Rigidbody>(); // Sphere nesnesine Rigidbody ekle

                // Yerçekimi ölçeðini azalt veya yerçekimini tamamen devre dýþý býrak
                rb.useGravity = false;
                // Alternatif olarak, yerçekimi ölçeðini ayarla
                // rb.gravityScale = 0.1f; 

                Vector3 direction = (_player.position - transform.position).normalized; // Player'a doðru olan yönü hesapla
                rb.velocity = direction * projectileThrowSpeed; // Sphere nesnesini player'a doðru fýrlat (10f hýzýnda)
                Destroy(thrownSphere, 3f); // 1 saniye sonra sphere nesnesini yok et
            }
            else if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
            else
            {
                Debug.LogError("PlayerHealth script'i playerGameObj üzerinde bulunamadý!");
            }

            isCanAttack = false; // Saldýrdýðýný belirtmek için bayraðý true yap
                                 // Saldýrý kodu buraya yazýlýr (örneðin, player'ýn saðlýðýný azaltma)
            Invoke(nameof(ResetAttack), timeBetweenAttacks); // Saldýrý bayraðýný belirtilen süre sonra sýfýrlamak için zamanlayýcý baþlat
        }
    }


    void ResetAttack()
    {
        isCanAttack = true; // Bayraðý false yaparak NPC'nin yeniden saldýrmasýný saðla
    }

    public void LookTarget(Transform target)
    {
        // NPC'nin oyuncuya bakmasýný saðla
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void LookTarget(Vector3 target)
    {
        // NPC'nin hedefe bakmasýný saðla
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

}
