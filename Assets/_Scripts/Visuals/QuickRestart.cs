using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickRestart : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float timeToHold;
    [SerializeField] float timeToHoldTimer = 0f;
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
            case GameLoopManager.GameStates.GAME_ACTIVE:
                Show();
                break;
            default:
                Hide();
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

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            timeToHoldTimer += Time.deltaTime;
            if (timeToHoldTimer >= timeToHold)
            {
                timeToHoldTimer = 0f;
                GameLoopManager.Instance.RestartLevel();
            }
        }
        else
        {
            timeToHoldTimer -= Time.deltaTime;
            if (timeToHoldTimer < 0) timeToHoldTimer = 0;
        }
        image.fillAmount = timeToHoldTimer / timeToHold;
    }

}
