using UnityEngine;

public class DestroyObstacle : Base
{
    public GameObject obstacle;
    Renderer m_Renderer;
    override public void Start()
    {
        base.Start();
        m_Renderer = obstacle.GetComponent<Renderer>();
    }
    void Update()
    {

        if (!m_Renderer.isVisible)
        {
            Destroy(obstacle);
        }

    }
}
