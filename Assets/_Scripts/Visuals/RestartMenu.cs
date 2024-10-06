using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartMenu : MonoBehaviour
{

    [SerializeField] private Button restartLevelButton;

    private void Awake()
    {
        restartLevelButton.onClick.AddListener(() =>
        {
            GameLoopManager.Instance.RestartLevel();
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
            case GameLoopManager.GameStates.GAME_OVER:
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
