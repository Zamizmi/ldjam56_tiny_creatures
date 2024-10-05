using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedVisual : MonoBehaviour
{
    [SerializeField] private WalkOnPlatform basePlatform;
    [SerializeField] private GameObject[] visualGameObjectArray;

    private void OnMouseEnter()
    {
        if (!basePlatform.HasFood()) Show();
    }
    private void OnMouseExit()
    {
        Hide();
    }

    public void Show()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }
}
