using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoopManager : MonoBehaviour
{
    public event EventHandler<OnStateChangedArgs> OnStateChanged;
    public class OnStateChangedArgs : EventArgs
    {
        public LevelSO activeLevelSO;
    }
    public static GameLoopManager Instance
    {
        get;
        private set;
    }
    private void Awake()
    {
        Instance = this;
    }
    public enum GameStates
    {
        PAUSE,
        SETUP,
        GAME_ACTIVE,
        GAME_WON,
        GAME_OVER
    }

    [SerializeField] private GameStates activeState;
    [SerializeField] private LevelSO[] allLevels;
    [SerializeField] private int activeLevelIndex;

    private void Start()
    {
        activeLevelIndex = 0;
        activeState = GameStates.PAUSE;
        WaveManager.Instance.OnWaveCompleted += OnWaveCompletedHandler;
    }

    public LevelSO GetActiveLevel()
    {
        return allLevels[activeLevelIndex];
    }

    public void RestartLevel()
    {
        StartSetup();
    }

    private void OnWaveCompletedHandler(object sender, EventArgs e)
    {
        activeLevelIndex++;
        if (activeLevelIndex >= allLevels.Length)
        {
            activeState = GameStates.GAME_WON;
            OnStateChanged?.Invoke(this, new OnStateChangedArgs
            {
                activeLevelSO = allLevels[0]
            });
            return;
        }
        StartSetup();
    }

    public void GameLost()
    {
        SoundManager.Instance.PlayGameOverSound();
        activeState = GameStates.GAME_OVER;
        OnStateChanged?.Invoke(this, new OnStateChangedArgs
        {
            activeLevelSO = allLevels[activeLevelIndex]
        });
    }

    public void StartSetup()
    {
        activeState = GameStates.SETUP;
        OnStateChanged?.Invoke(this, new OnStateChangedArgs
        {
            activeLevelSO = allLevels[activeLevelIndex]
        });
    }

    public void StartWave()
    {
        activeState = GameStates.GAME_ACTIVE;
        OnStateChanged?.Invoke(this, new OnStateChangedArgs
        {
            activeLevelSO = allLevels[activeLevelIndex]
        });
    }

    public GameStates GetActiveState()
    {
        return activeState;
    }

    public bool ActionsAllowed()
    {
        return activeState == GameStates.SETUP || activeState == GameStates.GAME_ACTIVE;
    }
}
