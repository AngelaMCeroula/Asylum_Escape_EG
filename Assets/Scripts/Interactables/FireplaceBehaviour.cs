using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class FireplaceBehaviour : MonoBehaviour
{
    
    [SerializeField] private GameObject screwdriver, SD_placeholder;
    private UITextResponseManager _uiTextResponseManager;

    private void Start()
    {
        _uiTextResponseManager = GameObject.Find("UITextResponseManager").GetComponent<UITextResponseManager>();
        screwdriver = GameObject.Find("Screwdriver");
        screwdriver.SetActive(false);
    }

    public void SearchFireplace()
    {
        if (screwdriver != null)
        {
            screwdriver.SetActive(true);
            SD_placeholder.SetActive(false);
            Debug.Log("There is a Screwdriver here");
            _uiTextResponseManager.TextToUI("There is a Screwdriver here");
        }
        else
        {
            Debug.Log("There is nothing here");
            _uiTextResponseManager.TextToUI("There is nothing here");
        }
        
    }
}
