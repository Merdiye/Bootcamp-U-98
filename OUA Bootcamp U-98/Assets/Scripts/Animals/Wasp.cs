using UnityEngine;

public class BeeController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 moveDirection;
    private float movementSpeed = 2f; // U�ma h�z�
    private float directionChangeInterval = 3f; // Y�n de�i�tirme aral���
    private float nextDirectionChangeTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Yer�ekimini devre d��� b�rak
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
        // Rastgele bir hareket y�n� belirle (X ve Z eksenlerinde)
        moveDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), Random.Range(-1f, 1f)).normalized;
    }

    void Move()
    {
        // Ar�n�n hareket etmesini sa�la
        rb.MovePosition(rb.position + moveDirection * movementSpeed * Time.deltaTime);

        // Ar�n�n y�n�n� hareket y�n�ne �evir
        if (moveDirection != Vector3.zero)
        {
            // Hareket y�n�ne bakacak �ekilde d�n�� yap
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * movementSpeed);
        }
    }
}
