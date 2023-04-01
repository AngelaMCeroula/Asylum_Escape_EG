using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class UITextResponseManager : MonoBehaviour
{
    [SerializeField] private GameObject textBox, recognizedKeywordImg;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private int waitTimeInSeconds;

    private void Start()
    {
        textBox.SetActive(false);
        _text.text = null;
        recognizedKeywordImg.SetActive(false);
    }

    public void TextToUI(string text)
    {
        _text.text = text;
        textBox.SetActive(true);
        Wait();

    }

    async void Wait()
    {
        await Task.Delay(waitTimeInSeconds * 1000);
        textBox.SetActive(false);
        _text.text = null;

    }

    public void recognizedKeywordIndicate()
    {
        recognizedKeywordImg.SetActive(true);
        Wait2();
    }
    
    async void Wait2()
    {
        await Task.Delay(1000);
        recognizedKeywordImg.SetActive(false);

    }
}
