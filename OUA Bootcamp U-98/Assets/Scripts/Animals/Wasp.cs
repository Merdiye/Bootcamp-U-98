using UnityEngine;

public class BeeController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 moveDirection;
    private float movementSpeed = 2f; // Uçma hýzý
    private float directionChangeInterval = 3f; // Yön deðiþtirme aralýðý
    private float nextDirectionChangeTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Yerçekimini devre dýþý býrak
        SetRandomDirection();

        nextDirectionChangeTime = Time.time + directionChangeInterval;
    }

    void Update()
    {
        if (Time.time > nextDirectionChangeTime)
        {
            SetRandomDirection();
            nextDirectionChangeTime = Time.time + directionChangeInterval;
        }

        Move();
    }

    void SetRandomDirection()
    {
        // Rastgele bir hareket yönü belirle (X ve Z eksenlerinde)
        moveDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), Random.Range(-1f, 1f)).normalized;
    }

    void Move()
    {
        // Arýnýn hareket etmesini saðla
        rb.MovePosition(rb.position + moveDirection * movementSpeed * Time.deltaTime);

        // Arýnýn yönünü hareket yönüne çevir
        if (moveDirection != Vector3.zero)
        {
            // Hareket yönüne bakacak þekilde dönüþ yap
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * movementSpeed);
        }
    }
}
