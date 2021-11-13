/*
Author: Emanuel Martinez
Class: cs 326
Project: team project 1
team: 3
last updated 11/27/2018 20:45

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : MonoBehaviour
{
    //Transform goTransform;
    float laserAngle = 0.0f; // angle for original laset
    float laserAngleB = 0.0f; // angle for reflected laset
    float laserlength = 1.0f;

    float ninetyDegAngle = 0.78f; // angel of split beamd
    float oneThreeFiveAngle = 2.356f; //second angle of split beam

    float laserWidth = 0.1f;

    //laser 
    LineRenderer laser;
    //public LineRenderer SplitLaser;
    LineRenderer SplitLaser;
    public GameObject spliiter;
    public Material red;


    //Ray ray;
    Ray2D ray; // original laser
    Ray2D rayB; // reflected laser
    Ray2D splitRay; // split laser
    RaycastHit2D raycastHit; //laser hit detect
    RaycastHit2D reflectRaycastHit; // reflected laset hit detect
    RaycastHit2D reflectAndSplitRaycasHit; // reflect then split laser hit detect
    RaycastHit2D splitRaycastHit; //split laser hit detect
    RaycastHit2D splitOrginalRaycastHit;
    float maxLength = 1000f; // max length of laser

    //int numPoints;

    GameObject laserHolder; //laser character

    Vector2 laserOrigin;

    bool splitCreated = false; //check if laser is plsit

    // Use this for initialization
    void Start()
    {
        laser = GetComponent<LineRenderer>();
        //goTransform = this.GetComponent<Transform> ();
        laserHolder = GameObject.FindGameObjectWithTag("LaserCharacter");

        //set split laser
        SplitLaser = spliiter.AddComponent<LineRenderer>();
        SplitLaser.GetComponent<LineRenderer>().material = red;
        SplitLaser.GetComponent<LineRenderer>().startWidth = laserWidth;
    }

    // Update is called once per frame
    void Update()
    {

        //set origin
        float remainingLength = maxLength;

        //increase length of laser
        laserlength += 0.1f;

        ////////////// MAIN LASER BEAM ////////////////////
        //start the laser on laser character
        laserOrigin = laserHolder.transform.position;
        //laserOrigin = new Vector2(laserHolder.transform.position.x,laserHolder.transform.position.x) ;
        laser.SetPosition(0, laserOrigin);
        //raycast for hit detection
        ray = new Ray2D(laserOrigin, new Vector2(Mathf.Cos(laserAngle), Mathf.Sin(laserAngle)));
        //nReflections = Mathf.Clamp (nReflections, 1, nReflections);
        raycastHit = Physics2D.Raycast(laserOrigin, new Vector2(Mathf.Cos(laserAngle), Mathf.Sin(laserAngle)));//, maxLength); 
        
       // transform.Rotate(Vector3.back * .01f);
       
        //move laser around 360 in radians
        if (Input.GetKey(KeyCode.O) && Main.S.characterSwitch == 1)
        {
            laserAngle += 0.01f;
            transform.Rotate(new Vector3(0,0, .55f));
           
            //laserlength = 1.0f;
        }
        else if (Input.GetKey(KeyCode.P) && Main.S.characterSwitch == 1)
        {
            laserAngle -= 0.01f;
            transform.Rotate(new Vector3(0, 0, -.55f));
            // = 1.0f;
        }

        ////////////// MAIN LASER BEAM  HIT ////////////////////
        //if laser hits something
        if (raycastHit.collider != null)
        {
            //Debug.Log(raycastHit.collider.tag);
            //if laser hits refelctor character

            if (raycastHit.collider.tag == "Switch")
            {
                //toggleHitSwitch(raycastHit);
                laser.SetPosition(2, raycastHit.point);
                Main.S.laserNotHittingSwitch = false;
                raycastHit.collider.gameObject.GetComponent<SwitchLaser>().switchON = true;
            }
            else
            {
                Main.S.laserNotHittingSwitch = true;
                //raycastHit.collider.gameObject.GetComponent<SwitchLaser>().switchON = false;
            }

            if (raycastHit.collider.tag == "ReflectCharacter") ////////// Main Laser reflection
            {
                destroySplitLaser();

                ///Laser BEAM CONTROLS ///

                //control refected beam 360 in radains
                if (Input.GetKey(KeyCode.O) && Main.S.characterSwitch == 2)
                {
                    laserAngleB += 0.01f;
                    //laserlength = 1.0f;
                }
                else if (Input.GetKey(KeyCode.P) && Main.S.characterSwitch == 2)
                {
                    laserAngleB -= 0.01f;
                    // = 1.0f;
                }


                /////// reflected laser beam /////////////

                //increase laset points to 3. like array points are 0-2 with 0 as origin
                laser.positionCount = 3;
                //set start position of point 2
                laser.SetPosition(1, raycastHit.point);
                //add new ray
                rayB = new Ray2D(raycastHit.point, new Vector2(Mathf.Cos(laserAngleB), Mathf.Sin(laserAngleB)));
                reflectRaycastHit = Physics2D.Raycast(raycastHit.point, new Vector2(Mathf.Cos(laserAngleB), Mathf.Sin(laserAngleB)));
                //set position of laer point 2
                laser.SetPosition(2, rayB.GetPoint(maxLength));


                /////// reflected laser beam /////////////

                //if reflected laser hits something
                if (reflectRaycastHit.collider != null)
                {
                    //if reflected laser hits anything but splitter
                    if (reflectRaycastHit.collider.tag != "Split")
                    {

                        //if relfected beam hits switch
                        if (reflectRaycastHit.collider.tag == "Switch")
                        {
                            //toggleHitSwitch(reflectRaycastHit);
                            laser.SetPosition(2, reflectRaycastHit.point);
                            Main.S.laserNotHittingSwitch = false;
                            reflectRaycastHit.collider.gameObject.GetComponent<SwitchLaser>().switchON = true;
                        }
                        else if (reflectRaycastHit.collider.tag != "Switch")
                        {
                            Main.S.laserNotHittingSwitch = true;
                            //stop laser at hit position
                            laser.SetPosition(2, reflectRaycastHit.point);
                            //reflectRaycastHit.collider.gameObject.GetComponent<SwitchLaser>().switchON = false;
                        }
                    }

                    //if reflected laser hits spliiter
                    else if (reflectRaycastHit.collider.tag == "Split")  ///split reflected laser beam
                    {


                        /////split right laser

                        // increase orginal laser vertice
                        laser.positionCount = 4;
                        //stop 3rd point at collision point
                        laser.SetPosition(2, reflectRaycastHit.point);
                        //create new ray to split
                        rayB = new Ray2D(reflectRaycastHit.point, new Vector2(Mathf.Cos(ninetyDegAngle), Mathf.Sin(ninetyDegAngle)));
                        //add hit detection for new split reflection
                        reflectAndSplitRaycasHit = Physics2D.Raycast(reflectRaycastHit.point, new Vector2(Mathf.Cos(ninetyDegAngle), Mathf.Sin(ninetyDegAngle)));
                        //set reflection
                        laser.SetPosition(3, rayB.GetPoint(maxLength));


                        /////split left laser

                        //create split laser if not created
                        if (!splitCreated)
                        {
                            splitCreated = true;
                            SplitLaser.positionCount = 2;
                        }

                        SplitLaser.SetPosition(0, spliiter.transform.position);
                        splitRay = new Ray2D(spliiter.transform.position, new Vector2(Mathf.Cos(oneThreeFiveAngle), Mathf.Sin(oneThreeFiveAngle)));
                        splitRaycastHit = Physics2D.Raycast(spliiter.transform.position, new Vector2(Mathf.Cos(oneThreeFiveAngle), Mathf.Sin(oneThreeFiveAngle)));
                        SplitLaser.SetPosition(1, splitRay.GetPoint(maxLength));


                        /////split left laser hit /////

                        //if slit laser hits something stop it
                        if (splitRaycastHit.collider != null)
                        {
                            //SplitLaser.SetPosition(1, splitRaycastHit.point);
                            if (splitRaycastHit.collider.tag == "Switch") ////switch ////////////////
                            {
                                //toggleHitSwitch(splitRaycastHit);
                                SplitLaser.SetPosition(1, splitRaycastHit.point);
                                Main.S.laserNotHittingSwitch = false;
                                splitRaycastHit.collider.gameObject.GetComponent<SwitchLaser>().switchON = true;
                            }
                            else
                            {
                                SplitLaser.SetPosition(1, splitRaycastHit.point);
                                Main.S.laserNotHittingSwitch = true;
                                //splitRaycastHit.collider.gameObject.GetComponent<SwitchLaser>().switchON = false;
                            }
                        }



                        /////split right laser hit /////

                        //if other reflected laser hits stop  it
                        if (reflectAndSplitRaycasHit.collider != null)
                        {
                            //laser.SetPosition(3, reflectAndSplitRaycasHit.point);
                            //if other reflected laser hits switch
                            if (reflectAndSplitRaycasHit.collider.tag == "Switch")
                            {
                                //toggleHitSwitch(reflectAndSplitRaycasHit);
                                laser.SetPosition(3, reflectAndSplitRaycasHit.point);
                                Main.S.laserNotHittingSwitch = false;
                                reflectAndSplitRaycasHit.collider.gameObject.GetComponent<SwitchLaser>().switchON = true;
                            }
                            else
                            {
                                laser.SetPosition(3, reflectAndSplitRaycasHit.point);
                                Main.S.laserNotHittingSwitch = true;
                                //reflectAndSplitRaycasHit.collider.gameObject.GetComponent<SwitchLaser>().switchON = false;
                            }
                        }


                    }
                }


            }
            //if anything bt slitter or laser character is hit stop laser at that point
            else if (raycastHit.collider.tag != "LaserCharacter" && raycastHit.collider.tag != "Split")
            {
                laser.positionCount = 2;
                laser.SetPosition(1, raycastHit.point);
                destroySplitLaser();
            }
            //if spliiter is hit split beam
            else if (raycastHit.collider.tag == "Split")
            {
                //orginal laser set to 3 vertices
                laser.positionCount = 3;
                //stop vertice 2 at hit point
                laser.SetPosition(1, raycastHit.point);
                //create new ray at 90deg
                rayB = new Ray2D(raycastHit.point, new Vector2(Mathf.Cos(ninetyDegAngle), Mathf.Sin(ninetyDegAngle)));
                //create new hit point at 90deg
                splitOrginalRaycastHit = Physics2D.Raycast(raycastHit.point, new Vector2(Mathf.Cos(ninetyDegAngle), Mathf.Sin(ninetyDegAngle)));
                //set laset to new ange
                laser.SetPosition(2, rayB.GetPoint(maxLength));

                //create split laser if not created
                if (!splitCreated)
                {
                    //set as split created
                    splitCreated = true;
                    //increase vertices
                    SplitLaser.positionCount = 2;
                }

                //split ray parameters
                SplitLaser.SetPosition(0, spliiter.transform.position);
                splitRay = new Ray2D(spliiter.transform.position, new Vector2(Mathf.Cos(oneThreeFiveAngle), Mathf.Sin(oneThreeFiveAngle)));
                splitRaycastHit = Physics2D.Raycast(spliiter.transform.position, new Vector2(Mathf.Cos(oneThreeFiveAngle), Mathf.Sin(oneThreeFiveAngle)));
                SplitLaser.SetPosition(1, splitRay.GetPoint(maxLength));


                ////SPLIT LASER //////

                //detection and control for split laser
                if (splitRaycastHit.collider != null)
                {
                    //if not reflector stop laset at position
                    if (splitRaycastHit.collider.tag != "ReflectCharacter")
                    {
                        //SplitLaser.SetPosition(1, splitRaycastHit.point);

                        if (splitRaycastHit.collider.tag == "Switch")///////////////////////////////////
                        {
                            //toggleHitSwitch(splitRaycastHit);
                            SplitLaser.SetPosition(1, splitRaycastHit.point);
                            Main.S.laserNotHittingSwitch = false;
                            splitRaycastHit.collider.gameObject.GetComponent<SwitchLaser>().switchON = true;
                        }
                        else
                        {
                            SplitLaser.SetPosition(1, splitRaycastHit.point);
                            Main.S.laserNotHittingSwitch = true;
                            //splitRaycastHit.collider.gameObject.GetComponent<SwitchLaser>().switchON = false;
                        }


                    }
                    //if laser hits refelctor character
                    else if (splitRaycastHit.collider.tag == "ReflectCharacter")
                    {

                        //control refected beam 360 in radains
                        if (Input.GetKey(KeyCode.O) && Main.S.characterSwitch == 2)
                        {
                            laserAngleB += 0.01f;

                        }
                        else if (Input.GetKey(KeyCode.P) && Main.S.characterSwitch == 2)
                        {
                            laserAngleB -= 0.01f;

                        }

                        //increase laset points to 3. like array points are 0-2 with 0 as origin
                        SplitLaser.positionCount = 3;
                        //set start position of point 2
                        SplitLaser.SetPosition(1, splitRaycastHit.point);
                        //add new ray
                        rayB = new Ray2D(splitRaycastHit.point, new Vector2(Mathf.Cos(laserAngleB), Mathf.Sin(laserAngleB)));
                        //hit detection in new angle
                        reflectRaycastHit = Physics2D.Raycast(splitRaycastHit.point, new Vector2(Mathf.Cos(laserAngleB), Mathf.Sin(laserAngleB)));
                        //set position of laer point 2
                        SplitLaser.SetPosition(2, rayB.GetPoint(maxLength));

                        //if not reflector stop laset at position
                        if (reflectRaycastHit.collider != null)
                        {

                            if (reflectRaycastHit.collider.tag == "Switch")///////////////////////////////////
                            {
                                //toggleHitSwitch(reflectRaycastHit);
                                SplitLaser.SetPosition(2, reflectRaycastHit.point);
                                Main.S.laserNotHittingSwitch = false;
                                reflectRaycastHit.collider.gameObject.GetComponent<SwitchLaser>().switchON = true;
                            }
                            else
                            {
                                SplitLaser.SetPosition(2, reflectRaycastHit.point);
                                Main.S.laserNotHittingSwitch = true;
                                //reflectRaycastHit.collider.gameObject.GetComponent<SwitchLaser>().switchON = false;
                            }

                        }
                    }

                }
                //detection and control for orginal from split laser
                if (splitOrginalRaycastHit.collider != null)
                {
                    if (splitOrginalRaycastHit.collider.tag != "ReflectCharacter")
                    {
                        //laser.SetPosition(2, splitOrginalRaycastHit.point);
                        if (splitOrginalRaycastHit.collider.tag == "Switch")///////////////////////////////////
                        {
                            //toggleHitSwitch(splitOrginalRaycastHit);
                            laser.SetPosition(2, splitOrginalRaycastHit.point);
                            Main.S.laserNotHittingSwitch = false;
                            splitOrginalRaycastHit.collider.gameObject.GetComponent<SwitchLaser>().switchON = true;
                        }
                        else
                        {
                            laser.SetPosition(2, splitOrginalRaycastHit.point);
                            Main.S.laserNotHittingSwitch = true;
                            //splitOrginalRaycastHit.collider.gameObject.GetComponent<SwitchLaser>().switchON = false;
                        }
                    }
                    //if laser hits refelctor character
                    else if (splitOrginalRaycastHit.collider.tag == "ReflectCharacter")
                    {

                        //control refected beam 360 in radains
                        if (Input.GetKey(KeyCode.O) && Main.S.characterSwitch == 2)
                        {
                            laserAngleB += 0.01f;

                        }
                        else if (Input.GetKey(KeyCode.P) && Main.S.characterSwitch == 2)
                        {
                            laserAngleB -= 0.01f;

                        }

                        //increase laset points to 3. like array points are 0-2 with 0 as origin
                        laser.positionCount = 4;
                        //set start position of point 2
                        laser.SetPosition(2, splitOrginalRaycastHit.point);
                        //add new ray
                        rayB = new Ray2D(splitOrginalRaycastHit.point, new Vector2(Mathf.Cos(laserAngleB), Mathf.Sin(laserAngleB)));
                        reflectRaycastHit = Physics2D.Raycast(splitOrginalRaycastHit.point, new Vector2(Mathf.Cos(laserAngleB), Mathf.Sin(laserAngleB)));
                        //set position of laer point 2
                        laser.SetPosition(3, rayB.GetPoint(maxLength));

                        if (reflectRaycastHit.collider != null)
                        {

                            if (reflectRaycastHit.collider.tag == "Switch")///////////////////////////////////
                            {
                                //toggleHitSwitch(reflectRaycastHit);
                                laser.SetPosition(3, reflectRaycastHit.point);
                                Main.S.laserNotHittingSwitch = false;
                                reflectRaycastHit.collider.gameObject.GetComponent<SwitchLaser>().switchON = true;
                            }
                            else
                            {
                                laser.SetPosition(3, reflectRaycastHit.point);
                                Main.S.laserNotHittingSwitch = true;
                                //reflectRaycastHit.collider.gameObject.GetComponent<SwitchLaser>().switchON = false;
                            }
                        }
                    }
                }

            }
            //if switch is hit
            else if (raycastHit.collider.tag == "Switch")
            {
                //toggleHitSwitch(raycastHit);
                laser.SetPosition(1, raycastHit.point);
            }
            //if nothing is hit keep increasing beam reduce laser points back to 2
            else
            {
                laser.positionCount = 2;
                laser.SetPosition(1, ray.GetPoint(maxLength));
                destroySplitLaser();
            }

        }
        //not hit 
        else
        {
            laser.positionCount = 2;
            laser.SetPosition(1, ray.GetPoint(maxLength));
            destroySplitLaser();
        }

    }

    //destroys rendered split laser
    void destroySplitLaser()
    {
        SplitLaser.positionCount = 1;
        splitCreated = false;
    }

    /*void toggleHitSwitch(RaycastHit2D hit)
    {
        hit.collider.gameObject.GetComponent<SwitchLaser>().toggleSwitch();

    }*/

}
