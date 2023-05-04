using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        Pause();

    }
    // if escape is pressed, pause the game
    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
        } 
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = 1;
        }
    }

    }
