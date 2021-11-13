using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float climbVelocity;
    public PickUpObjects reflector;

    void OnTriggerStay2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag == "LaserCharacter" && Input.GetKey(KeyCode.W) && Main.S.characterSwitch == 1)
        {
            Rigidbody2D otherRB;
            otherRB = other.GetComponent<Rigidbody2D>();
            otherRB.velocity = Vector2.up * climbVelocity;
        }
        else if (other.tag == "ReflectCharacter" && Input.GetKey(KeyCode.W) && Main.S.characterSwitch == 2 && reflector.pickedUpObject != true)
        {
            Rigidbody2D otherRB;
            otherRB = other.GetComponent<Rigidbody2D>();
            otherRB.velocity = Vector2.up * climbVelocity;
        }
        else if (other.tag == "TeleportCharacter" && Input.GetKey(KeyCode.W) && Main.S.characterSwitch == 3)
        {
            Rigidbody2D otherRB;
            otherRB = other.GetComponent<Rigidbody2D>();
            otherRB.velocity = Vector2.up * climbVelocity;
        }
    }
}
