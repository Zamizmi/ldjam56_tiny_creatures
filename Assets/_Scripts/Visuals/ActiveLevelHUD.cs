using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveLevelHUD : MonoBehaviour
{
    [SerializeField] private Transform visualPrefab;
    [SerializeField] private AnimalSO[] animalSOs;

    private void Start()
    {
        WaveManager.Instance.OnWavePrepared += WavePreparedHandler;
    }

    private void WavePreparedHandler(object sender, WaveManager.OnWaveStartedArgs e)
    {
        animalSOs = e.animalsToSpawn;
        UpdateVisual();
    }

    private void Awake()
    {
        visualPrefab.gameObject.SetActive(false);
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == visualPrefab) continue;
            Destroy(child.gameObject);
        }
        foreach (AnimalSO animalSO in animalSOs)
        {
            Transform iconTransform = Instantiate(visualPrefab, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<SingleWaveAnimalUI>().SetAnimalSO(animalSO);
        }
    }
}
