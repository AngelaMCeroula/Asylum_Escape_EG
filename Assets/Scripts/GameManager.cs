using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private int waitTimeInSeconds = 1;
    public GameObject howToPlayOverlay;
    
    
    //-------------------------SceneManagement
    public void PlayGame()
    {
        Wait();
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
    
    async void Wait()
    {
        await Task.Delay(waitTimeInSeconds * 1000);
        
    }
}
