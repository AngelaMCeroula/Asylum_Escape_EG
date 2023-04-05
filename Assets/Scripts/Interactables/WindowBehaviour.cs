using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class WindowBehaviour : MonoBehaviour
{

    private InventorySystem inventorySystem;
    private PlayerVoiceController PVC;
    [SerializeField] private string neededItemName;
    [SerializeField] private string neededItemName2;
    private bool windowIntact;
    //private GameObject windowExitTriggerArea;
    [SerializeField] private BreakableWindow _breakableWindow;
    private UITextResponseManager _uiTextResponseManager;

    [SerializeField] private GameObject windowGlass;

    void Start()
    {
        _uiTextResponseManager = GameObject.Find("UITextResponseManager").GetComponent<UITextResponseManager>();
        inventorySystem = GameObject.Find("Player").GetComponent<InventorySystem>();
        PVC = GameObject.Find("Player").GetComponent<PlayerVoiceController>();
        //windowExitTriggerArea = GameObject.Find("WindowExitTrigger");
        //windowExitTriggerArea.SetActive(false);
        
        windowGlass = GameObject.Find("WindowGlass");
        if (windowGlass != null)
        {
            windowIntact = true;
        }
            
    }
    public void SearchWindow()
    {
        if (windowIntact == true)
        {
            Debug.Log("I could break it with something");
            _uiTextResponseManager.TextToUI("I could break it with something");
        }
        else if (windowIntact == false)
        {
            Debug.Log("I can LEAVE");
            _uiTextResponseManager.TextToUI("I can LEAVE");
        }
    }

    public void BreakGlass()
    {
        if (inventorySystem.HasItem(neededItemName))
        {
            GlassBreak();
            EndLevelWait();

        }
        else if(inventorySystem.HasItem(neededItemName2))
        {
           
            GlassBreak();
            EndLevelWait();
        }
        else
        {
            Debug.Log("I can't do that");
            _uiTextResponseManager.TextToUI("I can't do that");
        }
    }
    
    async void EndLevelWait()
    {
        await Task.Delay(3 * 1000);
        PVC.levelManager.End2_Escape();
    }

    private void GlassBreak()
    {
        // breakablewindow trying to access something in splinters but saying that splinters were destroyed
        //_breakableWindow.breakWindow();
        windowGlass.SetActive(false);
    }

}
