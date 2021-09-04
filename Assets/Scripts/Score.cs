using UnityEngine;

public class Score : Base
{
    public Transform player;


    void Update()
    {
        gameManager.UpdateScore(player.position.z);
    }
}
