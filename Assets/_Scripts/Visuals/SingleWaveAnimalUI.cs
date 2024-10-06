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
    [SerializeField] private Image spawnedAnimal;
    [SerializeField] private Image sleepImage;

    public void SetAnimalSO(AnimalSO animalSO)
    {
        animal = animalSO;
        animalImage.sprite = animal.animalSprite;
    }
}
