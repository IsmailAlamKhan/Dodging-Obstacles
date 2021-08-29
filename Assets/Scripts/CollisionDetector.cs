
using UnityEngine;
public class CollisionDetector : Base
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            gameManager.GameOver();
        }
        if (collision.gameObject.tag == "Finish")
        {
            gameManager.WinGame();
        }
    }
}
