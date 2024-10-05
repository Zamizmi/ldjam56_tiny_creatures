using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFood : MonoBehaviour
{
    [SerializeField] private AnimalBehaviour.AnimalType animalType;
    [SerializeField] private float reducesHunger;
    [SerializeField] private bool isActive;

    public float Eat(AnimalBehaviour animal)
    {
        isActive = true;
        Destroy(gameObject, 1f);
        if (animalType == animal.animalType) return reducesHunger;
        return reducesHunger / 2;
    }

    public bool IsFree()
    {
        return !isActive;
    }
}
