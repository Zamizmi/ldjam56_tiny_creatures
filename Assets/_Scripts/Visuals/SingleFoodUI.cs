using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleFoodUI : MonoBehaviour
{
    [SerializeField] private FoodSO foodSO;
    [SerializeField] private int foodCount;
    [SerializeField] private Image foodImage;
    [SerializeField] private TextMeshProUGUI textMesh;

    public void SetFoodSO(FoodSO foodSO, int newCount)
    {
        this.foodSO = foodSO;
        foodImage.sprite = this.foodSO.foodSprite;
        textMesh.text = newCount.ToString();
    }
}
