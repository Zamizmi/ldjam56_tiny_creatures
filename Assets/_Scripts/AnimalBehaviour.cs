using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalBehaviour : MonoBehaviour
{
    public enum AnimalType
    {
        CAT,
        DOG,
        PIG,
        COW,
    }
    public EventHandler OnAnimalSatisfied;
    public AnimalType animalType;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float hungryAmount;
    [SerializeField] private BaseFood foodToEat;
    [SerializeField] private bool isEating = false;
    [SerializeField] private float timeToEat;
    [SerializeField] private float eatingTimer;
    [SerializeField] private AnimalVisual animalVisual;
    private Animator animator;
    string isMoving = "IsMoving";
    string isEatingStr = "IsEating";
    string isSleepingStr = "IsSleeping";
    [SerializeField] private AnimalSO animalSO;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animalVisual = GetComponent<AnimalVisual>();
        isEating = false;
        StartWalking();
        SoundManager.Instance.PlayRandomSound(animalSO.spawnClips, transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BaseFood foodToEat))
        {
            if (foodToEat.IsFree())
            {
                StartEating(foodToEat.Eat(this));
            }
        };
    }

    public float GetTimeToEat()
    {
        return timeToEat;
    }

    private void Update()
    {
        animator.SetBool(isMoving, agent.velocity.magnitude > 0.01f);
        if (IsHungry())
        {
            if (isEating)
            {
                eatingTimer += Time.deltaTime;
            }
            else
            {
                eatingTimer = 0;
            }
            if (eatingTimer > timeToEat)
            {
                StartWalking();
            }
        }
    }

    public bool IsHungry()
    {
        return hungryAmount > 0;
    }

    private void StartWalking()
    {
        isEating = false;
        animator.SetBool(isEatingStr, false);
        animalVisual.SetIdleMode();
        agent.enabled = true;
        agent.SetDestination(Goal.Instance.transform.position);
    }

    private void StartEating(float amount)
    {
        isEating = true;
        SoundManager.Instance.PlayRandomSound(animalSO.eatHappyClips, transform.position);
        animator.SetBool(isEatingStr, true);
        animalVisual.SetEatMode();
        hungryAmount -= amount;
        agent.enabled = false;
        if (!IsHungry()) { StartCoroutine(DelayedSleep()); }
    }

    private void StartSleep()
    {
        animator.SetBool(isSleepingStr, true);
        animalVisual.SetSleepMode();
        agent.enabled = false;
        OnAnimalSatisfied?.Invoke(this, EventArgs.Empty);
    }

    private IEnumerator DelayedSleep()
    {
        yield return new WaitForSeconds(timeToEat);
        StartSleep();
        hungryAmount = 0f;
    }
}
