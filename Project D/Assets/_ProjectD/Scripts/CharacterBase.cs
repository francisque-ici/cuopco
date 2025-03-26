using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public int WalkSpeed;
    public Vector3 MoveDirection;

    private Rigidbody rb;
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    private void Move()
    {
        rb.velocity = MoveDirection;
    }
}
