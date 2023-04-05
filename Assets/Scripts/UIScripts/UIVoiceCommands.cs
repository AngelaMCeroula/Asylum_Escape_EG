using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;


public class UIVoiceCommands : MonoBehaviour
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
        actions.Add("start", PlayGame);
        actions.Add("quit", QuitGame);
        actions.Add("help", Help);
        actions.Add("how to play", Help);
    }

    void PlayGame()
    {
        _gameManager.PlayGame();
        
    }

    void Help()
    {
        _gameManager.Help();
    }

    void QuitGame()
    {
        _gameManager.QuitGame();
    }
}
