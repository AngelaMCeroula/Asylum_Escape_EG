using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorAnimation : MonoBehaviour
{
    private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    public void OpenDoorTrigger()
    {
        _animator.SetTrigger("OpenDoorAnim");
    }

}
