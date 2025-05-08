using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject Tutorial;
    public GameObject Control;
    public GameObject MobileControl;
    public GameObject Dash;
    public Image DashCountdown;
    public GameObject Menu;
    public Button Play;
    public GameObject Win;
    public GameObject Lose;
    public GameObject RoundWin;
    public GameObject RoundLose;
    public GameObject Counter;
    public GameObject ScoreUI;
    public Transform PlayeScoreUI;
    public Transform EnemyScoreUI;
    public Texture FilledRedScore;
    public Texture FilledBlueScore;
    public Texture EmptyScore;
    

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;

        Menu.SetActive(true);
    }


    public void OpenWin()
    {
        Win.SetActive(true);
    }

    public void OpenLose()
    {
        Lose.SetActive(true);
    }

    public void OpenWinRound()
    {
        RoundWin.SetActive(true);
    }

    public void OpenLoseRound()
    {
        RoundLose.SetActive(true);
    }

    public void Clear()
    {
        Menu.SetActive(false);
        Win.SetActive(false);
        Lose.SetActive(false);
    }

    public void OpenControl()
    {
        Control.SetActive(true);
        if (GameManager.Instance.isMobile)
        {
            MobileControl.SetActive(true);
        }
    }

    public void OpenTutorial()
    {
        Tutorial.SetActive(true);
    }

    public void OpenScore()
    {
        ScoreUI.SetActive(true);
    }

    public void UpdateScore(int playerScore, int enemyScore)
    {
        foreach (Transform scoreBox in PlayeScoreUI)
        {
            if (playerScore > 0)
            {
                scoreBox.GetComponent<RawImage>().texture = FilledBlueScore;
                playerScore--;
            }
            else scoreBox.GetComponent<RawImage>().texture = EmptyScore;
        }
        foreach (Transform scoreBox in EnemyScoreUI)
        {
            if (enemyScore > 0)
            {
                scoreBox.GetComponent<RawImage>().texture = FilledRedScore;
                enemyScore--;
            }
            else scoreBox.GetComponent<RawImage>().texture = EmptyScore;
        }
    }

    public void Count()
    {
        Counter.SetActive(false);
        Counter.SetActive(true);
    }

    public void OnDashCountdown(float duration)
    {
        StartCoroutine(DashCountdownRoutine(duration - 0.05f));
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

}