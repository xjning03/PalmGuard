
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
//using UnityEditor.UIElements; // Comment this out or replace with necessary namespace
#endif


public enum GameState { FreeRoam, Dialog }

    public class GameController : MonoBehaviour
{
    [SerializeField] Controller playerController;

    GameState state;

    private void Start()
    {
        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };

        DialogManager.Instance.OnHideDialog += () =>
        {   
            if(state == GameState.Dialog)
            state = GameState.FreeRoam;
        };

    }

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
     }
}

