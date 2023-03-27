using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class FireplaceBehaviour : MonoBehaviour
{
    
    [SerializeField] private GameObject screwdriver;

    private void Start()
    {
        screwdriver = GameObject.Find("Screwdriver");
        screwdriver.SetActive(false);
    }

    public void SearchFireplace()
    {
        if (screwdriver != null)
        {
            screwdriver.SetActive(true);
            Debug.Log("There is a Screwdriver here");
        }
        else
        {
            Debug.Log("There is nothing here");
        }
        
    }
}
