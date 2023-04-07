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
    public LevelManager levelManager;

    //public LayerMask collisionLayer;
    
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
    
    //UI Text
    private UITextResponseManager _uiTextResponseManager;


    private AudioSource _audioSource;
    //CAN LEAVE
    //[HideInInspector]public bool _canLeave = false;
    //private bool nearExit = false;
    
    
   

    private void Start()
    {
        //---------------------Components
        inventorySystem = GetComponent<InventorySystem>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        _uiTextResponseManager = GameObject.Find("UITextResponseManager").GetComponent<UITextResponseManager>();
        _audioSource = GetComponent<AudioSource>();
        
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
        actions.Add("hold", Stop);
        actions.Add("hold up", Stop);
        
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
        actions.Add("search", Search);
        
        
        //-------- Use Commands
        actions.Add("use key", UseKey);
        actions.Add("use hammer", UseHammer);
        actions.Add("use screwdriver", UseScrewdriver);
        actions.Add("use bobby pin", UseBobby);
        
        
        //-----------Exit Commands
        /*
        actions.Add("leave", EndLevel);
        actions.Add("exit", EndLevel);
        */
        
        //----------UI commands
        actions.Add("pause game", PauseGame);
        actions.Add("resume game", ResumeGame);
        actions.Add("resume", ResumeGame);
        actions.Add("main menu", MainMenu);
        actions.Add("quit", QuitGame);
        actions.Add("quit game", QuitGame);
        actions.Add("open journal", OpenJournal);
        actions.Add("close journal", CloseJournal);
        actions.Add("help", HelpScreen);
        
        //-------- misc commands
        actions.Add("fuck you", FYou);
        actions.Add("listen to me", ListenHere);
        actions.Add("can you hear me", CanYouHearMe);
        actions.Add("hello", HelloBack);
        actions.Add("who are you", ItsAMe);
        
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
            //Debug.Log(_interactibleBehaviour.interactibleName+" in range "+isInteractableInRange);
            //_uiTextResponseManager.TextToUI("Mmm");
        }
        
        /*

        if (other.CompareTag("EXIT"))
        {
            nearExit = true;
            if (_canLeave == true)
            {
                Debug.Log("I can leave through here");
                _uiTextResponseManager.TextToUI("I can leave through here");
            }
        }
        */
        
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
            Debug.Log("interactible out of range");
        }
        if (other.CompareTag("EXIT"))
        {
            //nearExit = false;
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
        _uiTextResponseManager.recognizedKeywordIndicate();
    }
    
    //---------------------------------------------------Movement

    private void MoveForward()
    {
        z = 0;
        z = speed * Time.deltaTime;
        _audioSource.Play();
        
    }

    public void Stop()
    {
        x = 0;
        z = 0;
        cameraLook.cameraX = 0f;
        cameraLook.cameraY = 0f;
        _audioSource.Stop();
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
            _uiTextResponseManager.TextToUI("There is nothing nearby to pick up.");
            Debug.Log("There is nothing nearby to pick up.");
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
                    _uiTextResponseManager.TextToUI("I can't use it here");
                    Debug.Log("I can't use it here");
                }
            }
            
            else
            {
                _uiTextResponseManager.TextToUI("I don't see where to use it!");
                Debug.Log("There is nowhere to use it!");
            }
        }
        else
        {
            _uiTextResponseManager.TextToUI("I don't have that");
            Debug.Log("I don't have that");
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
                    _uiTextResponseManager.TextToUI("I can't use it here");
                    Debug.Log("I can't use it here");
                }
            }
            else
            {
                _uiTextResponseManager.TextToUI("There is nowhere to use it!");
                Debug.Log("There is nowhere to use it!");
            }
        }
        else
        {
            _uiTextResponseManager.TextToUI("I don't have that");
            Debug.Log("I don't have that");
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
                    _uiTextResponseManager.TextToUI("I can't use it here");
                    Debug.Log("I can't use it here");
                }
            }
            else
            {
                _uiTextResponseManager.TextToUI("There is nowhere to use it!");
                Debug.Log("There is nowhere to use it!");
            }
        }
        else
        {
            _uiTextResponseManager.TextToUI("I don't have that");
            Debug.Log("I don't have that");
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
                    _uiTextResponseManager.TextToUI("I can't use it here");
                    Debug.Log("I can't use it here");
                }
            }
            else
            {
                _uiTextResponseManager.TextToUI("There is nowhere to use it!");
                Debug.Log("There is nowhere to use it!");
            }
        }
        else
        {
            _uiTextResponseManager.TextToUI("I don't have that");
            Debug.Log("I don't have that");
        }
        
    }
    
    //-----------------------------------SEARCH COMMANDS
    
    private void Search()
    {
        if (isInteractableInRange == true)
        {
            if (_interactibleBehaviour == null)
            {
                _uiTextResponseManager.TextToUI("There is nothing of interest.");
                Debug.Log("There is nothing of interest");
            }
            
            else if (_interactibleBehaviour.interactibleName == "Tile")
            {
                tileBehaviour.SearchTile();
            }
            
            else if (_interactibleBehaviour.interactibleName == "Cabinet")
            {
                cabinetBehaviour.SearchCabinet();
            }
            
            else if (_interactibleBehaviour.interactibleName == "Fireplace")
            {
                fireplaceBehaviour.SearchFireplace();
                
            }
            else if (_interactibleBehaviour.interactibleName == "Door")
            {
                doorBehaviour.SearchDoor();
            }
            
            else if (_interactibleBehaviour.interactibleName == "Window")
            {
                windowBehaviour.SearchWindow();
            }
            
            else if (_interactibleBehaviour.interactibleName == "Cupboard")
            {
                _uiTextResponseManager.TextToUI("There is a Bobby Pin.");
                Debug.Log("There is a Bobby Pin.");
            }
        }

        else
        {
            _uiTextResponseManager.TextToUI("There is nothing interesting close enough to search.");
            Debug.Log("There is nothing close enough to search.");
        }
        
    }

    /*
    private void EndLevel()
    {
        if (nearExit && _canLeave)
        {
            levelManager.End2_Escape();
        }
        else if (nearExit == true && _canLeave == false)
        {
            _uiTextResponseManager.TextToUI("Exit is closed.");
            Debug.Log("Exit is closed");
        }
       
        else if (nearExit == false && _canLeave == true)
        {
            _uiTextResponseManager.TextToUI("Need to go to an exit.");
            Debug.Log("Need to go to an exit");
        }

        else
        {
            _uiTextResponseManager.TextToUI("There is no way out yet");
            Debug.Log("There is no way out yet");
        }
        
        
    }
    */
    
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
        levelManager._journalBehaviour.OpenJournal();
    }

    private void MainMenu()
    {
        levelManager.MainMenu();
    }
    private void QuitGame()
    {
        levelManager.QuitGame();
    }

    private void CloseJournal()
    {
        levelManager._journalBehaviour.CloseJournal();
    }

    private void HelpScreen()
    {
        levelManager.HelpScreen();
    }
    
    
    //-----------------------------------MISC COMMANDS
    private void FYou()
    {
       
        _uiTextResponseManager.TextToUI("F- you too Buddy");
        Debug.Log("F- you too Buddy");
        
    }
    private void ListenHere()
    { 
        _uiTextResponseManager.TextToUI("You should calm down, I'm learning");
        Debug.Log("You should calm down, I'm learning");
    }
    private void CanYouHearMe()
    { 
        _uiTextResponseManager.TextToUI("Yes.");
        Debug.Log("Yes.");
    }
    private void HelloBack()
    { 
        _uiTextResponseManager.TextToUI("Hello? Who are you?");
        Debug.Log("Hello? Who are you?");
    }
    
    private void ItsAMe()
    { 
        _uiTextResponseManager.TextToUI("It's a me, Mario!");
        Debug.Log("It's a me, Mario!");
    }

    private void AddAllKeyItems()
    {
        _uiTextResponseManager.TextToUI("You wish!");
        Debug.Log("You wish!");
        
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
