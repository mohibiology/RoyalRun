using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject playButton;

    private bool isPaused = false;

    public void PauseGame()
    {
        Time.timeScale = 0f; // Freeze the game
        isPaused = true;
        pauseButton.SetActive(false);
        playButton.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
        pauseButton.SetActive(true);
        playButton.SetActive(false);
    }
}
