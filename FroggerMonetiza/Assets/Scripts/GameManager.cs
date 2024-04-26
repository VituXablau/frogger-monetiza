using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameplay, gameOver;
    public GameObject[] lives;
    public int gameLives;
    [SerializeField] private float totalTime;
    public float timeToGoal;
    public bool isCounting;
    [SerializeField] private TMP_Text scoreTxt, highScoreTxt, gameOverScoreTxt, gameOverHighScoreTxt;
    [SerializeField] private Image timeBar;

    void Awake()
    {
        if (instance == null)
            instance = this;

        timeToGoal = totalTime;
        isCounting = true;
    }

    void Update()
    {
        scoreTxt.text = "Score: " + Score.score;
        highScoreTxt.text = "High Score: " + Score.highScore;

        while (isCounting)
        {
            timeToGoal -= Time.deltaTime;

            if (timeToGoal <= 0)
            {
                PlayerController.DestroyItself();
                GameOver();
            }

            break;
        }

        timeBar.fillAmount = timeToGoal / 100;
        timeToGoal = Mathf.Clamp(timeToGoal, 0, totalTime);
    }

    public void ResetTimer()
    {
        timeToGoal = totalTime;
    }

    public void IncreaseScore(int value)
    {
        Score.score += value;
    }

    public void GameOver()
    {
        isCounting = false;

        if (Score.score > Score.highScore)
            Score.highScore = Score.score;

        gameOverScoreTxt.text = "Score: " + Score.score;
        gameOverHighScoreTxt.text = "High Score: " + Score.highScore;

        gameplay.SetActive(false);
        gameOver.SetActive(true);
    }

    public void NextLevel()
    {
        int curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(curSceneIndex + 1);
    }

    public void RestartGame()
    {
        Score.score = 0;
        SceneManager.LoadScene(0);
    }
}
