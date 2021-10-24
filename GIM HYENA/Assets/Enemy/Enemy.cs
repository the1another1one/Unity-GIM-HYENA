using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header ("COMPONENT")]
    [SerializeField] private SpriteRenderer enemy_size;
    [SerializeField] private LayerMask ground_layer;
    [SerializeField] private Rigidbody2D rb;

    [Header("HORIZONTAL MOVEMENT")]
    [SerializeField] private float move_speed;
    [SerializeField] private float raycast_height;
    [SerializeField] private bool is_left_ground;
    [SerializeField] private bool is_right_ground;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(move_speed, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        is_right_ground = Physics2D.Raycast(transform.position + Vector3.right * (enemy_size.bounds.size.x / 2), Vector2.down, raycast_height, ground_layer); //mendeteksi apakah terdapat collider di kanan bawah
        is_left_ground = Physics2D.Raycast(transform.position + Vector3.left * (enemy_size.bounds.size.x / 2), Vector2.down, raycast_height, ground_layer); //mendeteksi apakah terdapat collider di kiri bawah
    }

    private void FixedUpdate()
    {
        Movement(is_right_ground, is_left_ground);
    }

    private void Movement (bool is_right_ground, bool is_left_ground)
    {
        if ((is_right_ground == true && is_left_ground == false) || (is_right_ground == false && is_left_ground == true))
        {
            move_speed = -move_speed;
        }
        rb.velocity = new Vector2(move_speed, rb.velocity.y);


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + Vector3.right * (enemy_size.bounds.size.x / 2), transform.position + Vector3.down * raycast_height + Vector3.right * (enemy_size.bounds.size.x / 2));
        Gizmos.DrawLine(transform.position + Vector3.left * (enemy_size.bounds.size.x / 2), transform.position + Vector3.down * raycast_height + Vector3.left * (enemy_size.bounds.size.x / 2));
    }
}
