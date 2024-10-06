using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFood : MonoBehaviour
{
    [SerializeField] private AnimalBehaviour.AnimalType animalType;
    [SerializeField] private float reducesHunger;
    [SerializeField] private bool isActive;
    [SerializeField] private FoodSO foodSO;

    private void Start()
    {
        SoundManager.Instance.PlaySpawnFoodSound(transform.position);
    }

    public bool IsMatchForAnimal(AnimalBehaviour animal)
    {
        if (animal.animalType == AnimalBehaviour.AnimalType.PIG) return true;
        if (animalType == animal.animalType) return true;
        return false;
    }

    public float Eat(AnimalBehaviour animal)
    {
        isActive = true;
        Destroy(gameObject, animal.GetTimeToEat());
        if (IsMatchForAnimal(animal)) return reducesHunger;
        return reducesHunger / 2;
    }

    public bool IsFree()
    {
        return !isActive;
    }
}
