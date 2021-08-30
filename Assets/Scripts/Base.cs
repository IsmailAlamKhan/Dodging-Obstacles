
using UnityEngine;

public abstract class Base : MonoBehaviour
{
    public GameManager gameManager;
    public Vector3 screenBounds;
    virtual public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }
}

