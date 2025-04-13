using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject Menu;
    public Button Play;
    public GameObject Win;
    public Button Win_Play;
    public GameObject Lose;
    public Button Lose_Play;
    public GameObject Counter;

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

    public void Clear()
    {
        Menu.SetActive(false);
        Win.SetActive(false);
        Lose.SetActive(false);
    }

    public void Count()
    {
        Counter.SetActive(false);
        Counter.SetActive(true);
    }

    void OnPlayButton()
    {
        Debug.Log("PRESSED PLAY");
        GameManager.Instance.StartNewGame();
    }
}

















// Start is called before the first frame update
    /*public GameObject TitleScreen;
    public GameObject GameOver;
    public GameObject win;
    public GameObject lose;
    public GameObject start;
    public GameObject restart;
    void Start()
    {
        start.GetComponent<Button>().onClick.AddListener(OnStartClicked);
        restart.GetComponent<Button>().onClick.AddListener(OnRestartClicked);
        GameOver.SetActive(false);
        TitleScreen.SetActive(true);
        win.SetActive(false);
        lose.SetActive(false);
    }
    public void OnStartClicked()
    {
        TitleScreen.SetActive(false);
        FindObjectOfType<GameManager>().StartNewGame();
    }
    public void ShowGameOver(bool isWin)
    {
        GameOver.SetActive(true);
        win.SetActive(isWin);
        lose.SetActive(!isWin);
    }
    public void OnRestartClicked()
    {
        GameOver.SetActive(false);
        win.SetActive(false);
        lose.SetActive(false);
        FindObjectOfType<GameManager>().StartNewGame();
    }

    void Update()
    {
        
    }*/


//Menu
//PlayButton
//Win
//Win_PlayAgainButton
//Lose
//Lose_PlayAgainButton