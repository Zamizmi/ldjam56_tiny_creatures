using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour, IHasProgress
{
    public static WaveManager Instance
    {
        get;
        private set;
    }
    private void Awake()
    {
        Instance = this;
    }
    public event EventHandler<OnWaveStartedArgs> OnWaveStarted;
    public event EventHandler<OnWaveStartedArgs> OnWavePrepared;
    public event EventHandler OnWaveCompleted;

    public class OnWaveStartedArgs : EventArgs
    {
        public AnimalSO[] animalsToSpawn;
    }
    [SerializeField] private AnimalSO[] waveContent;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int activeIndex = 0;
    [SerializeField] private float timeBetweenSpawns = 2f;
    [SerializeField] private float spawnTimer = 0f;
    [SerializeField] private int fedAnimals = 0;
    [SerializeField] private GenericAudioSO audios;
    private bool isSpawning;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    private void Start()
    {
        GameLoopManager.Instance.OnStateChanged += OnStateChangedHandler;
    }

    private void OnStateChangedHandler(object sender, GameLoopManager.OnStateChangedArgs e)
    {
        var latestState = GameLoopManager.Instance.GetActiveState();
        switch (latestState)
        {
            case GameLoopManager.GameStates.SETUP:
                PrepareWave(e.activeLevelSO);
                break;
            case GameLoopManager.GameStates.GAME_ACTIVE:
                StartWave();
                break;
            case GameLoopManager.GameStates.GAME_OVER:
                HandleGameOver();
                break;
        }
    }

    private void HandleGameOver()
    {
        ResetTimers();
        isSpawning = false;
    }

    private void PrepareWave(LevelSO newLevel)
    {
        ClearAnimals();
        ResetTimers();
        isSpawning = false;
        waveContent = new AnimalSO[newLevel.animalsToSpawn.Length];
        waveContent = newLevel.animalsToSpawn;
        OnWavePrepared?.Invoke(this, new OnWaveStartedArgs
        {
            animalsToSpawn = waveContent
        });
    }

    private void ResetTimers()
    {
        activeIndex = 0;
        spawnTimer = 0f;
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            progressNormalized = spawnTimer / timeBetweenSpawns
        });
    }

    private void ClearAnimals()
    {
        AnimalBehaviour[] animals = FindObjectsOfType<AnimalBehaviour>();
        for (int i = 0; i < animals.Length; i++)
        {
            Destroy(animals[i].gameObject);
        }
    }

    private void StartWave()
    {
        fedAnimals = 0;
        isSpawning = true;
        OnWaveStarted?.Invoke(this, new OnWaveStartedArgs
        {
            animalsToSpawn = waveContent
        });
    }

    private void SpawnNext()
    {
        if (activeIndex < waveContent.Length)
        {
            AnimalBehaviour spawnedAnimal = Instantiate(waveContent[activeIndex].animalToSpawn, spawnPoint.position, Quaternion.identity).GetComponent<AnimalBehaviour>();
            spawnedAnimal.OnAnimalSatisfied += OnAnimalFedHandler;
            activeIndex++;
        }
    }

    private void OnAnimalFedHandler(object sender, EventArgs e)
    {
        fedAnimals++;
        if (fedAnimals == waveContent.Length)
        {
            SoundManager.Instance.PlayVictorySound();
            StartCoroutine(DelayedCompleted());
        }
    }

    private IEnumerator DelayedCompleted()
    {
        yield return new WaitForSeconds(2);
        OnWaveCompleted?.Invoke(this, EventArgs.Empty);
    }

    private void Update()
    {

        if (isSpawning)
        {
            if (activeIndex >= waveContent.Length)
            {
                isSpawning = false;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 1f
                });
            }
            spawnTimer += Time.deltaTime;
            if (spawnTimer > timeBetweenSpawns)
            {
                SpawnNext();
                spawnTimer = 0f;
            }
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = spawnTimer / timeBetweenSpawns
            });
        }
    }
}
