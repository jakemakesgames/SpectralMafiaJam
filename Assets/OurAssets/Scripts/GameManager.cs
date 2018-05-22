using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        MENU_STATE,
        GAME_STATE,
        GAMEOVER_STATE
    };

    [Tooltip("The area for player one and player two to reach for the end of the level")]
    [SerializeField] float loadLevelSpeed;

    private GameObject player1GO;
    private GameObject player2GO;

    private Image fadeImage;
    [SerializeField] GameState currentGameState;

    private GameObject currentLevel;
    private GameObject levels;
    private bool fadeOut;
    private bool firstUpdate;

    private int currentLevelCount;
    private static GameManager instance;

    private bool setupByMenu = false;

    #region gets and sets
    public GameState CurrentGameState
    {
        get
        {
            return currentGameState;
        }

        set
        {
            currentGameState = value;
        }
    }
    public static GameManager Instance { get { return instance; } }
    public GameObject Player1GO { get { return player1GO; } set { player1GO = value; setupByMenu = true; } }
    public GameObject Player2GO { get { return player2GO; } set { player2GO = value; setupByMenu = true; } }
    #endregion

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        firstUpdate = true;
        fadeOut = false;

        if (setupByMenu == false && currentGameState == GameState.GAME_STATE)
            player1GO = GameObject.FindGameObjectWithTag("Player");
    }


    public void ChangeLevel()
    {
        fadeOut = true;
        currentLevelCount++;
    }


    void Update()
    {
        if(fadeOut)
        {
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, Mathf.Lerp(0.0f, 1.0f, Mathf.PingPong(loadLevelSpeed * Time.deltaTime, 1.0f)));
            if(fadeImage.color.a == 0.0f)
            {
                fadeOut = false;
            }
            else if(fadeImage.color.a == 1.0f)
            {
                ChangeLevel();
            }
        }

        switch (currentGameState)
        {
            case GameState.MENU_STATE:
                UpdateMenuState();
                break;
            case GameState.GAME_STATE:
                UpdateGameState();
                break;
            case GameState.GAMEOVER_STATE:
                UpdateGameOverState();
                break;
            default:
                Assert.IsTrue(true, "GameState is default");
                break;
        }
    }

    private void UpdateMenuState()
    {
        if (firstUpdate == true)
        {
            firstUpdate = false;
        }
    }

    private void UpdateGameState()
    {
        if (firstUpdate == true)
        {

            fadeImage = GameObject.Find("FadePanel").GetComponent<Image>();
            levels = GameObject.Find("Levels");
            currentLevel = levels.transform.GetChild(0).gameObject;
            firstUpdate = false;
        }
    }

    private void UpdateGameOverState()
    {
        if (firstUpdate == true)
            firstUpdate = false;
    }


    private void LoadLevel()
    {
        currentLevel.SetActive(false);
        currentLevel = levels.transform.GetChild(currentLevelCount).gameObject;
        currentLevel.SetActive(true);

        if(player1GO != null)
        {
            player1GO.transform.position = currentLevel.transform.GetChild(0).position;
            if(player2GO != null)
            {
                player2GO.transform.position = currentLevel.transform.GetChild(0).position;
            }
        }
    }

}
