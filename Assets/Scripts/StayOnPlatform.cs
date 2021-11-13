using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnPlatform : MonoBehaviour
{
    private bool onPlatform;

    private void Start()
    {
        onPlatform = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            this.transform.SetParent(collision.collider.transform);
            onPlatform = true;
        }

        if (collision.gameObject.tag != "Platform" && collision.gameObject.tag != "Ground" && collision.gameObject.tag != "Wall" && collision.gameObject.tag != "Teleporter" && onPlatform == true)
        {
            collision.collider.transform.SetParent(this.transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            this.transform.SetParent(null);
            onPlatform = false;
            collision.collider.transform.SetParent(null);
        }

        if(onPlatform)
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
