using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using Unity.VisualScripting;
using System;
using UnityEditor.MemoryProfiler;
public class DatabaseTest : MonoBehaviour
{

    string dbName = "URI=file:Record.db";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateDB();
        AddWep("hello", 10);
        AddWep("he2", 14);
        AddWep("her", 60);
        DisplayWep();
    }

    private void DisplayWep()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM weapons";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.Log("Name: " + reader["name"] + "\tDamage: " + reader["damage"]);
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }
    }

    private void AddWep(string v1, int v2)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO weapons (name, damage) VALUES('" + v1 + "', '" + v2 + "');";
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
                command.CommandText = "CREATE TABLE IF NOT EXISTS weapons (name VARCHAR(20), damage INT);";
                command.ExecuteNonQuery();
            }
        }
    }
}


