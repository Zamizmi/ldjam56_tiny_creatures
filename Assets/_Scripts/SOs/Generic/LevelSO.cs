using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelSO : ScriptableObject
{
    public AnimalSO[] animalsToSpawn;
    public FoodInLevelSO[] foodsToSpawn;
}
