using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWonMenu : MonoBehaviour
{
    [SerializeField] private Button restartLevelButton;

    private void Awake()
    {
        restartLevelButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
            case GameLoopManager.GameStates.GAME_WON:
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
