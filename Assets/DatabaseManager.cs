using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using Unity.VisualScripting;
using System;
using UnityEditor.MemoryProfiler;
using TMPro;
using UnityEngine.UI;
public class DatabaseManager : MonoBehaviour
{
    [SerializeField] GameObject scoreBoardPanel;
    [SerializeField] TMP_Text player;
    [SerializeField] TMP_Text score;
    [SerializeField] Button LeaderBoard;
    [SerializeField] Button backButton;
    string dbName = "URI=file:ScoreRecord.db";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Guest");
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        CreateDB();
        AddScore(playerName, finalScore);
        LeaderBoard.onClick.AddListener(ShowPanel);
        backButton.onClick.AddListener(ClosePanel);
        
    }
    void ShowPanel()
    {
        scoreBoardPanel.gameObject.SetActive(true);
        DisplayScore();
    }
    void ClosePanel()
    {
        scoreBoardPanel.gameObject.SetActive(false);
    }

    private void DisplayScore()
    {
        player.text = "";
        score.text = "";

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command1 = connection.CreateCommand())
            {
                command1.CommandText = "SELECT UPPER(name) AS name FROM (SELECT name, MAX(score) AS max_score FROM scoreboard GROUP BY UPPER(name)) AS grouped_scores ORDER BY max_score DESC LIMIT 10;";
                using (IDataReader reader = command1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        player.text += reader["name"] + "\n";
                    }
                    reader.Close();
                }
            }
            using (var command2 = connection.CreateCommand())
            {
                command2.CommandText = "SELECT MAX(score) as max_score FROM scoreboard GROUP BY name ORDER BY max_score DESC LIMIT 10;";
                using (IDataReader reader = command2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        score.text += reader["max_score"] + "\n";
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }
    }

    private void AddScore(string v1, int v2)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO scoreboard (name, score) VALUES('" + v1 + "', '" + v2 + "');";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateDB()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS scoreboard (name VARCHAR(20), score INT);";
                command.ExecuteNonQuery();
            }
        }
    }
}


