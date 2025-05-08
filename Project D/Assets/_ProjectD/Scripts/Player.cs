using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : CharacterBase
{
    public static Player Instance {get; set;}
    public bool Enabled = false;
    private PlayerInput playerInput;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;

        playerInput = gameObject.GetComponent<PlayerInput>();
    }

    void FixedUpdate()
    {
        if (Enabled == false) return;
        UpdateMoveDirection();
        if (!isStunned)
        {
            Move();
            Rotate();
            Animate();
            if (playerInput.actions["Dash"].ReadValue<float>() == 1)
            {
                if (Dash()) UIManager.Instance.OnDashCountdown(dashCooldown);
            }
        }
    }

    void OnDashRequest(InputAction.CallbackContext obj)
    {

    }

    void UpdateMoveDirection()
    {
        if (MoveDirection != Vector3.zero) LastMoveDirection = MoveDirection;
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        MoveDirection = new Vector3(input.x, 0, input.y);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("attack") && collider != gameObject && Flag.Instance.holder == gameObject)
        {
            if (GameManager.Instance.gameState != GameManager.GameState.Playing) return;
            GameManager.Instance.OnGameEnded(false);
        }
        else if (collider.CompareTag("mud"))
        {
            WalkSpeedMultiply = 0.5f;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("mud"))
        {
            WalkSpeedMultiply = 1f;
        }
    }

}
