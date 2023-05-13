using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3;
    [SerializeField] int playerScore = 0;
    [SerializeField] string currPlayerName;
    [SerializeField] int highScore;
    [SerializeField] string highScorePlayerName;
    public bool hasDisplayedTS = false;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOver;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] Button startButton;
    [SerializeField] Button restartButton;
    [SerializeField] GameObject highScoreDisplayer;
    [SerializeField] GameObject TitleScreen;
    [SerializeField] TMP_InputField inputPlayerNameText;

    PlayerMovement playerScriptRef;
    // Arrow arrowScriptRef;

    void Awake()
    {
      
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        highScore = PlayerPrefs.GetInt("HIGHSCORE");
        highScorePlayerName = PlayerPrefs.GetString("HIGHSCOREPLAYERNAME");

        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void TakeName()
    {
        currPlayerName = inputPlayerNameText.text;
    }

    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = playerScore.ToString();

          if (!hasDisplayedTS)
        {

            TitleScreen.SetActive(true);
            playerScriptRef = FindObjectOfType<PlayerMovement>();
            // // arrowScriptRef = FindObjectOfType<Arrow>();
            playerScriptRef.enabled = false;
            // // arrowScriptRef.enabled = false;
            startButton.onClick.AddListener(StartGame);
                hasDisplayedTS = true;
            void StartGame()
            {
                // currPlayerName = inputPlayerNameText.text;
                TitleScreen.SetActive(false);
                playerScriptRef.enabled = true;
                // arrowScriptRef.enabled = true;
            }
        }
        // playerScriptRef = FindObjectOfType<PlayerMovement>();
        // // arrowScriptRef = FindObjectOfType<Arrow>();
        // playerScriptRef.enabled = false;
        // // arrowScriptRef.enabled = false;
        // startButton.onClick.AddListener(StartGame);
    }


    public void AddToScore(int pointsToAdd)
    {
        playerScore += pointsToAdd;
        scoreText.text = playerScore.ToString();

        
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            Invoke("TakeLife", 1f);
        }
        else
        {
            playerLives--;
            livesText.text = playerLives.ToString();
            gameOver.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            CheckHighscore();
            highScoreDisplayer.SetActive(true);
            // ResetGameSession();
        }
    }

    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    public void ResetGameSession()
    {
        FindObjectOfType<ScenePersists>().ResetScenePersists();
        //    gameOver.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void CheckHighscore()
    {
        if (playerScore > highScore)
        {
            highScore = playerScore;
            PlayerPrefs.SetInt("HIGHSCORE", highScore);
            PlayerPrefs.SetString("HIGHSCOREPLAYERNAME", currPlayerName);
        }
        highScoreText.text = highScore.ToString();
        playerNameText.text = PlayerPrefs.GetString("HIGHSCOREPLAYERNAME");
    }
}
