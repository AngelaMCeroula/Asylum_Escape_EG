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
        public AudioSource _audioSource;
        

        private void Awake()
        {
            inventorySystem = GameObject.Find("Player").GetComponent<InventorySystem>();
        }

        public void AddItem()
        {
            _audioSource.PlayOneShot(_audioSource.clip, 1);
            inventorySystem.AddItem(itemName);
            Destroy(gameObject);
        }
    }
}
