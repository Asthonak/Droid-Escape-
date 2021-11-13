using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    [Header("Set in Inspector")]
    //currently laserChar is set to swtichNumber = 1, reflectChar = 2, teleChar = 3
    public int swtichNumber;

    [Header("Set Dynamically")]
    //item that waill be picked up
    public GameObject item;
    public bool pickedUpObject;

    //-1 = player facing left, 1 = player facing right
    private int playerDirection;

    void Start()
    {
        playerDirection = 1;
        pickedUpObject = false;
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

        //code that moves the object when it is picked up 
        if (pickedUpObject == true)
        {
            Vector2 trans = this.transform.position;
            //offset object depending on which direction the character is facing
            if (playerDirection == -1)
            {
                trans.x -= 1.5f;
            }
            else if (playerDirection == 1)
            {
                trans.x += 1.5f;
            }
            item.transform.position = trans;
            //pickedUpObject.transform = this.transform;
        }
        //code that drops the object
        if (pickedUpObject == true && Input.GetKeyDown(KeyCode.E) && swtichNumber == Main.S.characterSwitch)
        {
            pickedUpObject = false;
            item = null;
        }
    }

    //if character collides with the object and e is pressed, they pick up the object
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        
        if ((other.tag == "Object" && Input.GetKey(KeyCode.E) && pickedUpObject == false && swtichNumber == Main.S.characterSwitch) ||
            ((other.tag == "Split" && Input.GetKey(KeyCode.E) && pickedUpObject == false && swtichNumber == Main.S.characterSwitch)))
        {
            item = other;
            pickedUpObject = true;
            Vector2 trans = this.transform.position;
            trans.x += 1.5f;
            item.transform.position = trans;
        }
    }
}
