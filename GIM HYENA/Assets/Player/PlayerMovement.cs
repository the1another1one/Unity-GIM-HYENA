using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("COMPONENT")]
    [SerializeField] private Transform tf;
    [SerializeField] private Rigidbody2D rb;

    [Header("HORIZONTAL MOVEMENT")]
    private Vector2 direction;
    [SerializeField] private float max_speed;
    [SerializeField] private float acceleration;
    [SerializeField] private float linear_drag;

    [Header("VERTICAL MOVEMENT")]
    [SerializeField] private float jump_power;
    [SerializeField] private float gravity;
    [SerializeField] private float fall_multiplier;

    [Header("GROUND CHECK")]
    [SerializeField] private SpriteRenderer player_size;
    [SerializeField] private LayerMask ground_layer;
    [SerializeField] private float raycast_height;
    [SerializeField] private bool is_grounded;
    [SerializeField] private bool is_left_ground;
    [SerializeField] private bool is_center_ground;
    [SerializeField] private bool is_right_ground;

    // Update is called once per frame
    private void Update()
    {
        if (tf.position.y < -50)
        {
            tf.position = new Vector3(0, 0, 0); // respawn point if player fall into abyss
            rb.velocity = new Vector2(0, 0);
        }

        // detect which direction player want to move
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); 

        // determine the translation drag

        //ground checking
        is_left_ground = Physics2D.Raycast(transform.position + Vector3.left * player_size.bounds.size.x / 2, Vector2.down, raycast_height, ground_layer); //mendeteksi apakah terdapat collider di kiri bawah
        is_center_ground = Physics2D.Raycast(transform.position , Vector2.down, raycast_height, ground_layer); //mendeteksi apakah terdapat collider di bawah
        is_right_ground = Physics2D.Raycast(transform.position + Vector3.right * player_size.bounds.size.x / 2, Vector2.down, raycast_height, ground_layer); //mendeteksi apakah terdapat collider di kanan bawah
        is_grounded = GroundCheck(is_left_ground, is_center_ground, is_right_ground); 

        LinearDrag(direction);
        // jump 
        if (is_grounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        MoveCharacter(direction);
    }

    private bool GroundCheck(bool is_left_ground, bool is_center_ground, bool is_right_ground)
    {
        if (is_left_ground == false && is_center_ground == false && is_right_ground == false)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + Vector3.left * player_size.bounds.size.x / 2, transform.position + Vector3.down * raycast_height + Vector3.left * player_size.bounds.size.x / 2);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * raycast_height);
        Gizmos.DrawLine(transform.position + Vector3.right * player_size.bounds.size.x / 2, transform.position + Vector3.down * raycast_height + Vector3.right * player_size.bounds.size.x / 2);
    }

    private void LinearDrag(Vector2 direction)
    {
        bool changing_direction = (rb.velocity.x > 0 && direction.x < 0) || (rb.velocity.x < 0 && direction.x > 0);

        if (is_grounded)
        {
            if (Mathf.Abs(direction.x) < 0.4f || changing_direction) // player will slow down when there is no input on the ground or chaning direction
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
            rb.drag = linear_drag * 0.15f;
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

    private void MoveCharacter(Vector2 direction)
    {
        // check if player speed is already at max speed, movement in x axis
        if (Mathf.Abs(rb.velocity.x) < max_speed)
        {
            rb.AddForce(Vector2.right * direction.x * acceleration);
        }
        else if (Mathf.Abs(rb.velocity.x) >= max_speed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * max_speed, rb.velocity.y);
        }
    }
    // algorithm for jumping
    private void Jump()
    {
        rb.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
    }
}
