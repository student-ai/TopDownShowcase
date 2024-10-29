using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlstformerMoverment : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1.0f;
    [SerializeField]
    float jumpSpeed = 2.0f;
    [SerializeField]
    float dashSpeed = 1.0f;
    Rigidbody2D rb;
    bool grounded = false;
    bool dashAble = false;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // left and right movement based on horizontal axis input
        float moveX = Input.GetAxis("Horizontal");
        Vector2 velocity = rb.velocity;
        velocity.x = moveX * moveSpeed;
        rb.velocity = velocity;
        // jump when spacebar is hit
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(new Vector2(0, 100 * jumpSpeed));
            grounded = false;
        }
        anim.SetBool("grounded", grounded);
        anim.SetFloat("y", velocity.y);
        int x = (int)Input.GetAxisRaw("Horizontal");
        anim.SetInteger("x", x);
        // dash when the e key is pressed, regardless of whether or not the player is on the ground

        // bug fixing: wont dash, keycode seems correct, no force is being applied
        if (Input.GetKeyDown(KeyCode.E) && dashAble == true)
        {
            if (x < 0)
            {
                rb.AddForce(new Vector2(-100 * dashSpeed, 0));
                dashAble = false;
            }
            if (x > 0)
            {
                rb.AddForce(new Vector2(100 * dashSpeed, 0));
                dashAble = false;
            }
            velocity.y = 0;
        }
        if(x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            grounded = true;
            dashAble = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            grounded = false;
            if (collision.gameObject.tag != "MovingPlatform")
            {
                gameObject.transform.parent = null;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D platformCollision)
    {
        // if the player is colliding with a moving plaform, attach the two
        if (platformCollision.gameObject.tag == "MovingPlatform")
        {
            gameObject.transform.parent = platformCollision.gameObject.transform;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        // when the player stops colliding with the platform, disconect the two
        gameObject.transform.parent = null;
    }
}
