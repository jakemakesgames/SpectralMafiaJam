﻿using System.Collections;
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

    
    [SerializeField] float fadeInSpeed;
    [SerializeField] float fadeOutSpeed;

    private GameObject player1GO;
    private GameObject player2GO;

    private Image fadeImage;
    [SerializeField] GameState currentGameState;

    private GameObject currentLevel;
    private GameObject levels;
    private bool fadeOut;
    private bool fadeIn;

    private bool firstUpdate;

    private int currentLevelCount;
    private static GameManager instance;

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
        fadeInAndOutTime = 0;
    }


    public void ChangeLevel()
    {
        fadeOut = true;
        currentLevelCount++;
        fadeImage.color = new Color(0.0f, 0.0f, 0.0f, 0.051f);
        Debug.Log("changing scene");
    }


    void Update()
    {

        if (fadeOut)
        {
            fadeInAndOutTime += fadeInSpeed * Time.deltaTime;
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, (float)Mathf.Lerp(0, 1, fadeInAndOutTime));

            if(fadeImage.color.a >= 1f)
            {
                LoadLevel();
                StartCoroutine(Fade());
            }
        }
        if(fadeIn)
        {
            fadeInAndOutTime += fadeOutSpeed * Time.deltaTime;
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, Mathf.Lerp(1, 0, fadeInAndOutTime));
            if(fadeImage.color.a <= 0)
            {
                fadeIn = false;
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
    
    IEnumerator Fade()
    {
        yield return new WaitForSeconds(1f);
        fadeIn = true;
        fadeOut = false;
        fadeInAndOutTime = 0;
    }



}
