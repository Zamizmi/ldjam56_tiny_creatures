using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveLevelFoodHUD : MonoBehaviour
{
    [SerializeField] private Transform visualPrefab;
    [SerializeField] private FoodData[] allFoods;

    private void Start()
    {
        FoodSpawnManager.Instance.OnFoodStatusChanged += FoodStatusChangedHandler;
    }

    private void FoodStatusChangedHandler(object sender, FoodSpawnManager.OnFoodStatusChangedEventArgs e)
    {
        allFoods = e.foods;
        UpdateVisual();
    }

    private void Awake()
    {
        visualPrefab.gameObject.SetActive(false);
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == visualPrefab) continue;
            Destroy(child.gameObject);
        }
        foreach (FoodData food in allFoods)
        {
            Transform iconTransform = Instantiate(visualPrefab, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<SingleFoodUI>().SetFoodData(food);
        }
    }
}
