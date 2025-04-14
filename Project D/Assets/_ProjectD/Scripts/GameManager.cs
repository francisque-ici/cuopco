using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Prefabs & Spawns")]
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;
    public Transform SpawnA;
    public Transform SpawnB;
    public GameObject FlagPrefab;
    public Transform FlagRoot;
    public Transform ObstacleContainer;
    public ParticleSystem DustPrefab;

    private GameObject player1;
    private GameObject player2;
    private GameObject flag;

    public enum GameState { Waiting, Playing, GameOver }
    public GameState gameState = GameState.Waiting;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    public void StartNewGame()
    {
        gameState = GameState.Waiting;

        ClearObstacles();
        ClearCharacters();

        ObstacleSpawner.Instance.SpawnObstacles();

        SpawnPlayers();
        SpawnFlag();

        UIManager.Instance.Clear();
        UIManager.Instance.Count();

        StartCoroutine(StartCounter());
    }

    void ClearObstacles()
    {
        foreach (Transform child in ObstacleContainer)
        {
            Destroy(child.gameObject);
        }
    }

    void ClearCharacters()
    {
        if (player1 != null) Destroy(player1);
        if (player2 != null) Destroy(player2);
        if (flag != null) Destroy(flag);
    }

    void SpawnPlayers()
    {
        player1 = Instantiate(PlayerPrefab, SpawnA.position, Quaternion.identity);
        player2 = Instantiate(EnemyPrefab, SpawnB.position, Quaternion.identity);
    }

    void SpawnFlag()
    {
        flag = Instantiate(FlagPrefab, Vector3.zero , Quaternion.identity, FlagRoot.transform);
        flag.transform.localPosition = Vector3.zero;
    }

    public void OnGameEnded(bool isWin)
    {
        if (gameState == GameState.GameOver) return;
        gameState = GameState.GameOver;

        Enemy.Instance.Enabled = false;

        if (isWin) UIManager.Instance.OpenWin();
        else UIManager.Instance.OpenLose();
    }

    private IEnumerator StartCounter()
    {
        yield return new WaitForSeconds(1.85f);
        Player.Instance.Enabled = true;
        Enemy.Instance.Enabled = true;

        UIManager.Instance.OpenTutorial();

        gameState = GameState.Playing;
    }
}
