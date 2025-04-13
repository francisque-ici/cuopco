using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : CharacterBase
{
    public static Player Instance {get; set;}
    public bool Enabled = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    void FixedUpdate()
    {
        if (Enabled == false) return;
        UpdateMoveDirection();
        if (!isStunned)
        {
            Move();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Dash();
            }
        }
    }

    void UpdateMoveDirection()
    {
        MoveDirection = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical"));
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("attack") && collider != gameObject && Flag.Instance.holder == gameObject)
        {
            GameManager.Instance.OnGameEnded(false);
        }    
    }

}
