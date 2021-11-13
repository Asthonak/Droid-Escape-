using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [Header("Set in Inspector")]
    //currently laserChar is set to swtichNumber = 1, reflectChar = 2, teleChar = 3
    public int swtichNumber;
    public GameObject item;

    //-1 = player facing left, 1 = player facing right
    private int playerDirection;

    void Start()
    {
        playerDirection = 1;
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

        //when teleChar presses e a teleporter will spawn in front of them, if a previous teleporter was already spawned it will be deleted
        if (swtichNumber == Main.S.characterSwitch == true && Input.GetKeyDown(KeyCode.E))
        {
            Vector2 trans = this.transform.position;
            if (playerDirection == -1)
            {
                trans.x -= 1.5f;
            }
            else if (playerDirection == 1)
            {
                trans.x += 1.5f;
            }

            item.transform.position = trans;

            //if(item != null)
            //{
           

            GameObject clone = Instantiate<GameObject>(item);
            Destroy(item);
            item = clone;
            //}
        }
    }
}
