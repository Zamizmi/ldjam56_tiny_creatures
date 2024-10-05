using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleWaveAnimalUI : MonoBehaviour
{
    [SerializeField] private AnimalSO animal;
    [SerializeField] private Image animalImage;

    public void SetAnimalSO(AnimalSO animalSO)
    {
        animal = animalSO;
        animalImage.sprite = animal.animalSprite;
    }
}
