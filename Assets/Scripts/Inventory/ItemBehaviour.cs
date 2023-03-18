using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Inventory
{
    public class ItemBehaviour : MonoBehaviour
    {
        public string itemName;
        private InventorySystem inventorySystem;
        public bool canBePicked;
        
        private void Awake()
        {
            inventorySystem = GameObject.Find("Player").GetComponent<InventorySystem>();
        }
        
        void AddItem()
        {
            inventorySystem.AddItem(itemName);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.GameObject().name == "Player")
            {
                canBePicked = true;
                
            }
            else
            {
                canBePicked = false;
            }
        }

        /*
        void OnTriggerEnter(Collider other)
        {
            if (other.GameObject().name == "Player")
            {
                
                AddItem();
                Destroy(gameObject);
                
            }
        }
        */

        
    }
}
