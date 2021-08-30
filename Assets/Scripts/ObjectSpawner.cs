
using UnityEngine;

public class ObjectSpawner : Base
{
    public Transform player;
    public GameObject[] objects;
    public Vector3 spawnPos;

    void Update()
    {

        float distanceToPlayer = Vector3.Distance(player.position, spawnPos);
        if (distanceToPlayer < 80)
        {
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        GameObject newObject = objects[Random.Range(0, objects.Length)];
        Vector3 newObjectTransform = newObject.gameObject.transform.position;
        spawnPos = new Vector3(newObjectTransform.x, newObjectTransform.y, spawnPos.z + Random.Range(20, 40));
        if (gameManager.gameIsRunning())
        {
            if (player.position.z > 280)
            {
                Instantiate(newObject, spawnPos, Quaternion.identity);
            }
        }
    }
}



