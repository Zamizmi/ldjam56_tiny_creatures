using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkOnPlatform : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private bool hasFood;
    private SelectedVisual visual;

    private void Start()
    {
        visual = GetComponent<SelectedVisual>();
        GameLoopManager.Instance.OnStateChanged += OnStateChangedHandler;
    }

    private void OnStateChangedHandler(object sender, GameLoopManager.OnStateChangedArgs e)
    {
        var latestState = GameLoopManager.Instance.GetActiveState();
        switch (latestState)
        {
            case GameLoopManager.GameStates.SETUP:
                PrepareWave();
                break;
        }
    }

    private void PrepareWave()
    {
        hasFood = false;
    }

    void OnMouseDown()
    {
        if (!GameLoopManager.Instance.ActionsAllowed()) return;
        SpawnFood();
    }

    public bool HasFood()
    {
        return hasFood;
    }

    private void SpawnFood()
    {
        if (!hasFood && FoodSpawnManager.Instance.CanSpawn())
        {
            FoodSpawnManager.Instance.SpawnInPosition(spawnPoint.position, spawnPoint);
            hasFood = true; ;
            visual.Hide();
        }
    }
}
