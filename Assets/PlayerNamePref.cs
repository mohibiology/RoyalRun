using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerNamePref : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button saveButton;

    private void Start()
    {
        // Link the button click to the SaveName method
        saveButton.onClick.AddListener(SaveName);
    }

    private void SaveName()
    {
        string playerName = nameInputField.text;
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
        Debug.Log("Name saved: " + playerName);
        SceneManager.LoadScene("Main Level");
    }
}
