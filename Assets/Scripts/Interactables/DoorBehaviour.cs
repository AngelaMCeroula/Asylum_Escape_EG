using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class DoorBehaviour : MonoBehaviour
{
    private InventorySystem inventorySystem;
    private PlayerVoiceController PVC;
    [SerializeField] private string neededItemName;
    private bool doorClosed = true;
    private GameObject doorExitTriggerArea;
    

    void Start()
    {
        inventorySystem = GameObject.Find("Player").GetComponent<InventorySystem>();
        PVC = GameObject.Find("Player").GetComponent<PlayerVoiceController>();
        doorExitTriggerArea = GameObject.Find("DoorExitTrigger");
        doorExitTriggerArea.SetActive(false);
    }

    public void SearchDoor()
    {
        if (doorClosed == true)
        {
            Debug.Log("It's looked, I need a key");
        }
        else if (doorClosed == false)
        {
            Debug.Log("I can LEAVE");
        }
    }

    public void OpenDoor()
    {
        if (inventorySystem.HasItem(neededItemName) && doorClosed == true)
        {
            doorClosed = false;
            PVC._canLeave = true;
            doorExitTriggerArea.SetActive(true);
            //levelManager.End2_Escape();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("I can't do that");
        }

        

    }
}
