using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FoodSpawnManager : MonoBehaviour
{
    public event EventHandler<OnFoodStatusChangedEventArgs> OnFoodStatusChanged;
    public class OnFoodStatusChangedEventArgs : EventArgs
    {
        public FoodData[] foods;
    }
    public static FoodSpawnManager Instance
    {
        get;
        private set;
    }
    [SerializeField] private FoodData activeFoodToSpawn;
    [SerializeField] private FoodData[] allFoodData;

    [SerializeField] private FoodInLevelSO[] availableFoods;
    [SerializeField] private Transform foodDataPrefab;
    [SerializeField] private LayerMask landLayer;

    private void Awake()
    {
        Instance = this;
        foodDataPrefab.gameObject.SetActive(false);
    }

    private void Start()
    {
        GameLoopManager.Instance.OnStateChanged += OnStateChangedHandler;
    }

    private void OnStateChangedHandler(object sender, GameLoopManager.OnStateChangedArgs e)
    {
        var latestState = GameLoopManager.Instance.GetActiveState();
        switch (latestState)
        {
            case GameLoopManager.GameStates.SETUP:
                PrepareWave(e.activeLevelSO);
                break;
        }
    }

    private void PrepareWave(LevelSO newLevel)
    {
        ClearFoods();
        foreach (Transform child in transform)
        {
            if (child == foodDataPrefab) continue;
            Destroy(child.gameObject);
        }
        availableFoods = newLevel.foodsToSpawn;
        allFoodData = new FoodData[availableFoods.Length];
        for (int i = 0; i < availableFoods.Length; i++)
        {

            FoodData foodData = Instantiate(foodDataPrefab, transform).GetComponent<FoodData>();
            foodData.SetFoodAmount(availableFoods[i].foodSO, availableFoods[i].totalFoodAmount);
            allFoodData[i] = foodData;
        }
        SetActiveFood(availableFoods[0].foodSO);
    }

    private void ClearFoods()
    {
        BaseFood[] foods = FindObjectsOfType<BaseFood>();
        for (int i = 0; i < foods.Length; i++)
        {
            Destroy(foods[i].gameObject);
        }
    }

    public void SetActiveFood(FoodSO newFood)
    {
        foreach (var item in allFoodData)
        {
            if (item.foodSO == newFood) { activeFoodToSpawn = item; item.SetActive(); }
            else
            {
                item.SetNotActive();
            };
        }
        OnFoodStatusChanged?.Invoke(this, new OnFoodStatusChangedEventArgs
        {
            foods = allFoodData
        });
    }

    public FoodInLevelSO[] GetAvailableFoods()
    {
        return availableFoods;
    }

    public bool CanSpawn()
    {
        return activeFoodToSpawn.currentAmount > 0;
    }

    public void SpawnInPosition(Vector3 position, Transform parent)
    {
        activeFoodToSpawn.SpawnFood(position, parent);
        OnFoodStatusChanged?.Invoke(this, new OnFoodStatusChangedEventArgs
        {
            foods = allFoodData
        });
    }
}
