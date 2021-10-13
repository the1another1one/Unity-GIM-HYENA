using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform tf;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float max_speed;
    [SerializeField] private float acceleration;
    [SerializeField] private float linear_drag;
    private Vector2 direction;

    // Update is called once per frame
    private void Update()
    {
        if (tf.position.y < -50)
        {
            tf.position = new Vector3(0, 0, 0); // respawn point if player fall into abyss
            rb.velocity = new Vector2(0, 0);
        }

        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // detect which direction player want to move   
    }

    void FixedUpdate()
    {
        LinearDrag(direction);
        MoveCharacter(direction.x);
        Debug.Log(rb.drag);
    }

    private void LinearDrag(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) < 0.4f) // player will slow down when there is no input
        {
            rb.drag = linear_drag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void MoveCharacter(float horizontal)
    {
        if (Mathf.Abs(rb.velocity.x) < max_speed) // check if player speed is already at max speed
            {
                rb.AddForce(Vector2.right * horizontal * acceleration); // movement in x axis
            }
        else if (Mathf.Abs(rb.velocity.x) >= max_speed)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * max_speed, rb.velocity.y);
            }
    }

}
