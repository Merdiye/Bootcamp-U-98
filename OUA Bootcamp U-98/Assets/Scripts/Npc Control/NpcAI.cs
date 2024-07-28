using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.Shapes;

public class NpcAI : MonoBehaviour
{
    public NavMeshAgent _agent; // NPC'nin hareketini kontrol edecek NavMeshAgent bile�eni
    public GameObject playerGameObj;
    public PlayerHealth playerHealth;
    public Transform _player; // Player objesinin referans�
    public LayerMask ground, player; // Zemin ve player katmanlar�n� belirtmek i�in kullan�lan layer maskeleri
    public Vector3 destinationPoint; // NPC'nin devriye s�ras�nda gidece�i hedef noktay� tutan de�i�ken
    private bool destinationPointSet; // Hedef noktan�n belirlenip belirlenmedi�ini kontrol eden bayrak
    public float walkPointRange; // NPC'nin rastgele y�r�me noktas� belirlerken kullanaca�� mesafe aral���
    public float timeBetweenAttacks; // NPC'nin sald�r�lar� aras�nda bekleyece�i s�re
    public bool isCanAttack; // NPC'nin sald�r�p sald�rmad���n� kontrol eden bayrak
    public GameObject projectile; // Sald�r� menzili g�rselle�tirmesi i�in kullan�labilecek bir nesne
    public float sightRange = 15f, attackRange = 4f; // G�r�� ve sald�r� menzilleri
    public bool playerInSightRange, playerInAttackRange; // Player'�n g�r�� veya sald�r� menzilinde olup olmad���n� kontrol eden bayraklar
    NpcAnimator npcAnimator;
    NpcHealth npcHealth;
    public bool isRunningAway;
    public float projectileThrowSpeed = 20f; // Sphere f�rlatma h�z� i�in public de�i�ken
    

    public float attackDamage = 1f;

    private void Awake()
    {
        isRunningAway = false;
        isCanAttack = true;

        // "Knight" adl� objeyi sahneden bul ve atamalar�n� yap
        playerGameObj = GameObject.Find("Knight");
        if (playerGameObj != null)
        {
            playerHealth = playerGameObj.GetComponent<PlayerHealth>();
            _player = playerGameObj.transform;
        }
        else
        {
            Debug.LogError("Knight adl� obje bulunamad�!");
        }

        npcHealth = GetComponent<NpcHealth>();
        npcAnimator = GetComponent<NpcAnimator>(); // DemonNPCAnimator bile�enini al
        _agent = GetComponent<NavMeshAgent>(); // NavMeshAgent bile�enini al
    }


    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, player); // Player'�n NPC'nin g�r�� menzilinde olup olmad���n� kontrol et
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, player); // Player'�n NPC'nin sald�r� menzilinde olup olmad���n� kontrol et

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
            if (playerHealth.isDead || (!playerInSightRange && !playerInAttackRange && !npcAnimator.isPunchingBool)) // E�er player g�r�� menzilinde ve sald�r� menzilinde de�ilse, devriye gezer
            {
                Patroling();
            }
            else if (playerInSightRange && !playerInAttackRange && !npcAnimator.isPunchingBool) // E�er player g�r�� menzilinde ve sald�r� menzilinde de�ilse, player'� takip eder
            {
                ChasePlayer();
            }
            else if (playerInSightRange && playerInAttackRange && npcAnimator.isPunchingBool) // E�er player hem g�r�� hem de sald�r� menzilindeyse, player'a sald�r�r
            {
                AttackPlayer();
            }
        }
    }

    void Patroling()
    {
        if (!destinationPointSet) SearchWalkPoint(); // E�er hedef nokta belirlenmemi�se, yeni bir y�r�y�� noktas� bul

        if (destinationPointSet)
        {
            _agent.SetDestination(destinationPoint);   // E�er hedef nokta belirlenmi�se, NPC'yi oraya do�ru hareket ettir

            // NPC'nin hedefine bakmas�n� sa�la
            LookTarget(destinationPoint);
        }
        

        Vector3 distanceToDestinationPoint = transform.position - destinationPoint; // Hedef noktaya olan mesafeyi hesapla

        if (distanceToDestinationPoint.magnitude < 1.0f) destinationPointSet = false; // E�er NPC hedef noktaya yeterince yak�nsa, bayra�� s�f�rla

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
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange); // Y�r�y�� noktas� aral���nda rastgele X koordinat� �ret
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange); // Y�r�y�� noktas� aral���nda rastgele Z koordinat� �ret

        destinationPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ); // Rastgele koordinatlarla hedef noktay� belirle

        if (Physics.Raycast(destinationPoint, -transform.up, 2.0f, ground)) destinationPointSet = true; // Hedef noktan�n zeminde olup olmad���n� kontrol et
    }

    void ChasePlayer()
    {
        if (!npcAnimator.isPunchingBool) // Sadece sald�r� animasyonu oynanm�yorsa hareket et
        {
            _agent.SetDestination(_player.position); // Player'�n pozisyonunu NPC'nin hedef noktas� olarak ayarla
            LookTarget(_player);
        }
    }


    void AttackPlayer()
    {
        npcAnimator.animator.SetBool("isCanAttack", isCanAttack);
        _agent.SetDestination(transform.position);
        if (isCanAttack && !npcHealth.isHit && !playerHealth.isDead) // E�er NPC daha �nce sald�rmad�ysa
        {
            npcAnimator.animator.CrossFade("Attack", 0.2f);
            Debug.Log("npc sald�r� yapt�\n");

            if (projectile != null)
            {
                Vector3 spawnPosition = transform.position + transform.forward * 1.5f + new Vector3(0, 1.5f, 0); // NPC'nin biraz �n�nde ve yukar�s�nda bir pozisyon
                GameObject thrownSphere = Instantiate(projectile, spawnPosition, Quaternion.identity); // Sphere nesnesini olu�tur
                Rigidbody rb = thrownSphere.AddComponent<Rigidbody>(); // Sphere nesnesine Rigidbody ekle

                // Yer�ekimi �l�e�ini azalt veya yer�ekimini tamamen devre d��� b�rak
                rb.useGravity = false;
                // Alternatif olarak, yer�ekimi �l�e�ini ayarla
                // rb.gravityScale = 0.1f; 

                Vector3 direction = (_player.position - transform.position).normalized; // Player'a do�ru olan y�n� hesapla
                rb.velocity = direction * projectileThrowSpeed; // Sphere nesnesini player'a do�ru f�rlat (10f h�z�nda)
                Destroy(thrownSphere, 3f); // 1 saniye sonra sphere nesnesini yok et
            }
            else if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
            else
            {
                Debug.LogError("PlayerHealth script'i playerGameObj �zerinde bulunamad�!");
            }

            isCanAttack = false; // Sald�rd���n� belirtmek i�in bayra�� true yap
                                 // Sald�r� kodu buraya yaz�l�r (�rne�in, player'�n sa�l���n� azaltma)
            Invoke(nameof(ResetAttack), timeBetweenAttacks); // Sald�r� bayra��n� belirtilen s�re sonra s�f�rlamak i�in zamanlay�c� ba�lat
        }
    }


    void ResetAttack()
    {
        isCanAttack = true; // Bayra�� false yaparak NPC'nin yeniden sald�rmas�n� sa�la
    }

    public void LookTarget(Transform target)
    {
        // NPC'nin oyuncuya bakmas�n� sa�la
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void LookTarget(Vector3 target)
    {
        // NPC'nin hedefe bakmas�n� sa�la
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

}
