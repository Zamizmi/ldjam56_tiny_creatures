using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodData : MonoBehaviour
{
    [SerializeField] public int currentAmount;
    [SerializeField] public FoodSO foodSO;
    [SerializeField] public bool isActive;

    public void SpawnFood(Vector3 position, Transform parent)
    {
        if (currentAmount < 1) return;
        currentAmount--;
        Instantiate(foodSO.foodToSpawn, position, Quaternion.identity, parent);
    }

    public void SetFoodAmount(FoodSO newFoodSO, int amount)
    {
        foodSO = newFoodSO;
        currentAmount = amount;
    }

    public void SetActive()
    {
        isActive = true;
    }

    public void SetNotActive()
    {
        isActive = false;
    }
}
