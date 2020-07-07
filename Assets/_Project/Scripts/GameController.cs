using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{

    public Canvas startCanvas;
    public Canvas pauseCanvas;
    public Canvas gameCanvas;
    public Canvas gameOverCanvas;
    public Text scoreText;

    public Player player;
    public LevelController levelController;

    private int currentScore;

    // Start is called before the first frame update
    void Start()
    {
        ResetUI();
        ActivateGame(false);
        // Time.timeScale = 0;

        startCanvas.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        // PlayerController control = new PlayerController();
        // Reset UI
        ResetUI();
        // Reset Life Count
        // control.ResetLives();
        // Activate the gameplay game objects
        ActivateGame(true);
        // Time.timeScale = 1f;
        currentScore = 0;

    }

    public void GameOver()
    {
        ResetUI();
        ActivateGame(false);
        Time.timeScale = 0;
        gameOverCanvas.gameObject.SetActive(true);
    }

    private void ResetUI()
    {
        startCanvas.gameObject.SetActive(false);
        pauseCanvas.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);
    }

    private void ActivateGame(bool activate)
    {
        player.gameObject.SetActive(activate);
        levelController.gameObject.SetActive(activate);
        gameCanvas.gameObject.SetActive(activate);
    }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
            pauseCanvas.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseCanvas.gameObject.SetActive(false);
        }
    }

    public void IncrementScore(int point)
    {
        currentScore += point;
        scoreText.text = "Score: " + currentScore.ToString();
    }

    public void Restart()
    {
        string mainSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(mainSceneName);
    }
}
