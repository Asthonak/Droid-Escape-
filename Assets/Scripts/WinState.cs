using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : MonoBehaviour
{
    private bool[] inZone;

	void Start ()
    {
        inZone = new bool[3];
        for(int i=0; i<3; i++)
        {
            inZone[i] = false;
        }
    }
	
	void Update ()
    {
		if(inZone[0] == true && inZone[1] == true  && inZone[2] == true)
        {
            Main.S.PlayerWins();
        }
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "LaserCharacter")
        {
            inZone[0] = true;
        }
        else if(collision.gameObject.tag == "ReflectCharacter")
        {
            inZone[1] = true;
        }
        else if(collision.gameObject.tag == "TeleportCharacter")
        {
            inZone[2] = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LaserCharacter")
        {
            inZone[0] = false;
        }
        else if (collision.gameObject.tag == "ReflectCharacter")
        {
            inZone[1] = false;
        }
        else if (collision.gameObject.tag == "TeleportCharacter")
        {
            inZone[2] = false;
        }
    }
}
