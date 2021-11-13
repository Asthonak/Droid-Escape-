using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public float jumpVelocity = 10;
    public LayerMask groundLayer;
    //currently laserChar is set to swtichNumber = 1, reflectChar = 2, teleChar = 3
    public int swtichNumber;

    private Rigidbody2D rb;
    private Transform trans;

    public Sprite active;
    public Sprite inactive;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        trans = this.GetComponent<Transform>();
        /*tagGround = GameObject.Find(this.name+"/tag_ground").transform;*/
        //trans = this.transform;
    }

    void FixedUpdate()
    {
        if(swtichNumber == Main.S.characterSwitch)
        {
            GetComponent<SpriteRenderer>().sprite = active;
            Vector3 updatePos = transform.position;
            updatePos.z = -1.0f;
            transform.position = updatePos;
            
            //probably should change this to A/D for right and left
            Move(Input.GetAxisRaw("Horizontal"));
            if (Input.GetKey(KeyCode.W))
            {
                Jump();
            }
        }
        else
        {
            Vector3 updatePos = transform.position;
            updatePos.z = 0.0f;
            transform.position = updatePos;
            GetComponent<SpriteRenderer>().sprite = inactive;
        }
    }
    //function that reads horzontal movement and moves character accordingly
    void Move(float horizonalInput)
    {
        Vector2 moveVel = rb.velocity;
        moveVel.x = horizonalInput * speed;
        rb.velocity = moveVel;

        //orients sprite in movement direction
        if (moveVel.x > 0)
        GetComponent<SpriteRenderer>().flipX = false;
        if (moveVel.x < 0)
        GetComponent<SpriteRenderer>().flipX = true;

    }
    //function that makes the character jump
    public void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity += jumpVelocity * Vector2.up;
        }
    }

    bool IsGrounded()
    {
        Vector2 position = trans.position;
        Vector2 direction = Vector2.down;
        float distance = 1.01f;
        Debug.DrawRay(position, direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9 /*|| collision.gameObject.tag == "ReflectCharacter" || collision.gameObject.tag == "TeleportCharacter"*/)
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}
