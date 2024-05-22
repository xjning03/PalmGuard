
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
//using UnityEditor.UIElements; // Comment this out or replace with necessary namespace
#endif


public class LoadChat : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadScene()
    {
        SceneManager.LoadSceneAsync("Whisper Sample");
    }
}

