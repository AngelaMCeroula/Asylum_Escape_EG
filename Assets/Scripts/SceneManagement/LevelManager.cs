using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public JournalBehaviour _journalBehaviour;
    
    //-------------------------------UI CANVAS GameObject
    public GameObject end1, end2, pauseMenu, helpCanvas, openJournalButton;
    
    //-----------------------Bools
    [HideInInspector]public bool _gameIsPaused;
   
    

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
            Debug.Log("PAUSE");
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _gameIsPaused == true)
        { 
            Debug.Log("RESUME");
            ResumeGame();
            
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key was pressed");
        }

        if (Input.GetKeyDown(KeyCode.J) && _journalBehaviour._journalOpen == false)
        {
            _journalBehaviour.OpenJournal();
        }
        if (Input.GetKeyDown(KeyCode.J) && _journalBehaviour._journalOpen == true)
        {
            _journalBehaviour.CloseJournal();
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
    {  /*
        end1.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
        openJournalButton.SetActive(false);
        */
        SceneManager.LoadScene("EndScene 1");
    }
    public void End2_Escape()
    {
        /*
        end2.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
        openJournalButton.SetActive(false);
        */
        SceneManager.LoadScene("EndScene 2");
        
    }


    public void HelpScreen()
    {
        helpCanvas.SetActive(true);
        _gameIsPaused = true;
        Time.timeScale = 0;
        AudioListener.pause = true;
    }
    
}
