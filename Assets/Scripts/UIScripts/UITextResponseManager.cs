using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class UITextResponseManager : MonoBehaviour
{
    [SerializeField] private GameObject textBox;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private int waitTimeinSeconds;

    private void Start()
    {
        textBox.SetActive(false);
        _text.text = null;
    }

    public void TextToUI(string text)
    {
        _text.text = text;
        textBox.SetActive(true);
        Wait();

    }

    async void Wait()
    {
        await Task.Delay(waitTimeinSeconds * 1000);
        textBox.SetActive(false);
        _text.text = null;

    }
}
