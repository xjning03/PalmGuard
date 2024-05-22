
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
//using UnityEditor.UIElements; // Comment this out or replace with necessary namespace
#endif


public class LeaderboardManager : MonoBehaviour
{
    public Text score;
    public Text user;
    public Text id;
    public Text score1;
    public Text user1;
    public Text id1;
    public Text score2;
    public Text user2;
    public Text id2;
    public Text score3;
    public Text user3;
    public Text id3;
    public Text score4;
    public Text user4;
    public Text id4;
    public Text score5;
    public Text user5;
    public Text id5;

    private string connectionString = "URI=file:PalmGuard.db"; // Connection string to your SQLite database

    private void Start()
    {
        RetrieveLeaderboardData();
    }

    private void RetrieveLeaderboardData()
    {
        List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                // SQL query to retrieve score, name, and ID from the database
                command.CommandText = "SELECT Score.totalScore, Employee.EmpID, Employee.EmpFName, Employee.EmpLName " +
                                      "FROM Score " +
                                      "JOIN User ON Score.UserID = User.UserID " +
                                      "JOIN Employee ON User.EmpID = Employee.EmpID " +
                                      "ORDER BY Score.totalScore DESC " +
                                      "LIMIT 10";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int totalScore = reader.GetInt32(0);
                        int empID = reader.GetInt32(1);
                        string empFName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        string empLName = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        string empName = empFName + " " + empLName;

                        leaderboardEntries.Add(new LeaderboardEntry(totalScore, empID, empName));
                    }
                }
            }
        }

        // Display the top 1 scoring user's details if available
        if (leaderboardEntries.Count > 0)
        {
            var topEntry = leaderboardEntries[0];
            score.text = topEntry.totalScore.ToString();
            user.text = topEntry.empName;
            id.text = "@empid:" + topEntry.empID.ToString();
        }

        if (leaderboardEntries.Count > 0)
        {
            var secondEntry = leaderboardEntries[1];
            score1.text = secondEntry.totalScore.ToString();
            user1.text = secondEntry.empName;
            id1.text = "@empid:" + secondEntry.empID.ToString();
        }

        if (leaderboardEntries.Count > 0)
        {
            var thirdEntry = leaderboardEntries[2];
            score2.text = thirdEntry.totalScore.ToString();
            user2.text = thirdEntry.empName;
            id2.text = "@empid:" + thirdEntry.empID.ToString();
        }

        if (leaderboardEntries.Count > 0)
        {
            var forthEntry = leaderboardEntries[3];
            score3.text = forthEntry.totalScore.ToString();
            user3.text = forthEntry.empName;
            id3.text = "@empid:" + forthEntry.empID.ToString();
        }

        if (leaderboardEntries.Count > 0)
        {
            var fifthEntry = leaderboardEntries[4];
            score4.text = fifthEntry.totalScore.ToString();
            user4.text = fifthEntry.empName;
            id4.text = "@empid:" + fifthEntry.empID.ToString();
        }

        if (leaderboardEntries.Count > 0)
        {
            var sixthEntry = leaderboardEntries[5];
            score5.text = sixthEntry.totalScore.ToString();
            user5.text = sixthEntry.empName;
            id5.text = "@empid:" + sixthEntry.empID.ToString();
        }
    }

    public class LeaderboardEntry
    {
        public int totalScore;
        public int empID;
        public string empName;

        public LeaderboardEntry(int totalScore, int empID, string empName)
        {
            this.totalScore = totalScore;
            this.empID = empID;
            this.empName = empName;
        }
    }
}

