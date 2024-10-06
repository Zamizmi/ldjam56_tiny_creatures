using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class GenericAudioSO : ScriptableObject
{
    public AudioClip victory;
    public AudioClip gameLost;
    public AudioClip[] placeFood;
    public AudioClip[] menuHover;
    public AudioClip soundtrack;
}
