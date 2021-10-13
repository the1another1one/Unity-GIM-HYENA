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
    [SerializeField] private float jump_power;
    [SerializeField] private float gravity;
    [SerializeField] private float fall_multiplier;
    [SerializeField] private bool is_grounded;
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

        LinearDrag(direction);
        Debug.Log(rb.drag + ", " + rb.gravityScale);
    }

    void FixedUpdate()
    {
        MoveCharacter(direction, is_grounded);
    }

    private void LinearDrag(Vector2 direction)
    {
        bool changing_direction = (rb.velocity.x > 0 && direction.x < 0) || (rb.velocity.x < 0 && direction.x > 0);

        if (is_grounded)
        {
            if (Mathf.Abs(direction.x) < 0.4f || changing_direction) // player will slow down when there is no input on the ground
            {
                rb.drag = linear_drag;
            }
            else
            {
                rb.drag = 0;
            }
            rb.gravityScale = 0f;
        }
        else
        {
            rb.gravityScale = gravity;  
            rb.drag = linear_drag * 0.3f;
            if (rb.velocity.y < 0) // player will falling down fast
            {
                rb.gravityScale = gravity * fall_multiplier;
            }
            else if (rb.velocity.y > 0 && Input.GetButton("Jump")) // higher jump while holding the button, jump button can be seen at edit -> project setting -> input manager
            {
                rb.gravityScale = gravity * (fall_multiplier / 2);
            }
        }
    }

    private void MoveCharacter(Vector2 direction, bool is_grounded)
    {
        if (Mathf.Abs(rb.velocity.x) < max_speed) // check if player speed is already at max speed
        {
            rb.AddForce(Vector2.right * direction.x * acceleration); // movement in x axis
        }
        else if (Mathf.Abs(rb.velocity.x) >= max_speed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * max_speed, rb.velocity.y);
        }

        if (is_grounded && direction.y == 1) 
        {
            rb.AddForce(Vector2.up * jump_power ,ForceMode2D.Impulse); // algorithm for jumping
        }
    }

    //check if player is on ground every frame
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            is_grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            is_grounded = false;
        }
    }
    //check if player is on ground every frame 
}
