
using UnityEngine;

public class ObjectSpawner : Base
{
    public Transform player;
    public GameObject[] objects;
    public Vector3 spawnPos;
    public Transform surface;

    override public void Start()
    {
        base.Start();
        Spawn();
    }

    void Update()
    {

        float distanceToPlayer = Vector3.Distance(player.position, spawnPos);
        float spawnDis = Random.Range(0, 40 - (gameManager.score / 500));

        if (distanceToPlayer < spawnDis)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        GameObject newObject = objects[Random.Range(0, objects.Length)];
        float surfaceWidthInHalf = (surface.localScale.x / 2) - 2;
        float newObjectWidth = newObject.transform.localScale.x;
        float x = Random.Range(-surfaceWidthInHalf, surfaceWidthInHalf);


        spawnPos = new Vector3(x, surface.transform.localScale.y + newObject.transform.localScale.y, spawnPos.z + Random.Range(20, 40));

        if (gameManager.gameIsRunning())
        {
            Instantiate(newObject, spawnPos, newObject.transform.rotation);
        }
    }
}



