using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Vector3 platformVelocity;

    public bool isHorizontal;
    public bool isVertical;

    public float from;
    public float to;
    //public float platformVelocity;

    private Rigidbody2D rb;
    private Transform trans;
    private Transform origin;

    private bool movingRight;
    private bool movingLeft;

    private bool movingUp;
    private bool movingDown;

    void Start ()
    {
        rb = this.GetComponent<Rigidbody2D>();
        trans = this.GetComponent<Transform>();
        movingRight = true;
        movingLeft = false;
        movingUp = true;
        movingDown = false;
    }
	
	void FixedUpdate()
    {
        if (isHorizontal)
        {
            if (trans.position.x < to && movingRight == true)
            {
                transform.position += (platformVelocity * Time.deltaTime);
            }
            else
            {
                movingRight = false;
                movingLeft = true;
            }
            if (trans.position.x > from && movingLeft == true)
            {
                transform.position -= (platformVelocity * Time.deltaTime);
            }
            else
            {
                movingRight = true;
                movingLeft = false;
            }
        }
        else if (isVertical)
        {
            if (trans.position.y < to && movingUp == true)
            {
                transform.position += (platformVelocity * Time.deltaTime);
            }
            else
            {
                movingUp = false;
                movingDown = true;
            }
            if (trans.position.y > from && movingDown == true)
            {
                transform.position -= (platformVelocity * Time.deltaTime);
            }
            else
            {
                movingUp = true;
                movingDown = false;
            }
        }
    }
}
