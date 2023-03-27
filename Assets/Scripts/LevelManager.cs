using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //-------------------------------UI CANVAS GameObject
    public GameObject end1, end2, pauseMenu;

    //-----------------------Is Paused bool
    private bool _gameIsPaused;
    

    void Start()
    {
        //pauseMenu = GameObject.Find("PauseCanvas");
        _gameIsPaused = false;
        
        end1.SetActive(false);
        end2.SetActive(false);
        pauseMenu.SetActive(false);
    }

   void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _gameIsPaused == false)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _gameIsPaused == true)
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        _gameIsPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;

    }

    public void ResumeGame()
    {
        _gameIsPaused= false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public void End1_Sleep()
    {
        end1.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
    }
    public void End2_Escape()
    {
        end1.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
        
    }
    
    
}
