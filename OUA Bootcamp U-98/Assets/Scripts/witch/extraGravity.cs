using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extraGravity : MonoBehaviour
{

    [SerializeField] float extraGravityForce = 10f;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * extraGravityForce, ForceMode.Acceleration);
    }
}
