using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] float startTime =  10f;
    [SerializeField] ScoreManager scoreManager;

    float timeLeft;
    bool gameOver = false;
    // public bool GameOver { get { return gameOver; } }
    // public bool GameOver {get; private set;}
    public bool GameOver => gameOver;
    private void Start() 
    {
        timeLeft = startTime;
    }
    void Update()
    {
        DecreaseTime();
    }
    public void IncreaseTime(float amount)
    {
        timeLeft+=amount;
    }
    private void DecreaseTime()
    {
        if (gameOver) return;
        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F1");
        if (timeLeft <= 0f)
        {
            PlayerGameOver();
        }
    }

    void PlayerGameOver()
    {
        gameOver = true;
        playerController.enabled = false;
        gameOverText.SetActive(true);
        Time.timeScale = 0.1f;
        StartCoroutine(LoadGameOverSceneAfterDelay(0.2f));
    }
    IEnumerator LoadGameOverSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // This uses unscaled time since timeScale is still 1

        PlayerPrefs.SetInt("FinalScore", scoreManager.getScore());
        SceneManager.LoadScene("GameOver");
    }
}
