using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerScores = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = ("LIVES: " + playerLives.ToString());
        scoreText.text = ("SCORE: " + playerScores.ToString());
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 0 )
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void AddToScore(int scoreValue)
    {
        playerScores += scoreValue;
        scoreText.text = ("SCORE: " + playerScores.ToString());
    }

    IEnumerator WaitBeforeLoadingScene()
    {
        yield return new WaitForSecondsRealtime(2f);
        if (playerLives > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            FindObjectOfType<ScenePersist>().ResetScenePersist();
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
    }

    private void TakeLife()
    {
        playerLives--;
        livesText.text = ("LIVES: " + playerLives.ToString());
        StartCoroutine(WaitBeforeLoadingScene());
    }

    private void ResetGameSession()
    {
        StartCoroutine(WaitBeforeLoadingScene());
    }
}
