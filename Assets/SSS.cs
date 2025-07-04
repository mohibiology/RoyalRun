using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SceneLoaders : MonoBehaviour
{
    [SerializeField] private TMP_Text usernameDisplayText; // UI Text to show the username
    [SerializeField] private Button logoutButton;          // Logout button
    [SerializeField] private Button exitButton;            // Exit button
    [SerializeField] private Button startButton;           // Start button

    private void Start()
    {
        // Check if this is the first time the game is running
        if (!PlayerPrefs.HasKey("FirstRun"))
        {
            // This is the first time running the game
            PlayerPrefs.SetString("PlayerName", ""); // Set username to empty
            PlayerPrefs.SetInt("FirstRun", 1);       // Mark the game as initialized
            PlayerPrefs.Save();
        }

        string playerName = PlayerPrefs.GetString("PlayerName", "");

        if (!string.IsNullOrEmpty(playerName))
        {
            usernameDisplayText.text = "Player: " + playerName;
            logoutButton.gameObject.SetActive(true); // Show logout button
        }
        else
        {
            usernameDisplayText.text = "No User";
            logoutButton.gameObject.SetActive(false); // Hide logout button
        }

        logoutButton.onClick.AddListener(Logout);
        exitButton.onClick.AddListener(ExitGame);
        startButton.onClick.AddListener(LoadSceneByName);
    }

    public void LoadSceneByName()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "");

        if (string.IsNullOrEmpty(playerName))
        {
            // No username saved, go to Username Scene
            SceneManager.LoadScene("ss"); // Username Scene
        }
        else
        {
            // Username exists, directly go to Game Scene
            SceneManager.LoadScene("Main Level");
        }
    }

    private void Logout()
    {
        PlayerPrefs.DeleteKey("PlayerName");
        PlayerPrefs.Save();

        usernameDisplayText.text = "No User";
        logoutButton.gameObject.SetActive(false);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);

        Debug.Log("User logged out.");
    }

    private void ExitGame()
    {
        Debug.Log("Exit button pressed.");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in Unity Editor
#endif
    }
}
