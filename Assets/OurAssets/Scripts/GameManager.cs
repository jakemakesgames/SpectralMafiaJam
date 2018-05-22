﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        GAME_STATE,
        GAMEOVER_STATE
    };


    [SerializeField] float fadeInSpeed;
    [SerializeField] float fadeOutSpeed;

    [SerializeField] GameObject player1Prefab;
    [SerializeField] GameObject player2Prefab;

    private GameObject player1GO;
    private GameObject player2GO;

    private PlayerController player1Controller;
    private PlayerController player2Controller;


    private Image fadeImage;
    [SerializeField] GameState currentGameState;

    private GameObject currentLevel;
    private GameObject levels;
    private bool fadeOut;
    private bool fadeIn;

    private bool firstUpdate;

    private int currentLevelCount;
    private static GameManager instance;
    private FollowObjects camScript;

    private bool setupByMenu = false;
    private float fadeInAndOutTime;

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
    public GameObject Player1GO { get { return player1GO; } }
    public GameObject Player2GO { get { return player2GO; } }
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

        camScript = GameObject.FindGameObjectWithTag("MainCamera").transform.parent.GetComponent<FollowObjects>();

        if (camScript == null)
        {
            camScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowObjects>();
        }

        firstUpdate = true;
        fadeOut = false;

        if (setupByMenu == false && currentGameState == GameState.GAME_STATE)
            player1GO = GameObject.FindGameObjectWithTag("Player");
        fadeInAndOutTime = 0;
    }


    public void ChangeLevel()
    {
        fadeOut = true;
        currentLevelCount++;
        fadeImage.color = new Color(0.0f, 0.0f, 0.0f, 0.051f);
        camScript.Stop();
        Debug.Log("changing scene");
    }


    void Update()
    {

        if (fadeOut)
        {
            fadeInAndOutTime += fadeInSpeed * Time.deltaTime;
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, Mathf.Lerp(0, 1, fadeInAndOutTime));

            if (fadeImage.color.a >= 1f)
            {
                LoadLevel();
                StartCoroutine(Fade());
                fadeOut = false;
            }
        }
        if (fadeIn)
        {
            fadeInAndOutTime += fadeOutSpeed * Time.deltaTime;
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, Mathf.Lerp(1, 0, fadeInAndOutTime));
            if (fadeImage.color.a <= 0)
            {
                fadeIn = false;
            }
        }

        switch (currentGameState)
        {
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


    private void UpdateGameState()
    {
        if (firstUpdate == true)
        {

            fadeImage = GameObject.Find("FadePanel").GetComponent<Image>();
            levels = GameObject.FindGameObjectWithTag("Levels");
            currentLevel = levels.transform.GetChild(0).gameObject;
            firstUpdate = false;
        }

        if (player2Controller != null)
        {
            if (player1Controller.IsAlive == false && player2Controller.IsAlive == false)
            {
                currentLevelCount--;
                ChangeLevel();
            }
        }
        else
        {
            if (player1Controller.IsAlive == false)
            {
                currentLevelCount--;
                ChangeLevel();
            }
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

        if (player1GO != null)
        {
            player1GO.transform.position = currentLevel.transform.GetChild(0).position;
            if (player2GO != null)
            {
                player2GO.transform.position = currentLevel.transform.GetChild(1).position;
            }
        }

        camScript.Move();
        camScript.SnapToTargetPos();

    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(1f);
        fadeIn = true;
        fadeOut = false;
        fadeInAndOutTime = 0;
    }

    public void Player1Button()
    {
        player1GO = Instantiate(player1Prefab, new Vector3(-1000, -1000, -1000), Quaternion.identity);
        player1Controller = player1GO.GetComponent<PlayerController>();
        camScript.AddObject(player1GO);
        ChangeLevel();
    }

    public void Player2Button()
    {
        player1GO = Instantiate(player1Prefab, new Vector3(-1000, -1000, -1000), Quaternion.identity);
        player2GO = Instantiate(player2Prefab, new Vector3(-1000, -1000, -1000), Quaternion.identity);
        player1Controller = player1GO.GetComponent<PlayerController>();
        player2Controller = player2GO.GetComponent<PlayerController>();
        camScript.AddObject(player1GO);
        camScript.AddObject(player2GO);


        ChangeLevel();
    }

    public void Exit()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

}
