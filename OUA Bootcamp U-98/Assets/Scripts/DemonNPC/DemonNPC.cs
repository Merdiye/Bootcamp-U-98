using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemonNPC : MonoBehaviour
{
    public NavMeshAgent _agent; // NPC'nin hareketini kontrol edecek NavMeshAgent bileşeni
    [SerializeField] Transform _player; // Player objesinin referansı
    public LayerMask ground, player; // Zemin ve player katmanlarını belirtmek için kullanılan layer maskeleri
    public Vector3 destinationPoint; // NPC'nin devriye sırasında gideceği hedef noktayı tutan değişken
    private bool destinationPointSet; // Hedef noktanın belirlenip belirlenmediğini kontrol eden bayrak
    public float walkPointRange; // NPC'nin rastgele yürüme noktası belirlerken kullanacağı mesafe aralığı
    public float timeBetweenAttacks; // NPC'nin saldırıları arasında bekleyeceği süre
    private bool alreadyAttacked; // NPC'nin saldırıp saldırmadığını kontrol eden bayrak
    public GameObject sphere; // Saldırı menzili görselleştirmesi için kullanılabilecek bir nesne
    public float sightRange, attackRange; // Görüş ve saldırı menzilleri
    public bool playerInSightRange, playerInAttackRange; // Player'ın görüş veya saldırı menzilinde olup olmadığını kontrol eden bayraklar
    DemonNPCAnimator demonAnimator;

    private void Awake()
    {
        demonAnimator = GetComponent<DemonNPCAnimator>(); // DemonNPCAnimator bileşenini al
        _agent = GetComponent<NavMeshAgent>(); // NavMeshAgent bileşenini al
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, player); // Player'ın NPC'nin görüş menzilinde olup olmadığını kontrol et
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, player); // Player'ın NPC'nin saldırı menzilinde olup olmadığını kontrol et

        if (!playerInSightRange && !playerInAttackRange && !demonAnimator.isPunchingBool) Patroling(); // Eğer player görüş menzilinde ve saldırı menzilinde değilse, devriye gezer
        if (playerInSightRange && !playerInAttackRange && !demonAnimator.isPunchingBool) ChasePlayer(); // Eğer player görüş menzilinde ve saldırı menzilinde değilse, player'ı takip eder
        if (playerInSightRange && playerInAttackRange) AttackPlayer(); // Eğer player hem görüş hem de saldırı menzilindeyse, player'a saldırır
    }

    void Patroling()
    {
        if (!destinationPointSet) SearchWalkPoint(); // Eğer hedef nokta belirlenmemişse, yeni bir yürüyüş noktası bul
        if (destinationPointSet) _agent.SetDestination(destinationPoint); // Eğer hedef nokta belirlenmişse, NPC'yi oraya doğru hareket ettir

        Vector3 distanceToDestinationPoint = transform.position - destinationPoint; // Hedef noktaya olan mesafeyi hesapla

        if (distanceToDestinationPoint.magnitude < 1.0f) destinationPointSet = false; // Eğer NPC hedef noktaya yeterince yakınsa, bayrağı sıfırla
    }

    void SearchWalkPoint()
    {
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange); // Yürüyüş noktası aralığında rastgele X koordinatı üret
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange); // Yürüyüş noktası aralığında rastgele Z koordinatı üret

        destinationPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ); // Rastgele koordinatlarla hedef noktayı belirle

        if (Physics.Raycast(destinationPoint, -transform.up, 2.0f, ground)) destinationPointSet = true; // Hedef noktanın zeminde olup olmadığını kontrol et
    }

    void ChasePlayer()
    {
        if (!demonAnimator.isPunchingBool) // Sadece saldırı animasyonu oynanmıyorsa hareket et
        {
            _agent.SetDestination(_player.position); // Player'ın pozisyonunu NPC'nin hedef noktası olarak ayarla
        }
    }

    void AttackPlayer()
    {

        _agent.SetDestination(transform.position); 

        if (!alreadyAttacked) // Eğer NPC daha önce saldırmadıysa
        {
            alreadyAttacked = true; // Saldırdığını belirtmek için bayrağı true yap
            // Saldırı kodu buraya yazılır (örneğin, player'ın sağlığını azaltma)
            Invoke(nameof(ResetAttack), timeBetweenAttacks); // Saldırı bayrağını belirtilen süre sonra sıfırlamak için zamanlayıcı başlat
        }
    
    }

    void ResetAttack()
    {
        alreadyAttacked = false; // Bayrağı false yaparak NPC'nin yeniden saldırmasını sağla
    }
}


