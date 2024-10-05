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
}
