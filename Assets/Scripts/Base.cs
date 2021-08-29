using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Base : MonoBehaviour
{
    public GameManager gameManager;
    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
}

