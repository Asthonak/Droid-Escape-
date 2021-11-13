using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    //singleton for main
    static public Main S;

    public GameObject canvas;

    [Header("Set in Inspector")]
    public int levelNumber;
    //number of characters, currently set to 3: laserChar, reflectChar, and teleChar
    public int numberOfCharacters;
    public int numberOfSwitches;
    public GameObject[] Switches;
    public int numberOfDoors;
    public GameObject[] Doors;

    [Header("Set Dynamically")]
    //currently laserChar is set to swtichNumber = 1, reflectChar = 2, teleChar = 3
    public int characterSwitch;
    public bool laserNotHittingSwitch;
    public bool[] switchIsOn;

    void Start ()
    {
        S = this;
        //switch set to 1 by default
        characterSwitch = 1;
        switchIsOn = new bool[numberOfSwitches];
        for (int i = 0; i < numberOfDoors; i++)
        {
            switchIsOn[i] = false;
        }
    }
	
	void Update ()
    {
        //when space is pressed the switch goes to the next character allowing the player to control them
		if(Input.GetKeyDown(KeyCode.Space))
        {
            if(characterSwitch != numberOfCharacters && characterSwitch != 0)
            {
                characterSwitch++;
            }
            else
            {
                characterSwitch = 1;
            }
            //print(characterSwitch);
        }

        /*This code will need to be changed each level depending on what swiches open which doors for each level*/

        if(levelNumber == 1)
        {
            if (switchIsOn[0] == true)
            {
                Doors[0].SetActive(false);
            }

            if (switchIsOn[1] == true && switchIsOn[2] == true)
            {
                Doors[1].SetActive(false);
            }

            if (switchIsOn[3] == true && switchIsOn[4] == true)
            {
                Doors[2].SetActive(false);
            }
        }
        else if (levelNumber == 2)
        {
            if (switchIsOn[0] == true)
            {
                Doors[0].SetActive(false);
            }

            if (switchIsOn[1] == true && switchIsOn[2] == true)
            {
                Doors[1].SetActive(false);
            }

            if (switchIsOn[3] == true && switchIsOn[4] == true)
            {
                Doors[2].SetActive(false);
            }
        }
        else if (levelNumber == 3)
        {
            if (switchIsOn[0] == true)
            {
                Doors[0].SetActive(false);
            }

            if (switchIsOn[1] == true && switchIsOn[2] == true)
            {
                Doors[1].SetActive(false);
            }

            if (switchIsOn[3] == true && switchIsOn[4] == true)
            {
                Doors[2].SetActive(false);
            }
        }
    }

    public void PlayerWins()
    {
        /*UI here for level cleared and option to restart/return to main menu*/
        Instantiate(canvas, new Vector2(0, 0), Quaternion.identity);
        //print("Win State Achieved");
    }
}
