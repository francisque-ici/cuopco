using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;
    public Transform SpawnA;
    public Transform SpawnB;
    public GameObject FlagPrefab;
    public Transform FlagSpawnpoint;

    private GameObject player1;
    private GameObject player2;
    private GameObject flag;

    public enum GameState { Waiting, Playing, GameOver }
    public GameState gameState = GameState.Waiting;

    void Start()
    {
        StartNewGame();
    }

    public void StartNewGame()
    {
        gameState = GameState.Playing;
        SpawnPlayers();
        SpawnFlag();
    }

    void SpawnPlayers()
    {
        if (player1 != null) Destroy(player1);
        if (player2 != null) Destroy(player2);

        player1 = Instantiate(PlayerPrefab, SpawnA.position, Quaternion.identity);
        player2 = Instantiate(EnemyPrefab, SpawnB.position, Quaternion.identity);
    }

    void SpawnFlag()
    {
        if (flag != null) Destroy(flag);
        flag = Instantiate(FlagPrefab, FlagSpawnpoint.position, Quaternion.identity);
    }

    public void GameOver(string winner)
    {
        gameState = GameState.GameOver;
        Debug.Log("Game Over! " + winner + " wins!");
    }
} 