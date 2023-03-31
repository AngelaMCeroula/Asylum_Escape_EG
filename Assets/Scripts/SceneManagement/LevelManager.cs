using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //-------------------------------UI CANVAS GameObject
    public GameObject end1, end2, pauseMenu, journalCanvas, helpCanvas, openJournalButton;

    //-----------------------Is Paused bool
    private bool _gameIsPaused;
    

    void Start()
    {
        //pauseMenu = GameObject.Find("PauseCanvas");
        _gameIsPaused = false;
        
        end1.SetActive(false);
        end2.SetActive(false);
        pauseMenu.SetActive(false);
        helpCanvas.SetActive(false);
        
    }

   void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _gameIsPaused == false)
        {
            PauseGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && _gameIsPaused == true)
        {
            ResumeGame();
        }

        if (Input.GetKeyDown(KeyCode.J) && _gameIsPaused == false)
        {
            OpenJournal();
        }
        if (Input.GetKeyDown(KeyCode.J) && _gameIsPaused == true)
        {
            CloseJournal();
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        _gameIsPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
        openJournalButton.SetActive(false);

    }

    public void ResumeGame()
    {
        _gameIsPaused = false;
        pauseMenu.SetActive(false);
        helpCanvas.SetActive(false);
        openJournalButton.SetActive(true);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public void End1_Sleep()
    {
        end1.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
        openJournalButton.SetActive(false);
    }
    public void End2_Escape()
    {
        end2.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
        openJournalButton.SetActive(false);
        
    }

    public void OpenJournal()
    {
        openJournalButton.SetActive(false);
        // animate journal coming into main camera
        //journalcanvas transform position new
    }

    public void CloseJournal()
    {
        // animate journal returning to place
    }

    public void HelpScreen()
    {
        helpCanvas.SetActive(true);
        _gameIsPaused = true;
        Time.timeScale = 0;
        AudioListener.pause = true;
    }
    


}
