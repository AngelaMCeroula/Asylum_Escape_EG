using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

public class InteractibleBehaviour : MonoBehaviour
{
    public string interactibleName;
    private PlayerVoiceController PVC;

    private void Awake()
    {
        PVC = GameObject.Find("Player").GetComponent<PlayerVoiceController>();
    }
    
}
