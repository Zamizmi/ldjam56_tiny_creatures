using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupMenu : MonoBehaviour
{

    [SerializeField] private Button button;

    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            GameLoopManager.Instance.StartWave();
            Hide();
        });
    }
    private void Start()
    {

        GameLoopManager.Instance.OnStateChanged += OnStateChangedHandler;
        Hide();
    }

    private void OnStateChangedHandler(object sender, EventArgs e)
    {
        var latestState = GameLoopManager.Instance.GetActiveState();
        switch (latestState)
        {
            case GameLoopManager.GameStates.SETUP:
                Show();
                break;
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
