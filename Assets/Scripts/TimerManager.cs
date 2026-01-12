using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public float timeRemaining = 120f;
    public Text timerText;

    public GameObject gameOverPanel;
    public Text finalScoreText;

    bool timerRunning = true;

    void Start()
    {
        Time.timeScale = 1f;   
        timerRunning = true;
    }

    void Update()
    {
        if (!timerRunning) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI(timeRemaining);
        }
        else
        {
            timeRemaining = 0;
            timerRunning = false;
            TimeUp();
        }
    }

    void UpdateTimerUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timerText.text = string.Format("Time : {0:00}:{1:00}", minutes, seconds);
    }

    void TimeUp()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);

        finalScoreText.text =
            string.Format("Final Score : {0:000}", ScoreManager.Instance.score);
    }
}
