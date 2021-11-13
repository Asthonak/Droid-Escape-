using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIlevels : MonoBehaviour {

    public bool _controlsVisible = false;
    public Texture2D controls;
    public GameObject canvas;
    public GameObject canvasobject;

    public float _InputTimer;
    public float _InputDelay = .5f;
    Scene level;

    // Use this for initialization
    void Start () {
        level = SceneManager.GetActiveScene();

    }

    // Update is called once per frame
    void Update()
    {
        if (_InputTimer > 0)
        {
            _InputTimer -= 1f * Time.deltaTime;
        }

        if (Input.GetButton("Controls") && _controlsVisible == false)
        {
            if (_InputTimer > 0)
            {
                return;
            }
            _controlsVisible = true;
            canvasobject = Instantiate(canvas, new Vector2(0, 0), Quaternion.identity);
            _InputTimer = _InputDelay;
        }
        if (Input.GetButton("Controls") && _controlsVisible == true)
        {
            if (_InputTimer > 0)
            {
                return;
            }
            _controlsVisible = false;
            DestroyObject(canvasobject);
            _InputTimer = _InputDelay;
        }
        if (Input.GetButton("Restart"))
        {
            SceneManager.LoadScene(level.name);
        }
        if (Input.GetButton("GoBack"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    /*
    private void OnGUI()
    {
        if(_controlsVisible == true)
        {
            GUI.DrawTexture(new Rect(0-(Screen.width/2), 0 - (Screen.height/2), Screen.width, Screen.height), controls);
        }
    }*/
}
