using System;
using Inventory;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class TileBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject interactableTile;
    [SerializeField] private GameObject brokenTile;
    [SerializeField] private GameObject key;
    [SerializeField] private string neededItemName;
    private InventorySystem inventorySystem;

    private void Start()
    {
        inventorySystem = GameObject.Find("Player").GetComponent<InventorySystem>();
        key.SetActive(false);
        
        key = GameObject.Find("Key");
        brokenTile = GameObject.Find("BrokenTile");
        interactableTile = GameObject.Find("InteractableTile");
        brokenTile.SetActive(false);
        
    }

    public void BreakTile()
    {
        if (brokenTile != null && inventorySystem.HasItem(neededItemName))
        {
            key.SetActive(true);
            brokenTile.SetActive(true);
            Destroy(interactableTile);
        }
        
    }
}
