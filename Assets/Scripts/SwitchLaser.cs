using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLaser : MonoBehaviour
{
    [Header("Set in Inspector")]
    //public GameObject door;
    public int switchNumber;

    [Header("Set Dynamically")]
    public bool switchON = false;

    bool hit = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Main.S.laserNotHittingSwitch == true)
        {
            switchON = false;
        }

        if (switchON)
        {
            Main.S.switchIsOn[switchNumber] = true;
            //print(switchNumber);
        }
        else
        {
            Main.S.switchIsOn[switchNumber] = false;
        }
        //openDoor();
    }

    //if switch is on opens door
    /*void openDoor()
    {
        if (switchON)
        {
            door.gameObject.SetActive(false);
        }
    }*/

    //toggle switch on 
    //controles from laser script when hit by raycast hit
    /*public void toggleSwitch()
    {
        switchON = true;
    }*/
}
