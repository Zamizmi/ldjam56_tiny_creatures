using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FoodSO : ScriptableObject
{
    public GameObject foodToSpawn;
    public string foodName;

    public Sprite foodSprite;
    public AnimalBehaviour.AnimalType animalType;
}
