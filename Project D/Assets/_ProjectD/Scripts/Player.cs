using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBase
{
    void FixedUpdate()
    {
        float hinput=Input.GetAxis("Horizontal");
        float vinput=Input.GetAxis("Vertical");
        MoveDirection=new Vector3(hinput,0,vinput);
        Move();
    }
} 