using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// NPC AI sınıfı tanımı
public class DemonNPC : MonoBehaviour
{
    // NPC'nin hareketini kontrol edecek NavMeshAgent bileşeni
    public NavMeshAgent _agent;

    // Player objesinin referansı
    [SerializeField] Transform _player;

    // Zemin ve player katmanlarını belirtmek için kullanılan layer maskeleri
    public LayerMask ground, player;

    // NPC'nin devriye sırasında gideceği hedef noktayı tutan değişken
    public Vector3 destinationPoint;

    // Hedef noktanın belirlenip belirlenmediğini kontrol eden bayrak
    private bool destinationPointSet;

    // NPC'nin rastgele yürüme noktası belirlerken kullanacağı mesafe aralığı
    public float walkPointRange;

    // NPC'nin saldırıları arasında bekleyeceği süre
    public float timeBetweenAttacks;

    // NPC'nin saldırıp saldırmadığını kontrol eden bayrak
    private bool alreadyAttacked;

    // Saldırı menzili görselleştirmesi için kullanılabilecek bir nesne
    public GameObject sphere;

    // Görüş ve saldırı menzilleri
    public float sightRange, attackRange;

    // Player'ın görüş veya saldırı menzilinde olup olmadığını kontrol eden bayraklar
    public bool playerInSightRange, playerInAttackRange;

    // Oyun başladığında çağrılan fonksiyon
    private void Awake()
    {
        // NavMeshAgent bileşenini al
        _agent = GetComponent<NavMeshAgent>();
    }

    // Her karede çağrılan fonksiyon
    private void Update()
    {
        // Player'ın NPC'nin görüş menzilinde olup olmadığını kontrol et
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, player);

        // Player'ın NPC'nin saldırı menzilinde olup olmadığını kontrol et
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, player);

        // Eğer player görüş menzilinde ve saldırı menzilinde değilse, devriye gezer
        if (!playerInSightRange && !playerInAttackRange) Patroling();

        // Eğer player görüş menzilinde ve saldırı menzilinde değilse, player'ı takip eder
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();

        // Eğer player hem görüş hem de saldırı menzilindeyse, player'a saldırır
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    // NPC'nin devriye davranışını kontrol eden fonksiyon
    void Patroling()
    {
        // Eğer hedef nokta belirlenmemişse, yeni bir yürüyüş noktası bul
        if (!destinationPointSet)
        {
            SearchWalkPoint();
        }

        // Eğer hedef nokta belirlenmişse, NPC'yi oraya doğru hareket ettir
        if (destinationPointSet)
        {
            _agent.SetDestination(destinationPoint);
        }

        // Hedef noktaya olan mesafeyi hesapla
        Vector3 distanceToDestinationPoint = transform.position - destinationPoint;

        // Eğer NPC hedef noktaya yeterince yakınsa, bayrağı sıfırla
        if (distanceToDestinationPoint.magnitude < 1.0f)
        {
            destinationPointSet = false;
        }
    }

    // NPC için rastgele bir yürüyüş noktası belirleyen fonksiyon
    void SearchWalkPoint()
    {
        // Yürüyüş noktası aralığında rastgele X ve Z koordinatları üret
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        // Rastgele koordinatlarla hedef noktayı belirle
        destinationPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Hedef noktanın zeminde olup olmadığını kontrol et
        if (Physics.Raycast(destinationPoint, -transform.up, 2.0f, ground))
        {
            destinationPointSet = true;
        }
    }

    // Player'ı takip etme davranışını kontrol eden fonksiyon
    void ChasePlayer()
    {
        // Player'ın pozisyonunu NPC'nin hedef noktası olarak ayarla
        _agent.SetDestination(_player.position);
    }

    // Player'a saldırma davranışını kontrol eden fonksiyon
    void AttackPlayer()
    {
        // NPC'yi hareket ettirmeyi durdur
        _agent.SetDestination(transform.position);

        // NPC'nin player'a bakmasını sağla
        transform.LookAt(_player);

        // Eğer NPC daha önce saldırmadıysa
        if (!alreadyAttacked)
        {
            // Saldırdığını belirtmek için bayrağı true yap
            alreadyAttacked = true;

            // Saldırı kodu buraya yazılır (örneğin, player'ın sağlığını azaltma)

            // Saldırı bayrağını belirtilen süre sonra sıfırlamak için zamanlayıcı başlat
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    // Saldırı bayrağını sıfırlayan fonksiyon
    void ResetAttack()
    {
        // Bayrağı false yaparak NPC'nin yeniden saldırmasını sağla
        alreadyAttacked = false;
    }
}

