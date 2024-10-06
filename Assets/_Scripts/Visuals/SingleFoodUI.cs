using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SingleFoodUI : MonoBehaviour
{
    [SerializeField] private FoodData foodData;
    [SerializeField] private int foodCount;
    [SerializeField] private Image foodImage;
    [SerializeField] private GameObject activeImage;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Button selectButton;

    private void Start()
    {
        selectButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySelectFoodSound();
            FoodSpawnManager.Instance.SetActiveFood(foodData.foodSO);
        });
    }

    public void SetFoodData(FoodData newFoodData)
    {
        foodData = newFoodData;
        foodImage.sprite = foodData.foodSO.foodSprite;
        textMesh.text = foodData.currentAmount.ToString();
        if (foodData.isActive)
        {
            ShowActive();
        }
        else
        {
            HideActive();
        }
    }

    private void ShowActive()
    {
        activeImage.SetActive(true);
    }

    private void HideActive()
    {
        activeImage.SetActive(false);
    }
}
