using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform tf;
    [SerializeField] private BoxCollider2D bc;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float max_speed = 20.0f;
    [SerializeField] private float acceleration = 50.0f;
    private Vector2 movement;
    private bool is_grounded;


    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (tf.position.y < -100)
        {
            tf.position = new Vector3(0, 0, 0);
            rb.velocity = new Vector2(0, 0);
        }

        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Debug.Log(movement);

        if (Input.GetAxis("Horizontal") == 0 && rb.velocity.x != 0)
        {
            rb.drag = 0.8f;
        }

        
    }
    void FixedUpdate()
    {
    if (Mathf.Abs(movement.x) != 0)
        {
            Debug.Log("Move detected !");
            rb.AddForce(movement * acceleration);
        }
    }
    
    //rb.velocity = movement * max_speed * Time.deltaTime;
        
}
