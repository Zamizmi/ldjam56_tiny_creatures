using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class FoodInLevelSO : ScriptableObject
{
    public LevelSO levelRelated;
    public FoodSO foodSO;
    public int totalFoodAmount;
}
