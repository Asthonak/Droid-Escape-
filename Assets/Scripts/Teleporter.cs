using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    static public Teleporter S;

    [Header("Set in Inspector")]
    //currently laserChar is set to swtichNumber = 1, reflectChar = 2, teleChar = 3
    public int swtichNumber;

    //-1 = player facing left, 1 = player facing right
    private int playerDirection;
    //used to find where teleChar is
    private GameObject TeleportCharacter;

    void Start()
    {
        playerDirection = 1;
        TeleportCharacter = GameObject.Find("TeleportCharacter");
    }

    void Update()
    {
        //code that finds which direction the player is facing
        if (Input.GetKeyDown(KeyCode.A) && swtichNumber == Main.S.characterSwitch)
        {
            playerDirection = -1;
        }
        if (Input.GetKeyDown(KeyCode.D) && swtichNumber == Main.S.characterSwitch)
        {
            playerDirection = 1;
        }
    }

    //when an object collides with the collider it is teleported to the left or right of teleChar depending on the direction they are facing
    /*NOTE: currently "picked up" objects must first be dropped for the teleport to work on them*/
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag == "Object" || other.tag == "Split")
        {
            //print("here");
            Vector2 trans = TeleportCharacter.transform.position;
            if (playerDirection == -1)
            {
                trans.x -= 1.5f;
            }
            else if (playerDirection == 1)
            {
                trans.x += 1.5f;
            }
            other.transform.position = trans;
        }
    }
}
