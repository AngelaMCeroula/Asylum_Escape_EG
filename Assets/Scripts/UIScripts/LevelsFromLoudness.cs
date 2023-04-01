using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsFromLoudness : MonoBehaviour
{
    private LoudnessDetectionMicrophone _loudnessDetectionM;
    
    [SerializeField]private float threshold1 = 25, threshold2 = 20;
    [SerializeField] private GameObject bar1, bar2, bar3;
    

    private void Start()
    {
        _loudnessDetectionM = GetComponent<LoudnessDetectionMicrophone>();
        bar1.SetActive(false);
        bar2.SetActive(false);
        bar3.SetActive(false);

    }
    private void Update()
    {
        float loudness = _loudnessDetectionM.GetLoudnessFromMicrophone();
        int db = (int)Mathf.Round(loudness*100);
        
        if ( db < 1)
        {
            bar1.SetActive(false);
            bar2.SetActive(false);
            bar3.SetActive(false);
        }
        
        if (db >= 1 && db < threshold1)
        {
            bar1.SetActive(true);
            bar2.SetActive(false);
            bar3.SetActive(false);
        }

        if (db >= threshold1 && db < threshold2)
        {
            bar1.SetActive(true);
            bar2.SetActive(true);
            bar3.SetActive(false);
        }

        if (db >= threshold2)
        { 
            bar1.SetActive(true);
            bar2.SetActive(true);
            bar3.SetActive(true);
        }
        //Debug.Log("db");
    }

    /*
    void ColourSet()
    {
        if (db >= 0 && db < 25)
        {
            _color = Color.blue;
            currentColor = "blue";
        }

        if (db >= 25 && db < 50)
        {
            _color = Color.yellow;
            currentColor = "yellow";
        }

        if (db >= 50)
        { 
            _color = Color.red;
            currentColor = "red";
        }
        
    }
    */
}
