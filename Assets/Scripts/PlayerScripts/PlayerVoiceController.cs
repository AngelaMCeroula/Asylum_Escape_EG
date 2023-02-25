using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;


public class PlayerVoiceController : MonoBehaviour
{
    public CharacterController controller;
    
    public float speed = 12f;
    public float gravity = -19.62f;
    public float jump = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    private Vector3 velocity;
    private bool isGrounded;
    
    // x and z
    private float x;
    private float z;
    
    // voice
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();


    private void Start()
    {
        actions.Add("forward", MoveForward);
        actions.Add("move", MoveForward);
        actions.Add("stop", Stop);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
        

    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        Movement();
        //Talk();
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
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
        }
        
        // gravity 
        velocity.y += gravity * Time.deltaTime;
        
        //movement falling motion speed
        controller.Move(velocity * Time.deltaTime);
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void MoveForward()
    {
        z = 1;

    }

    private void Stop()
    {
        x = 0;
        z = 0;

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
    
}
