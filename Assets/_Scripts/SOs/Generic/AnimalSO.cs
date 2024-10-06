using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AnimalSO : ScriptableObject
{
    public GameObject animalToSpawn;
    public string animalName;

    public Sprite animalSprite;
    public AnimalBehaviour.AnimalType animalType;
    public AudioClip[] spawnClips;
    public AudioClip[] eatHappyClips;
    public AudioClip[] eatMiddleClips;
    public AudioClip[] skipFoodClips;
}
