using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> responses;
    public AudioClip _UIButtonClick;

    private void Start()
    {
        
    }

    public void UIButtonPress()
    {
        AudioSource.PlayClipAtPoint(_UIButtonClick, transform.position);
    }
}
