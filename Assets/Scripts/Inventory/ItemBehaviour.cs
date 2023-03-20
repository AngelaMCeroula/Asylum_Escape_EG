using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Inventory
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class ItemBehaviour : MonoBehaviour
    {
        public string itemName;
        private InventorySystem inventorySystem;

        private void Awake()
        {
            inventorySystem = GameObject.Find("Player").GetComponent<InventorySystem>();
        }

        public void AddItem()
        {
            inventorySystem.AddItem(itemName);
            Destroy(gameObject);
        }
    }
}
