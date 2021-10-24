using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSystem : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Rigidbody2D rb_player;
    private Transform tf_player;
    private SpriteRenderer sprite_player;
    private Color ori_color;
    private float t = 0f; // measure time
    [SerializeField] private float player_life;
    private Transform contact;

    private void Start()
    {
        rb_player = player.GetComponent<Rigidbody2D>();
        tf_player = player.GetComponent<Transform>();
        sprite_player = player.GetComponent<SpriteRenderer>();
        ori_color = sprite_player.color; // stroring original color
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < -10f || player_life <= 0)
        {
            FindObjectOfType<GameManager>().GameOver(); // run GameOver function on GameManager script
        }

        if (sprite_player.color == Color.red && (Time.time - t) >= 1)
        {
            sprite_player.color = ori_color;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            sprite_player.color = Color.Lerp(ori_color, Color.red, 1); // change color when collide with enemy
            t = Time.time;
                

            player_life -= 1;
            contact = collision.gameObject.transform; // store where the enemy position

            rb_player.velocity = new Vector2(0, 0);
            rb_player.AddForce((tf_player.position - contact.position) * 10, ForceMode2D.Impulse); 
            // bounce off from enemy
            Debug.Log(contact);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "finish")
        {

            FindObjectOfType<GameManager>().GameOver(); // run GameOver function on GameManager script
        }
    }


}
