
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
//using UnityEditor.UIElements; // Comment this out or replace with necessary namespace
#endif


public class HideObject : MonoBehaviour
{
    public GameObject item;
    // Start is called before the first frame update
    public void setVisibility()
    {
        item.SetActive(false);
    }
}
