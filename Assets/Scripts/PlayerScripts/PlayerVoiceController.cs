using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Inventory;
using UnityEngine;
using UnityEngine.Windows.Speech;


public class PlayerVoiceController : MonoBehaviour
{
    public CharacterController controller;
    
    [SerializeField]private float speed = 12f;
    [SerializeField]private float gravity = -19.62f;
    [SerializeField] private float lookSpeed = 0.2f;
    //[SerializeField]private float jump = 3f;

    public Transform groundCheck;
    [SerializeField]private float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    private Vector3 velocity;
    private bool isGrounded;
    
    // x and z
    private float x;
    private float z;
    
    // voice
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    
    //Camerascript
    public CameraLook cameraLook;
    //inventory
    private InventorySystem inventorySystem;


    private void Start()
    {
        inventorySystem = GetComponent<InventorySystem>();
        
        AddKeywords();
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
        Debug.Log(keywordRecognizer.IsRunning.ToString());
        
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
        //move and look keywords
        actions.Add("forward", MoveForward);
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
        
        //Interaction keywords
        actions.Add("use", UseItem);
        actions.Add("pick up", PickUpItem);

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
        z = 1;
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
        cameraLook.cameraY = 0;
        cameraLook.cameraY += lookSpeed;
    }

    private void LookDown()
    {
        cameraLook.cameraY = 0;
        cameraLook.cameraY -= lookSpeed;
    }

    private void LookRight()
    {
        cameraLook.cameraX = 0;
        cameraLook.cameraX += lookSpeed;
    }

    private void LookLeft()
    {
        cameraLook.cameraX = 0;
        cameraLook.cameraX -= lookSpeed;
    }

    private void LookFront()
    {
        cameraLook.xRotation = 0;
    }
    
    //---------------------------------------------------Interaction
    
    private void Action()
    {
        
    }
    private void PickUpItem()
    {
        
    }
    private void UseItem()
    {
        
    }
    private void DropItem()
    {
        
    }
    
    
    
}
