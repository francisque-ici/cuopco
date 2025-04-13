using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoal : MonoBehaviour
{
    public static PlayerGoal Instance {get; private set;}
    private GameManager _GameManager;
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
        if (Player.Instance == null && Flag.Instance == null) return;
        if (collider.CompareTag("character") && collider.gameObject == Player.Instance.gameObject && collider.gameObject == Flag.Instance.holder)
        {
            GameManager.Instance.OnGameEnded(true);
        }
    }
}
