using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class PlayerNamePref : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button saveButton;
    [SerializeField] private TMP_Text feedbackText;

    private string baseUrl = "https://royalrunleaderboard.onrender.com";

    private void Start()
    {
        saveButton.onClick.AddListener(OnSaveButtonClicked);
    }

    private void Update()
    {
        // When Enter key is pressed
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            OnSaveButtonClicked();
        }
    }
    private void OnSaveButtonClicked()
    {
        string playerName = nameInputField.text.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            feedbackText.text = "Please enter a username.";
            return;
        }

        StartCoroutine(CheckUsername(playerName));
    }

    IEnumerator CheckUsername(string playerName)
    {
        string url = baseUrl + "/check-username/";

        UsernameData data = new UsernameData { name = playerName };
        string jsonData = JsonUtility.ToJson(data);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            UsernameResponse response = JsonUtility.FromJson<UsernameResponse>(request.downloadHandler.text);

            if (response.available)
            {
                PlayerPrefs.SetString("PlayerName", playerName);
                PlayerPrefs.Save();
                Debug.Log("Name saved: " + playerName);
                SceneManager.LoadScene("Main Level");
            }
            else
            {
                feedbackText.text = "Username is already taken. Please try another.";
            }
        }
        else
        {
            feedbackText.text = "Error connecting to server. Please try again.";
            Debug.Log("Error: " + request.error);
        }
    }

    [System.Serializable]
    public class UsernameData
    {
        public string name;
    }

    [System.Serializable]
    public class UsernameResponse
    {
        public bool available;
        public string message;
    }
}
