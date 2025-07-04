using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField] GameObject scoreBoardPanel;
    [SerializeField] TMP_Text player;
    [SerializeField] TMP_Text score;
    [SerializeField] Button LeaderBoard;
    [SerializeField] Button backButton;

    private string baseUrl = "https://royalrunleaderboard.onrender.com";

    private void Start()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Guest");
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);

        // Submit the score to the remote server
        StartCoroutine(SubmitScore(playerName, finalScore));

        LeaderBoard.onClick.AddListener(ShowPanel);
        backButton.onClick.AddListener(ClosePanel);
    }

    void ShowPanel()
    {
        scoreBoardPanel.gameObject.SetActive(true);
        StartCoroutine(DisplayScore()); // Fetch leaderboard from API
    }

    void ClosePanel()
    {
        scoreBoardPanel.gameObject.SetActive(false);
    }

    IEnumerator SubmitScore(string playerName, int finalScore)
    {
        string url = baseUrl + "/submit-score/";

        ScoreData data = new ScoreData { name = playerName, score = finalScore };
        string jsonData = JsonUtility.ToJson(data);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Score submitted successfully!");
        }
        else
        {
            Debug.Log("Error submitting score: " + request.error);
        }
    }

    IEnumerator DisplayScore()
    {
        player.text = "";
        score.text = "";

        string url = baseUrl + "/leaderboard/";

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            LeaderboardResponse response = JsonUtility.FromJson<LeaderboardResponse>("{\"entries\":" + json + "}");

            foreach (var entry in response.entries)
            {
                player.text += entry.name.Replace(" ", "\u00A0").ToUpper() + "\n";
                score.text += entry.score + "\n";
            }
        }
        else
        {
            Debug.Log("Error fetching leaderboard: " + request.error);
        }
    }

    // Data class for submitting score
    [System.Serializable]
    public class ScoreData
    {
        public string name;
        public int score;
    }

    // Data classes for leaderboard response
    [System.Serializable]
    public class LeaderboardEntry
    {
        public string name;
        public int score;
    }

    [System.Serializable]
    public class LeaderboardResponse
    {
        public List<LeaderboardEntry> entries;
    }
}
