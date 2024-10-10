using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public static Goal Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameLoopManager.Instance.GetActiveState() == GameLoopManager.GameStates.GAME_ACTIVE)
        {
            if (other.gameObject.TryGetComponent(out AnimalBehaviour animal))
            {
                GameLoopManager.Instance.GameLost();
            }
        }
    }

}
