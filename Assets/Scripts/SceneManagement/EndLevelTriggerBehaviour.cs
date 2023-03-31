using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTriggerBehaviour : MonoBehaviour
{
    private LevelManager _levelManager;

    private void Start()
    {
        _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _levelManager.End2_Escape();
        }
    }
}
