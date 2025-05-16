using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using Unity.VisualScripting;
using System;
using UnityEditor.MemoryProfiler;
public class DatabaseManager : MonoBehaviour
{

    string dbName = "URI=file:ScoreRecord.db";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Guest");
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        CreateDB();
        AddScore(playerName, finalScore);
        DisplayScore();
    }

    private void DisplayScore()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM scoreboard";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.Log("Name: " + reader["name"] + "\tScore: " + reader["score"]);
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


