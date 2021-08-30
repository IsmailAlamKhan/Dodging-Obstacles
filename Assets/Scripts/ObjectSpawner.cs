
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
        float max = (surface.localScale.x - 4) - newObject.transform.localScale.x;

        spawnPos = new Vector3(Random.Range(-max, max), surface.localPosition.x + 1, spawnPos.z + Random.Range(20, 40));

        if (gameManager.gameIsRunning())
        {
            Instantiate(newObject, spawnPos, Quaternion.identity);
        }
    }
}



