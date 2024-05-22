using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour   
{
    public Text score;
    private string dbName = "URI=file:PalmGuard.db"; // Corrected URI syntax

    void Start()
    {
        Debug.Log("User ID: " + UserSession.UserID);
        // Use the UserID for scoring or any other purpose
    }

    public void SubmitScore(int score)
    {
        // Open a connection to the database
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Check if the user already has a score in the database
            int existingScore = GetExistingScore(connection);

            // Calculate the new score by adding the existing score and the provided score
            int newScore = existingScore + score;

            // Update the score in the database
            UpdateScore(connection, newScore);

            Debug.Log("Score updated successfully for UserID: " + UserSession.UserID);
        }
    }

    private int GetExistingScore(SqliteConnection connection)
    {
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT Day1Score FROM Score WHERE UserID = @userID";
            command.Parameters.AddWithValue("@userID", UserSession.UserID);

            // Execute the query and get the existing score
            object result = command.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                return Convert.ToInt32(result);
            }
            else
            {
                // If no existing score found, return 0
                return 0;
            }
        }
    }

    private void UpdateScore(SqliteConnection connection, int newScore)
    {
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "UPDATE Score SET Day1Score = @score WHERE UserID = @userID";
            command.Parameters.AddWithValue("@userID", UserSession.UserID);
            command.Parameters.AddWithValue("@score", newScore);

            // Execute the update command
            command.ExecuteNonQuery();
        }
        score.text = "+"+newScore.ToString();
    }
}
