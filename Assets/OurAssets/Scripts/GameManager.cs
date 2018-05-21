using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject playerGO;
    public GameObject PlayerGO { get { return playerGO; } }

    public int playerCount = 1;

    private Image fadeImage;
    public enum GameState
    {
        MENU_STATE,
        GAME_STATE,
        GAMEOVER_STATE
    };
    private GameState currentGameState;

    private bool changeCamColour;
    private bool firstUpdate;

    private int level;
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private GameState CurrentGameState
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

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        currentGameState = GameState.MENU_STATE;
        playerGO = GameObject.FindGameObjectWithTag("Player");
        firstUpdate = true;
        changeCamColour = false;
    }


    private void ChangeLevel()
    {
        changeCamColour = true;
    }


    void Update()
    {
        if(changeCamColour)
        {
            //fadeImage.color.a = 
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
            fadeImage = GameObject.Find("FadePanel").GetComponent<Image>();
            firstUpdate = false;
        }
    }

    private void UpdateGameState()
    {
        if (firstUpdate == true)
            firstUpdate = false;
    }

    private void UpdateGameOverState()
    {
        if (firstUpdate == true)
            firstUpdate = false;
    }


}
