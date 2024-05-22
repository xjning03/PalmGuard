using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class Answer : MonoBehaviour
{
 

    public GameObject ShowCorrect;
    public GameObject ShowWrong;
    public DialogController Dialog;
    public ScoreManager scoreManager;


    private int correctButtonID = 1; 

   
    public void OnButtonPress(int buttonID)
    {
        if (buttonID == correctButtonID)
        {
            updateScore();
            setVisibility();
            Debug.Log("Correct button pressed!");
        }
        else
        {
            ShowWrong.SetActive(true);
            Debug.Log("Incorrect button pressed.");
        }
    }

    public void setVisibility()
    {
        if (ShowCorrect != null)
        {
            ShowCorrect.SetActive(true);
        }
        else
        {
            Debug.LogError("ShowCorrect GameObject is not assigned.");
        }
    }

    public void updateScore()
    {
        scoreManager.SubmitScore(10);
    }
}
