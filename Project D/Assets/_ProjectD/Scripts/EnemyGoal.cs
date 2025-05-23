using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoal : MonoBehaviour
{
    public static EnemyGoal Instance {get; private set;}
    private Enemy _Enemy;
    private Flag _Flag;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (GameManager.Instance.gameState != GameManager.GameState.Playing) return;
        if (Enemy.Instance == null && Flag.Instance == null) return;
        if (collider.CompareTag("character") && collider.gameObject == Enemy.Instance.gameObject && collider.gameObject == Flag.Instance.holder)
        {
            GameManager.Instance.OnGameEnded(false);
        }
    }
}
