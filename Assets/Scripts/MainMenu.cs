using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text highScoreText;
    public Button playButton;

    void Start()
    {
        DisplayHighScores();
        if (playButton != null)
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
            Debug.Log("Play button listener added");
        }
        else
        {
            Debug.LogError("Play Button is not assigned in the Inspector");
        }
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

    void OnPlayButtonClicked()
    {
        Debug.Log("Play button clicked");
        SceneManager.LoadScene("BaseScene");
    }
}