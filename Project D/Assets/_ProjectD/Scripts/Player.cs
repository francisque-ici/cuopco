using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBase
{
    void FixedUpdate()
    {
        UpdateMoveDirection();
    }

    void UpdateMoveDirection()
    {
        MoveDirection = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical"));
    }
} 