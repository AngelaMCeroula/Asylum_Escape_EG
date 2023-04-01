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
    private UITextResponseManager _uiTextResponseManager;

    private void Start()
    {
        _uiTextResponseManager = GameObject.Find("UITextResponseManager").GetComponent<UITextResponseManager>();
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
            Debug.Log("This tile seems loose");
            _uiTextResponseManager.TextToUI("This tile seems loose");
        }

        else
        {
            Debug.Log("It's broken");
            _uiTextResponseManager.TextToUI("It's broken");
        }
       
    }

    public void BreakTile()
    {
        if (interactableTile != null && inventorySystem.HasItem(neededItemName))
        {
            key.SetActive(true);
            brokenTile.SetActive(true);
            Destroy(interactableTile);
            Debug.Log("There's a Key underneath");
            _uiTextResponseManager.TextToUI("There's a Key underneath");
        }
        
        else
        {
            Debug.Log("I can't do that");
            _uiTextResponseManager.TextToUI("I can't do that");
        }
        
    }
}
