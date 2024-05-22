using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    [SerializeField] Dialog dialog;
    public void startDialog()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    }
}
