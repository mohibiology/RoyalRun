using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] Button retryButton;
    [SerializeField] Button homeButton;
    [SerializeField] Button scoreboardButton;

    void Start()
    {
        Time.timeScale = 1f; // Reset time scale just in case
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        scoreText.text = finalScore.ToString();
        retryButton.onClick.AddListener(RetryGame);
        // homeButton.onClick.AddListener(GoHome);
    }
    void RetryGame()
    {
        SceneManager.LoadScene("Main Level");
    }
    void GoHome()
    {
        // SceneManager.LoadScene(homeSceneName);
    }
}
