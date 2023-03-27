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
        key = GameObject.Find("Key");
        brokenTile = GameObject.Find("BrokenTile");
        interactableTile = GameObject.Find("InteractableTile");
        
        brokenTile.SetActive(false);
        key.SetActive(false);
        
    }

    public void SearchTile()
    {
        if (interactableTile != null)
        {
            Debug.Log("It's loose");
        }

        else
        {
            Debug.Log("It's broken");
        }
       
    }

    public void BreakTile()
    {
        if (interactableTile != null && inventorySystem.HasItem(neededItemName))
        {
            key.SetActive(true);
            brokenTile.SetActive(true);
            Destroy(interactableTile);
        }
        
        else
        {
            Debug.Log("I can't do that");
        }
        
    }
}
