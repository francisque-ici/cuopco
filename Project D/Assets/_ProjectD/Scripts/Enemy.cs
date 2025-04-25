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

        WalkSpeed = Random.Range(WalkSpeedRange.x, WalkSpeedRange.y);

        agent.obstacleAvoidanceType = (ObstacleAvoidanceType)Random.Range(0, 4);

        agent.updateRotation = false;
        agent.updatePosition = false;

        Debug.Log(agent.obstacleAvoidanceType);
    }
    
    void LateUpdate()
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

        //if (Time.time < nextUpdateTime) return;
        //nextUpdateTime = Time.time + updateInterval;

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
            Target = Flag.Instance.gameObject;
        }

        NavMeshPath navPath = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, Target.transform.position, NavMesh.AllAreas, navPath))
        {
            if (navPath.corners.Length > 1)
            {
                Vector3 direction = navPath.corners[1] - transform.position;
                direction.y = 0;
                MoveDirection = direction.normalized;

                agent.nextPosition = transform.position;

                Move();
                Rotate();
                Animate();
            }
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