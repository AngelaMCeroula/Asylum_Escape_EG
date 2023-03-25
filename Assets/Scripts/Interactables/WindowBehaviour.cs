using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class WindowBehaviour : MonoBehaviour
{

    private InventorySystem inventorySystem;
    private PlayerVoiceController PVC;
    [SerializeField] private string neededItemName;
    private bool windowIntact = false;

    void Start()
    {
        inventorySystem = GameObject.Find("Player").GetComponent<InventorySystem>();
        PVC = GameObject.Find("Player").GetComponent<PlayerVoiceController>();
    }
    public void SearchWindow()
    {
        if (windowIntact == true)
        {
            Debug.Log("I could break it with something");
        }
        else if (windowIntact == false)
        {
            Debug.Log("I can LEAVE");
        }
    }

    public void BreakGlass()
    {
        if (inventorySystem.HasItem(neededItemName) && windowIntact == true)
        {
            windowIntact = false;
            PVC._canLeave = true;
            //levelManager.End2_Escape();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("I can't do that");
        }
    }
}
