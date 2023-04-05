using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject howToPlayOverlay;
    //-------------------------SceneManagement
    public void PlayGame()
    {
        SceneManager.LoadScene("GameLevel");
    }

    public void Help()
    {
        howToPlayOverlay.SetActive(true);
    }

    public void CloseHelp()
    {
        howToPlayOverlay.SetActive(false);
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
