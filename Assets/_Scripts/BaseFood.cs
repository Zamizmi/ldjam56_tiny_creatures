using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFood : MonoBehaviour
{
    [SerializeField] private AnimalBehaviour.AnimalType animalType;
    [SerializeField] private float reducesHunger;
    [SerializeField] private bool isActive;

    private void Start()
    {
        SoundManager.Instance.PlaySpawnFoodSound(transform.position);
    }

    public float Eat(AnimalBehaviour animal)
    {
        isActive = true;
        Destroy(gameObject, animal.GetTimeToEat());
        if (animalType == animal.animalType) return reducesHunger;
        return reducesHunger / 2;
    }

    public bool IsFree()
    {
        return !isActive;
    }
}
