using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

    public int _selectedButton = 0;
    public float _timeBetweenButtonPress = 0.1f;
    public float _timeDelay;

    public float _mainMenuButtonWidth = 100f;
    public float _mainMenuButtonHeight = 25f;
    public float _mainMenuGUIOffset = 10f;

    public float _mainMenuVerticalInputTimer;
    public float _mainMenuVerticalInputDelay = .5f;

    private string[] _mainMenuButtons = new string[]
    {
        "_Level1",
        "_Level2",
        "_Level3"
    };

    private bool _startingLevel1;
    private bool _startingLevel2;
    private bool _startingLevel3;

    // Use this for initialization
    void Start () {
        _startingLevel1 = false;
        _startingLevel2 = false;
        _startingLevel3 = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (_mainMenuVerticalInputTimer > 0)
        {
            _mainMenuVerticalInputTimer -= 1f * Time.deltaTime;
        }

        // dont go above button zero
        if (Input.GetAxis("Vertical") > 0f && _selectedButton == 0)
        {
            return;
        }

        // going up from button one
        if (Input.GetAxis("Vertical") > 0f && _selectedButton == 1)
        {
            if (_mainMenuVerticalInputTimer > 0)
            {
                return;
            }
            _mainMenuVerticalInputTimer = _mainMenuVerticalInputDelay;
            _selectedButton = 0;
        }

        // going up from button two
        if (Input.GetAxis("Vertical") > 0f && _selectedButton == 2)
        {
            if (_mainMenuVerticalInputTimer > 0)
            {
                return;
            }
            _mainMenuVerticalInputTimer = _mainMenuVerticalInputDelay;
            _selectedButton = 1;
        }

        // going down from button zero
        if (Input.GetAxis("Vertical") < 0f && _selectedButton == 0)
        {
            if (_mainMenuVerticalInputTimer > 0)
            {
                return;
            }
            _mainMenuVerticalInputTimer = _mainMenuVerticalInputDelay;
            _selectedButton = 1;
        }

        // going down from button one
        if (Input.GetAxis("Vertical") < 0f && _selectedButton == 1)
        {
            if (_mainMenuVerticalInputTimer > 0)
            {
                return;
            }
            _mainMenuVerticalInputTimer = _mainMenuVerticalInputDelay;
            _selectedButton = 2;
        }

        // dont go below button two
        if (Input.GetAxis("Vertical") < 0f && _selectedButton == 2)
        {
            return;
        }

        if(_startingLevel1 == true)
        {
            SceneManager.LoadScene("Level1");
        }
        else if (_startingLevel2 == true)
        {
            SceneManager.LoadScene("level2");
        }
        else if (_startingLevel3 == true)
        {
            SceneManager.LoadScene("level3");
        }
    }

    private void MainMenuButtonPress()
    {
        Debug.Log("MainMenuBuutonPressed");

        GUI.FocusControl(_mainMenuButtons[_selectedButton]);

        switch (_selectedButton)
        {
            case 0:
                _startingLevel1 = true;
                break;
            case 1:
                _startingLevel2 = true;
                break;
            case 2:
                _startingLevel3 = true;
                break;
        }
    }

    private void OnGUI()
    {
        if (Time.deltaTime >= _timeDelay && (Input.GetButton("Fire1")))
        {
            StartCoroutine("MainMenuButtonPress");

            //reset timer
            _timeDelay = Time.deltaTime + _timeBetweenButtonPress;
        }

        
        GUI.BeginGroup(new Rect(Screen.width / 2 - _mainMenuButtonWidth / 2,
                                  Screen.height / 1.5f,
                                  _mainMenuButtonWidth,
                                  _mainMenuButtonHeight * 3 + _mainMenuGUIOffset * 2));

        GUI.SetNextControlName("_Level1");
        if (GUI.Button(new Rect(0, 0,
            _mainMenuButtonWidth, _mainMenuButtonHeight),
            "Level 1"))
        {
            _selectedButton = 0;
            MainMenuButtonPress();
        }

        GUI.SetNextControlName("_Level2");
        if (GUI.Button(new Rect(0, _mainMenuButtonHeight + _mainMenuGUIOffset,
            _mainMenuButtonWidth, _mainMenuButtonHeight),
            "Level 2"))
        {
            _selectedButton = 1;
            MainMenuButtonPress();
        }

        GUI.SetNextControlName("_Level3");
        if (GUI.Button(new Rect(0, (_mainMenuButtonHeight + _mainMenuGUIOffset)*2,
            _mainMenuButtonWidth, _mainMenuButtonHeight),
            "Level 3"))
        {
            _selectedButton = 2;
            MainMenuButtonPress();
        }

        GUI.EndGroup();


        GUI.FocusControl(_mainMenuButtons[_selectedButton]);

    }
}
