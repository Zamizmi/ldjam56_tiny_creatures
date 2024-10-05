using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
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

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(Goal.Instance.transform.position);
        isEating = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BaseFood foodToEat))
        {
            StartEating(foodToEat.Eat(this));
        };
    }

    private void Update()
    {
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
                isEating = false;
                agent.SetDestination(Goal.Instance.transform.position);
            }
        }
    }

    public bool IsHungry()
    {
        return hungryAmount > 0;
    }

    private void StartEating(float amount)
    {
        isEating = true;
        hungryAmount -= amount;
        agent.SetDestination(transform.position);
        if (!IsHungry()) { Sleep(); hungryAmount = 0f; }
    }

    private void Sleep()
    {
        GetComponent<Rigidbody>().detectCollisions = false;
        agent.enabled = false;
        OnAnimalSatisfied?.Invoke(this, EventArgs.Empty);
    }
}
