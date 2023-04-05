using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;

public class UIEndSceneCommands : MonoBehaviour
{
    private GameManager _gameManager;
    private KeywordRecognizer keywordRecognizer;
    private UITextResponseManager _uiTextResponseManager;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    
    // Start is called before the first frame update
    void Start()
    {
        
        AddKeywords();
        _uiTextResponseManager = GameObject.Find("UITextResponseManager").GetComponent<UITextResponseManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
        Debug.Log("keyword recognizer on is " + keywordRecognizer.IsRunning.ToString());
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
        _uiTextResponseManager.recognizedKeywordIndicate();
    }

    void  AddKeywords()
    {
        actions.Add("play again", PlayGame);
        actions.Add("quit", QuitGame);
        actions.Add("main menu", MainMenu);
    }

    void PlayGame()
    {
        _gameManager.PlayGame();
    }
    void MainMenu()
    {
        _gameManager.MainMenu();
    }


    void QuitGame()
    {
        _gameManager.QuitGame();
    }
}
