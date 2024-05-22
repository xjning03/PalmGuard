
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
//using UnityEditor.UIElements; // Comment this out or replace with necessary namespace
#endif

public class Wound : MonoBehaviour
{
    // Reference to the Image component you want to change
    public List<Image> imageToChange;

    // Sprites you want to set
    public List<Sprite> newSprite;

    public GameObject ShowCorrect;
    public GameObject ShowCorrect2;
    public GameObject ShowCorrect3;
    public DialogController Dialog;
    public GameObject hide;
    public GameObject hide2;
    public VideoManager video;

    // Current index to track which image/sprite to use
    private int currentIndex = 0;

    // Sequence of correct buttons to press
    private List<int> correctSequence = new List<int> { 1,2,3 }; // Example sequence
    private int currentSequenceIndex = 0;

    // Reference to ScoreManager
    public ScoreManager scoreManager;

    // Method to change the sprite of the image
    public void ChangeImage()
    {
        // Check if the Image component and the new sprite are assigned
        if (imageToChange != null && newSprite != null && imageToChange.Count == newSprite.Count)
        {
            // Check if the current index is within the range
            if (currentIndex >= 0 && currentIndex < imageToChange.Count)
            {
                // Assign the new sprite to the current Image component's sprite property
                imageToChange[currentIndex].sprite = newSprite[currentIndex];
            }
            else
            {
                // Log an error if the current index is out of range
                Debug.LogError("Current index is out of range.");
            }
        }
        else
        {
            // Log an error if the Image component or the new sprite is not assigned
            Debug.LogError("Image or new sprite is not assigned.");
        }
    }

    // Method to handle the button press and check the correct sequence
    public void HandleButtonPress(int button)
    {
        if (button == correctSequence[currentSequenceIndex])
        {
            // Correct button pressed
            currentSequenceIndex++;
            currentIndex++;
            updateScore();

            // Update visibility based on the correct step
            switch (currentSequenceIndex)
            {
                case 1:
                    setVisibility();
                    ChangeImage();
                    break;
                case 2:
                    setVisibility2();
                    ChangeImage();
                    break;
                case 3:
                    setVisibility3();
                    ChangeImage();
                    break;
            }
        }
        else
        {
            // Incorrect button pressed
            Debug.LogError("Incorrect button. Please try again.");
            hide.SetActive(false);
            hide2.SetActive(true);
            Dialog.startDialog();
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
            // Log an error if the ShowCorrect GameObject is not assigned
            Debug.LogError("ShowCorrect GameObject is not assigned.");
        }
    }

    public void setVisibility2()
    {
        if (ShowCorrect2 != null)
        {
            ShowCorrect2.SetActive(true);
        }
        else
        {
            // Log an error if the ShowCorrect GameObject is not assigned
            Debug.LogError("ShowCorrect2 GameObject is not assigned.");
        }
    }

    public void setVisibility3()
    {
        if (ShowCorrect3 != null)
        {
            ShowCorrect3.SetActive(true);
        }
        else
        {
            // Log an error if the ShowCorrect3 GameObject is not assigned
            Debug.LogError("ShowCorrect3 GameObject is not assigned.");
        }
    }

    // Method to update the score
    public void updateScore()
    {
        scoreManager.SubmitScore(10);
    }
    
    public void Video()
    {
        hide2.SetActive(false);
        video.PlayVideo();
    }
}
