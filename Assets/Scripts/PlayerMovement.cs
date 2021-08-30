
using UnityEngine;

public class PlayerMovement : Base
{
    public Rigidbody rb;
    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;

    bool isMovingLeft = false;
    bool isMovingRight = false;

    void FixedUpdate()
    {
        float _forwardForce = forwardForce * gameManager.speedMultiplier;
        float _sidewaysForce = sidewaysForce * gameManager.speedMultiplier;
        rb.AddForce(0, 0, _forwardForce * Time.deltaTime);
        if (isMovingLeft)
        {
            rb.AddForce(-_sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if (isMovingRight)
        {
            rb.AddForce(_sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if (rb.position.y < -1f)
        {
            gameManager.GameOver();
        }
    }
    void Update()
    {
        isMovingLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        isMovingRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
    }
}
