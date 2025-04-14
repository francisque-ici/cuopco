using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : CharacterBase
{
    public static Enemy Instance {get; set;}
    public bool Enabled = false;
    public Vector2 WalkSpeedRange;

    private GameObject Target;
    private NavMeshAgent agent;
    private float updateInterval = 0.2f;
    private float nextUpdateTime;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;

        agent = GetComponent<NavMeshAgent>();

        agent.speed = Random.Range(WalkSpeedRange.x, WalkSpeedRange.y);
        agent.stoppingDistance = Random.Range(0, 4);
        agent.obstacleAvoidanceType = (ObstacleAvoidanceType)Random.Range(2, 5);
        agent.radius = Random.Range(1f, 2.5f);

        Debug.Log(agent.obstacleAvoidanceType);
    }
    
    void FixedUpdate()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.GameOver)
        {
            agent.speed = 0;
            agent.enabled = false;
            enabled = false;
            return;
        }

        if (Enabled == false) return;
        if (Flag.Instance == null) return;

        if (Time.time < nextUpdateTime) return;
        nextUpdateTime = Time.time + updateInterval;

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
            agent.stoppingDistance = 0;
            Target = Player.Instance.gameObject;
        }

        if (!isStunned)
        {
            MoveDirection = agent.desiredVelocity.normalized;
            agent.SetDestination(Target.transform.position);
            Move();
        }
        else
        {
            MoveDirection = Vector3.zero;
            agent.ResetPath();
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