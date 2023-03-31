using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Inventory;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows.Speech;


public class PlayerVoiceController : MonoBehaviour
{
    public CharacterController controller;
    private LevelManager levelManager;
    
    [SerializeField]private float speed = 12f;
    [SerializeField]private float gravity = -19.62f;
    
    //[SerializeField]private float jump = 3f;

    public Transform groundCheck;
    [SerializeField]private float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    private Vector3 velocity;
    private bool isGrounded;
    
    // x and z movement
    private float x;
    private float z;
    
    // voice
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    
    //Camerascript
    public CameraLook cameraLook;
    //[SerializeField] private float lookSpeedX = 2;
    [SerializeField] private float lookSpeedY = 2;
    [SerializeField] private float lookTimeInSeconds = 1f;
    [SerializeField] private float angleY = 45f;
    

    //inventory
    private InventorySystem inventorySystem;
    private ItemBehaviour itemBehaviour;
    private bool isKeyItemInRange;
    private bool isInteractableInRange;
    
    //Interactibles
    private InteractibleBehaviour _interactibleBehaviour;
    private CabinetBehaviour cabinetBehaviour;
    private TileBehaviour tileBehaviour;
    private FireplaceBehaviour fireplaceBehaviour;
    private DoorBehaviour doorBehaviour;
    private WindowBehaviour windowBehaviour;
    
    //UI
    //[SerializeField] private GameObject TEXT;
    
    //CAN LEAVE
    [HideInInspector]public bool _canLeave = false;
    private bool nearExit = false;
    
    
   

    private void Start()
    {
        //---------------------Components
        inventorySystem = GetComponent<InventorySystem>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        
        cabinetBehaviour = GameObject.Find("Cabinet").GetComponent<CabinetBehaviour>();
        tileBehaviour = GameObject.Find("Tiles").GetComponent<TileBehaviour>();
        fireplaceBehaviour = GameObject.Find("Fireplace").GetComponent<FireplaceBehaviour>();
        doorBehaviour = GameObject.Find("ExitDoor").GetComponent<DoorBehaviour>();
        windowBehaviour = GameObject.Find("ExitWindow").GetComponent<WindowBehaviour>();
        
        
        AddKeywords();
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
        Debug.Log("keyword recognizer on is " + keywordRecognizer.IsRunning.ToString());
        

    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        Movement();
        //Talk();
    }

    void AddKeywords()
    {
        //--------move and look keywords
        actions.Add("move", MoveForward);
        actions.Add("walk", MoveForward);
        actions.Add("stop", Stop);
        actions.Add("look up", LookUp);
        actions.Add("up", LookUp);
        actions.Add("look down", LookDown);
        actions.Add("down", LookDown);
        actions.Add("look right", LookRight);
        actions.Add("right", LookRight);
        actions.Add("turn right", LookRight);
        actions.Add("look left", LookLeft);
        actions.Add("turn left", LookLeft);
        actions.Add("left", LookLeft);
        actions.Add("turn around", TurnAround);
        actions.Add("turn", TurnAround);
        actions.Add("look back", TurnAround);
        actions.Add("look front", LookFront);
        actions.Add("front", LookFront);
        actions.Add("look forward", LookFront);
        
        //--------Interaction keywords
       
        actions.Add("pick up", PickUpItem);
        
        
        //-------- Use Commands
        actions.Add("use key", UseKey);
        actions.Add("use hammer", UseHammer);
        actions.Add("use screwdriver", UseScrewdriver);
        actions.Add("use bobby pin", UseBobby);
        
        
        //-------- search commands
        actions.Add("search", Search);
        
        //-----------Exit Commands
        actions.Add("leave", EndLevel);
        actions.Add("exit", EndLevel);
        
        //----------UI commands
        actions.Add("pause game", PauseGame);
        actions.Add("resume game", ResumeGame);
        actions.Add("open journal", OpenJournal);
        actions.Add("close journal", CloseJournal);
        actions.Add("help", HelpScreen);
        
        //-------- misc commands
        actions.Add("fuck you", AppQuit);
        actions.Add("listen to me", ListenHere);
        
        //Debug Commands
        actions.Add("give me everything", AddAllKeyItems);



    }
    //---------------------------------------------------Collision check-------------------------
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KeyItem"))
        {
            isKeyItemInRange = true;
            itemBehaviour = other.gameObject.GetComponent<ItemBehaviour>();
            Debug.Log(itemBehaviour.itemName+" in range " + isKeyItemInRange);
        }
        if (other.CompareTag("Interactable"))
        {
            isInteractableInRange = true;
            _interactibleBehaviour = other.gameObject.GetComponent<InteractibleBehaviour>();
            Debug.Log(_interactibleBehaviour.interactibleName+" in range "+isInteractableInRange);
        }

        if (other.CompareTag("EXIT"))
        {
            nearExit = true;
            if (_canLeave == true)
            {
                Debug.Log("I can leave through here");
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("KeyItem"))
        {
            isKeyItemInRange = false;
            itemBehaviour = null;
        }
        if (other.CompareTag("Interactable"))
        {
            isInteractableInRange = false;
            _interactibleBehaviour = null;
        }
        if (other.CompareTag("EXIT"))
        {
            nearExit = false;
        }
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
    }

    void Movement()
    {
        // using input
        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * (speed * Time.deltaTime));

        //jump input
        /*
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
        }
        */
        
        // gravity 
        velocity.y += gravity * Time.deltaTime;
        
        //movement falling motion speed
        controller.Move(velocity * Time.deltaTime);
    }
    
    private void Talk()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            keywordRecognizer.Start();
            Debug.Log(keywordRecognizer.IsRunning.ToString());
        }

        if (Input.GetKeyUp(KeyCode.V))
        {
            keywordRecognizer.Stop();
        }
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    
    //---------------------------------------------------Movement

    private void MoveForward()
    {
        z = speed * Time.deltaTime;
    }

    private void Stop()
    {
        x = 0;
        z = 0;
        cameraLook.cameraX = 0f;
        cameraLook.cameraY = 0f;
    }

    private void TurnAround()
    {
        transform.localRotation = transform.localRotation * Quaternion.Euler(0f,180f,0f);
    }

    private void LookUp()
    {
        //m1
        cameraLook.cameraY = 0;
        cameraLook.cameraY += lookSpeedY;
        StartCoroutine(LookTime());
        
        //m2
        
    }

    private void LookDown()
    {
        //m1
        cameraLook.cameraY = 0;
        cameraLook.cameraY -= lookSpeedY;
        StartCoroutine(LookTime());
        
        //m2
        
    }

    private void LookRight()
    {
        //m1
        /*
        cameraLook.cameraX = 0f;
        cameraLook.cameraX += lookSpeedX;
        StartCoroutine(LookTime());
        */

       
        //m2
        transform.localRotation = transform.localRotation * Quaternion.Euler(0f,angleY,0f);
    }

    private void LookLeft()
    {
        
        //m1
          /*
        cameraLook.cameraX = 0f;
        cameraLook.cameraX -= lookSpeedX;
        StartCoroutine(LookTime());
        */

        //m2
        transform.localRotation = transform.localRotation * Quaternion.Euler(0f,-angleY,0f);

    }

    private void LookFront()
    {
        //m1
        cameraLook.cameraY = 0;
        cameraLook.xRotation = 0;
        
    }
    
    //---------------------------------------------------Interaction
    
    // method template
    private void Action()
    {
        // add action on start method e.g. actions.Add("keyword", Action);
        //create method 
    }

    private void PickUpItem()
    {
        if (isKeyItemInRange == true && itemBehaviour != null)
        {
            itemBehaviour.AddItem();
        }
        
        else
        {
            Debug.Log("Pick up what?");
        }
        
    }
    
    //-----------------------------------USE COMMANDS
    private void UseKey()
    {
        if (inventorySystem.HasItem("Key") == true)
        {
            if (isInteractableInRange == true)
            {
                if (_interactibleBehaviour.interactibleName == "Door")
                {
                    doorBehaviour.OpenDoor();
                }
                else
                {
                    Debug.Log("I can't use it here");
                }
            }
            else
            {
                Debug.Log("Use it where? (You are out of range)");
            }
        }
        else
        {
            Debug.Log("I don't have a Key");
        }
        
    }
    private void UseBobby()
    {
        if (inventorySystem.HasItem("BobbyPin") == true)
        {
            if (isInteractableInRange == true)
            {
                if (_interactibleBehaviour.interactibleName == "Cabinet")
                {
                    cabinetBehaviour.OpenCabinet();
                }
                else
                {
                    Debug.Log("I can't use it here");
                }
            }
            else
            {
                Debug.Log("Use it where? (You are out of range)");
            }
        }
        else
        {
            Debug.Log("I don't have a BobbyPin");
        }
    }
    private void UseScrewdriver()
    {
        if (inventorySystem.HasItem("Screwdriver") == true)
        {
            if (isInteractableInRange == true)
            {
                if (_interactibleBehaviour.interactibleName == "Window")
                {
                    windowBehaviour.BreakGlass();
                }
                else
                {
                    Debug.Log("I can't use it here");
                }
            }
            else
            {
                Debug.Log("Use it where? (You are out of range)");
            }
        }
        else
        {
            Debug.Log("I don't have a Screwdriver");
        }
        
    }
    private void UseHammer()
    {
        if (inventorySystem.HasItem("Hammer") == true)
        {
            if (isInteractableInRange == true)
            {
                if (_interactibleBehaviour.interactibleName == "Window")
                {
                    windowBehaviour.BreakGlass();
                }
                
                else if (_interactibleBehaviour.interactibleName == "Tile")
                {
                    tileBehaviour.BreakTile();
                }
                else
                {
                    Debug.Log("I can't use it here");
                }
            }
            else
            {
                Debug.Log("Use it where? (You are out of range)");
            }
        }
        else
        {
            Debug.Log("I don't have a Hammer");
        }
        
    }
    
    //-----------------------------------SEARCH COMMANDS
    
    private void Search()
    {
        if (isInteractableInRange == true)
        {
            if (_interactibleBehaviour.interactibleName == "Tile")
            {
                tileBehaviour.SearchTile();
            }
            
            if (_interactibleBehaviour.interactibleName == "Cabinet")
            {
                cabinetBehaviour.SearchCabinet();
            }
            
            if (_interactibleBehaviour.interactibleName == "Fireplace")
            {
                fireplaceBehaviour.SearchFireplace();
                
            }
            if (_interactibleBehaviour.interactibleName == "Door")
            {
                doorBehaviour.SearchDoor();
            }
            
            if (_interactibleBehaviour.interactibleName == "Window")
            {
                windowBehaviour.SearchWindow();
            }
            
            else if (_interactibleBehaviour == null)
            {
                Debug.Log("There is nothing of interest");
            }

        }

        else
        {
            Debug.Log("Search what?");
        }
        
    }

    private void EndLevel()
    {
        if (nearExit && _canLeave)
        {
            levelManager.End2_Escape();
        }
        else if (nearExit == true && _canLeave == false)
        {
            Debug.Log("Exit is closed");
        }
       
        else if (nearExit == false && _canLeave == true)
        {
            Debug.Log("Need to go to an exit");
        }

        else
        {
            Debug.Log("There is no way out yet");
        }
    }
    
    //-----------------------------------IU COMMANDS

    private void PauseGame()
    {
        levelManager.PauseGame();
    }
    private void ResumeGame()
    {
        levelManager.ResumeGame();
    }

    private void OpenJournal()
    {
        levelManager.OpenJournal();
    }

    private void CloseJournal()
    {
        levelManager.CloseJournal();
    }

    private void HelpScreen()
    {
        levelManager.HelpScreen();
    }
    
    
    //-----------------------------------MISC COMMANDS
    private void AppQuit()
    {
        //TEXT.SetActive(true);
        Debug.Log("F- you too Buddy");
        
    }
    private void ListenHere()
    { 
        Debug.Log("You should calm down, I'm learning");
    }

    private void AddAllKeyItems()
    {
        inventorySystem.AddItem("Key");
        inventorySystem.AddItem("Screwdriver");
        inventorySystem.AddItem("Hammer");
        inventorySystem.AddItem("BobbyPin");
    }
    
    //-----------------------------------coroutine
    IEnumerator LookTime()
    {
        yield return new WaitForSeconds(lookTimeInSeconds);
        cameraLook.cameraX = 0f;
        cameraLook.cameraY = 0f;
    }
    
}
