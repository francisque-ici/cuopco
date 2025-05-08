using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Prefabs & Spawns")]
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;
    public Image DashCountdown;
    public Transform SpawnA;
    public Transform SpawnB;
    public GameObject FlagPrefab;
    public Transform FlagRoot;
    public Transform ObstacleContainer;
    public ParticleSystem DustPrefab;

    public bool isMobile;

    public int playerScore;
    public int enemyScore;

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

        if (SystemInfo.deviceType == DeviceType.Handheld) isMobile = true;
    }
    public void StartNewGame()
    {
        gameState = GameState.Waiting;

        UIManager.Instance.Clear();
        UIManager.Instance.OpenScore();

        playerScore = 0;
        enemyScore = 0;

        UIManager.Instance.UpdateScore(playerScore, enemyScore);

        if (isMobile)
        {
            StartNewRound();
        }
        else
        {
            UIManager.Instance.OpenTutorial();
        }
    }

    public void StartNewRound()
    {
        gameState = GameState.Playing;

        ClearObstacles();
        ClearCharacters();

        ObstacleSpawner.Instance.SpawnObstacles();

        SpawnPlayers();
        SpawnFlag();

        UIManager.Instance.Clear();
        UIManager.Instance.UpdateScore(playerScore, enemyScore);

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

        if (isWin)
        {
            playerScore += 1;
            if (playerScore >= 3)
            {
                UIManager.Instance.OpenWin();
            }
            else
            {
                UIManager.Instance.OpenWinRound();
            }
        }
        else
        {
            enemyScore += 1;
            if (enemyScore >= 3)
            {
                UIManager.Instance.OpenLose();
            }
            else
            {
                UIManager.Instance.OpenLoseRound();
            }
        }
        UIManager.Instance.UpdateScore(playerScore, enemyScore);
    }

    public void OnDashCountdown(float duration)
    {
        StartCoroutine(DashCountdownRoutine(duration));
    }
    
    private IEnumerator DashCountdownRoutine(float duration)
    {
        if (DashCountdown == null) yield break;
    
        DashCountdown.gameObject.SetActive(true);
        
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            DashCountdown.fillAmount = Mathf.Lerp(1f, 0f, timer / duration);
            yield return null;
        }
    
        DashCountdown.fillAmount = 0f;
        DashCountdown.gameObject.SetActive(false);
    }

    private IEnumerator StartCounter()
    {
        yield return new WaitForSeconds(1.85f);
        Player.Instance.Enabled = true;
        Enemy.Instance.Enabled = true;

        UIManager.Instance.OpenControl();

        gameState = GameState.Playing;
    }

    public void SetIsMobileDevice(string _isMobile)
    {
        isMobile = _isMobile == "true";
    }

}
