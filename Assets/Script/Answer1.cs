using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class Answer1 : MonoBehaviour
{


    public GameObject ShowCorrect;
    public GameObject ShowWrong;
    public GameObject ShowWrong1;
    public GameObject ShowWrong2;
    public DialogController Dialog;
    public ScoreManager scoreManager;


    private int correctButtonID = 1;
    private int wrongButtonID1 = 2;
    private int wrongButtonID2 = 3;


    public void OnButtonPress(int buttonID)
    {
        if (buttonID == correctButtonID)
        {
            updateScore();
            setVisibility();
            Debug.Log("Correct button pressed!");
        }
        else if (buttonID == wrongButtonID1)

        {
            ShowWrong.SetActive(true);
            Debug.Log("Incorrect button pressed.");
        }
        else if (buttonID == wrongButtonID2)

        {
            ShowWrong1.SetActive(true);
            Debug.Log("Incorrect button pressed.");
        }
        else
            ShowWrong2.SetActive(true);
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
