using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public LayerMask collisionLayer;
    private PlayerVoiceController _playerVoiceController;


    private void Start()
    {
        _playerVoiceController = GetComponent<PlayerVoiceController>();
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
        if (hit.gameObject.layer == 0)
        {
            _playerVoiceController.Stop();
            
        }
    }
    
    
}
