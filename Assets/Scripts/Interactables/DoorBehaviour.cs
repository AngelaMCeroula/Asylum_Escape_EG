using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using System.Threading.Tasks;

[RequireComponent(typeof(CapsuleCollider))]
public class DoorBehaviour : MonoBehaviour
{
    private InventorySystem inventorySystem;
    private PlayerVoiceController PVC;
    [SerializeField] private OpenDoorAnimation _openDoorAnimation;
    [SerializeField] private string neededItemName;
    private bool doorClosed = true;
    private GameObject doorExitTriggerArea;
    private LevelManager levelManager;
    private UITextResponseManager _uiTextResponseManager;
    public AudioSource _audioSource;
    public AudioSource _audioSource2;
    

    void Start()
    {
        _uiTextResponseManager = GameObject.Find("UITextResponseManager").GetComponent<UITextResponseManager>();
        inventorySystem = GameObject.Find("Player").GetComponent<InventorySystem>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        PVC = GameObject.Find("Player").GetComponent<PlayerVoiceController>();
        doorExitTriggerArea = GameObject.Find("DoorExitTrigger");
        doorExitTriggerArea.SetActive(false);
    }

    public void SearchDoor()
    {
        if (doorClosed == true)
        {
            _uiTextResponseManager.TextToUI("It's looked, I need a key");
            Debug.Log("It's looked, I need a key");
        }
        else if (doorClosed == false)
        {
            _uiTextResponseManager.TextToUI("I can LEAVE");
            Debug.Log("I can LEAVE");
        }
    }

    public void OpenDoor()
    {
        if (inventorySystem.HasItem(neededItemName) && doorClosed == true)
        {
            doorClosed = false;
            //doorExitTriggerArea.SetActive(true);
            _audioSource.PlayOneShot(_audioSource.clip, 1);
            SecondIntWait();
            EndLevelWait();
        }
        else
        {
            _uiTextResponseManager.TextToUI("I can't do that");
            Debug.Log("I can't do that");
        }

        async void SecondIntWait()
        {
            await Task.Delay(1 * 1000);
            _audioSource2.PlayOneShot(_audioSource.clip, 1);
            _openDoorAnimation.OpenDoorTrigger();
        }

        async void EndLevelWait()
        {
            await Task.Delay(3 * 1000);
            levelManager.End2_Escape();
        }

    }
}
