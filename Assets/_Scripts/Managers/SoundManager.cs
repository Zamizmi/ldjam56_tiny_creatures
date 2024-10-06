using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private GenericAudioSO audios;
    public static SoundManager Instance
    {
        get;
        private set;
    }
    private void Awake()
    {
        Instance = this;
    }

    public void PlayVictorySound()
    {
        PlaySound(audios.victory, Camera.main.transform.position, 0.2f);
    }
    public void PlayGameOverSound()
    {
        PlaySound(audios.gameLost, Camera.main.transform.position, 0.2f);
    }

    public void PlaySelectFoodSound()
    {
        PlayRandomSound(audios.menuHover, Camera.main.transform.position, 0.1f);
    }

    public void PlaySpawnFoodSound(Vector3 position)
    {
        PlayRandomSound(audios.placeFood, position);
    }

    private void PlaySound(AudioClip audio, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audio, position, volume);
    }

    public void PlayRandomSound(AudioClip[] audios, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audios[Random.Range(0, audios.Length)], position, volume);
    }
}
