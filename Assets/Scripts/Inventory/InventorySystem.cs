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
        public List<GameObject> inventorySlots;

        public void AddItem(string itemName)
        {
            _inventoryList.Add(itemName);
            
            foreach (GameObject slot in inventorySlots)
            {
                if (slot.activeSelf == false)
                {
                    slot.SetActive(true);
                    slot.GetComponent<TMPro.TextMeshProUGUI>().text = itemName;
                    break;
                }
            }
            
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