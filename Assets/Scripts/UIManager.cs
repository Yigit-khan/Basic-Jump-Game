using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic; // Add this line

public class UIManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Button restartButton;
    public Button mainMenuButton;
    public Text scoreText;
    public Text highScoreText;

    void Start()
    {
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        gameOverPanel.SetActive(false);
        ScoreManager.instance.scoreText = scoreText;
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }
        else
        {
            Debug.LogError("Main Menu Button is not assigned in the Inspector");
        }

        // Ensure highScoreText is anchored correctly
        RectTransform rectTransform = highScoreText.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 1);
        rectTransform.anchorMax = new Vector2(0.5f, 1);
        rectTransform.pivot = new Vector2(0.5f, 1);
        rectTransform.anchoredPosition = new Vector2(0, -50); // Adjust as needed
    }

    void OnRestartButtonClicked()
    {
        // Restart the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ScoreManager.instance.ResetScore();
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        scoreText.text = "Score: " + ScoreManager.instance.score;
        highScoreText.text = "High Score: " + ScoreManager.instance.highScore;
        Debug.Log("High Score: " + ScoreManager.instance.highScore); // Add debug log
        HighScoreManager.instance.AddHighScore(ScoreManager.instance.score);
        DisplayHighScores(); // Add this line
    }

    void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void DisplayHighScores()
    {
        if (HighScoreManager.instance == null)
        {
            Debug.LogError("HighScoreManager instance is null");
            return;
        }

        List<int> highScores = HighScoreManager.instance.GetHighScores();
        if (highScores == null)
        {
            Debug.LogError("HighScores list is null");
            return;
        }

        highScoreText.text = "High Scores:\n";
        for (int i = 0; i < highScores.Count; i++)
        {
            highScoreText.text += string.Format("{0, -3} {1, 10}\n", (i + 1) + ".", highScores[i]);
        }
    }
}