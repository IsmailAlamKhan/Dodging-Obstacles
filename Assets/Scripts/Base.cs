
using UnityEngine;

public abstract class Base : MonoBehaviour
{
    public GameManager gameManager;
    virtual public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
}

