using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventorySystem : MonoBehaviour
    {
        private List<string> _inventoryList = new List<string>();
        private bool _hasItem;

        public void AddItem(string itemName)
        {
            _inventoryList.Add(itemName);
            
        }

        public void RemoveItem(string itemName)
        {
            _inventoryList.Remove(itemName);
        }

        

        public bool HasItem(string itemName)
        {

            if (_inventoryList.Contains(itemName))
            {
                _hasItem = true;
            }

            if (!_inventoryList.Contains(itemName))
            {
                _hasItem = false;
            }
            
            return _hasItem;
        }
    }
}