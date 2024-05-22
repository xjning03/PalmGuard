using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.SceneManagement;

public static class UserSession
{
    public static string UserID { get; set; }
}

public class AuthManage : MonoBehaviour
{
    public InputField idInputField; // Reference to the input field where the user enters their ID
    public GameObject ErrorMessage; // Reference to the error message UI object
    string dbName = "URI=file:PalmGuard.db"; // Corrected URI syntax

    public void ReadID(string input)
    {
        UserSession.UserID = input;
        Debug.Log("ID Read: " + UserSession.UserID);
    }

    public void CheckEmployeeId()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT EmpId FROM Employee WHERE EmpId = @empId";
                command.Parameters.AddWithValue("@empId", UserSession.UserID);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Debug.Log("Employee ID found: " + reader["EmpId"]);
                        // Do something when ID matches
                        ErrorMessage.SetActive(false); // Hide error message if previously shown
                        SceneManager.LoadSceneAsync("Scene1");
                    }
                    else
                    {
                        Debug.Log("Employee ID not found!");
                        // Do something when ID doesn't match
                        ErrorMessage.SetActive(true); // Show error message
                    }
                }
            }
        }
    }
}