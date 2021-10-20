using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer enemy_size;
    [SerializeField] private LayerMask ground_layer;
    [SerializeField] private float raycast_height;
    [SerializeField] private bool is_left_ground;
    [SerializeField] private bool is_right_ground;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        is_right_ground = Physics2D.Raycast(transform.position + Vector3.right * enemy_size.bounds.size.x / 2, Vector2.down, raycast_height, ground_layer);
        is_left_ground = Physics2D.Raycast(transform.position + Vector3.left * enemy_size.bounds.size.x / 2, Vector2.down, raycast_height, ground_layer);
        Debug.Log(is_right_ground);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + Vector3.right * enemy_size.bounds.size.x / 2, transform.position + Vector3.down * raycast_height + Vector3.right * enemy_size.bounds.size.x / 2);
        Gizmos.DrawLine(transform.position + Vector3.left * enemy_size.bounds.size.x / 2, transform.position + Vector3.down * raycast_height + Vector3.left * enemy_size.bounds.size.x / 2);
    }
}
