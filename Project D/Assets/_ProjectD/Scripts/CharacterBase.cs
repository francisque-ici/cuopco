using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public float WalkSpeed;
    public Vector3 MoveDirection;

    private Rigidbody rb;
    void Start()
    {
        WalkSpeed = 7f;
        rb = transform.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        rb.velocity = MoveDirection * WalkSpeed;
    }
}
