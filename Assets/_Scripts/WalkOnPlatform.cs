using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkOnPlatform : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private bool hasFood;
    private SelectedVisual visual;

    private void Start()
    {
        visual = GetComponent<SelectedVisual>();
    }

    void OnMouseDown()
    {
        SpawnFood();
    }

    public bool HasFood()
    {
        return hasFood;
    }

    private void SpawnFood()
    {
        if (!hasFood && FoodSpawnManager.Instance.CanSpawn())
        {
            FoodSpawnManager.Instance.SpawnInPosition(spawnPoint.position, spawnPoint);
            hasFood = true; ;
            visual.Hide();
        }
    }
}
