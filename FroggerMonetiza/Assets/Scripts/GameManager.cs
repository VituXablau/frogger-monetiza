using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameplay, gameOver;
    public GameObject[] lives;
    public TMP_Text scoreTxt, highScoreTxt, gameOverScoreTxt, gameOverHighScoreTxt;
    public Image timeBar;

    public float timeToGoal, totalTime;
    public bool isCounting;
    public int gameLives, curSceneIndex;
    public static int score, highScore;

    void Awake()
    {
        if (instance == null)
            instance = this;

        timeToGoal = totalTime;
        isCounting = true;

        curSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        scoreTxt.text = "Score: " + score;
        highScoreTxt.text = "High Score: " + highScore;

        while (isCounting)
        {
            timeToGoal -= Time.deltaTime;

            if (timeToGoal <= 0)
            {
                PlayerController.DestroyItself();
                AudioManager.instance.PlaySFX("Time");
                GameOver();
            }

            break;
        }

        timeBar.fillAmount = timeToGoal / totalTime;
        timeToGoal = Mathf.Clamp(timeToGoal, 0, totalTime);

        if (Input.GetKeyDown(KeyCode.C))
            NextLevel();
    }

    public void ResetTimer()
    {
        timeToGoal = totalTime;
    }

    public void IncreaseScore(int value)
    {
        score += value;
    }

    public void GameOver()
    {
        AudioManager.instance.StopMusic();

        isCounting = false;

        if (score > highScore)
            highScore = score;

        gameOverScoreTxt.text = "Score: " + score;
        gameOverHighScoreTxt.text = "High Score: " + highScore;

        gameplay.SetActive(false);
        gameOver.SetActive(true);
    }

    public void NextLevel()
    {
        if (curSceneIndex != 3)
            SceneManager.LoadScene(curSceneIndex + 1);
        else
            SceneManager.LoadScene(1);
    }

    public void RestartGame()
    {
        score = 0;
        SceneManager.LoadScene(0);
    }
}
