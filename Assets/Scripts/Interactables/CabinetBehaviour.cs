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
          hammer.SetActive(false); 
          hammer = GameObject.Find("Screwdriver"); 
          cabinetFront = GameObject.Find("CabinetFrontPanel");
       
      }

      public void OpenCabinet()
      {
          if (cabinetFront != null && inventorySystem.HasItem(neededItemName))
          {
              hammer.SetActive(true);
              Destroy(cabinetFront);
          }
      }
   }
