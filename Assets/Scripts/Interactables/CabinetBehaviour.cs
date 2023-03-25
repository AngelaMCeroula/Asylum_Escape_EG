using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

   [RequireComponent(typeof(CapsuleCollider))]
   public class CabinetBehaviour : MonoBehaviour
   {
       
      [SerializeField] private GameObject cabinetFront;
      [SerializeField] private GameObject hammer;
      [SerializeField] private string neededItemName;
      private InventorySystem inventorySystem;

   
      private void Start()
      {
          inventorySystem = GameObject.Find("Player").GetComponent<InventorySystem>();
          hammer = GameObject.Find("Hammer");
          cabinetFront = GameObject.Find("CabinetFrontPanel");
          
          hammer.SetActive(false);
       
      }
      
      public void SearchCabinet()
      {
          if (cabinetFront != null)
          {
              Debug.Log("It's locked");
          }

          else
          {
              Debug.Log("It's open");
          }
       
      }

      public void OpenCabinet()
      {
          if (cabinetFront != null && inventorySystem.HasItem(neededItemName) == true)
          {
              hammer.SetActive(true);
              Destroy(cabinetFront);
          }
          else if (inventorySystem.HasItem(neededItemName) != true)
          {
              Debug.Log("I don't have a bobby pin");
          }
          
          else
          {
              Debug.Log("I can't do that");
          }
      }
   }
