using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : CharacterBase
{
    public static Enemy Instance {get; set;}
    public bool Enabled = false;

    private GameObject Target;
    private NavMeshAgent agent;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;

        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = true;
        agent.updateRotation = true;
    }
    
    void FixedUpdate()
    {
        if (Enabled == false) return;
        if (Flag.Instance == null) return;

        if (Flag.Instance.holder == null)
        {
            Target = Flag.Instance.gameObject;
        }
        else if (Flag.Instance.holder == gameObject)
        {
            Target = EnemyGoal.Instance.gameObject;
        }
        else if (Flag.Instance.holder == Player.Instance.gameObject)
        {
            Target = Player.Instance.gameObject;
        }

        MoveDirection = agent.desiredVelocity.normalized;

        if (!isStunned)
        {
            agent.SetDestination(Target.transform.position);
            Move();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("attack") && collider != gameObject && Flag.Instance.holder == gameObject)
        {
            GameManager.Instance.OnGameEnded(true);
        }    
    }
}