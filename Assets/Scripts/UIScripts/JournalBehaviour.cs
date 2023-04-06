using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UIElements;

public class JournalBehaviour : MonoBehaviour
{
    [SerializeField]private GameObject openJournalButton;
    private LevelManager _levelManager;
    
    //[HideInInspector]public bool _journalIsOpen;

    [HideInInspector]public bool _journalOpen;
    private Animator _animator;
    
    private void Start()
    {
        _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        _animator = gameObject.GetComponent<Animator>();
        
    }

    public void OpenJournal()
    {
        if (_levelManager._gameIsPaused == false)
        {
            if (_journalOpen == false)
            {
                _journalOpen = true;
                openJournalButton.SetActive(false);
                JournalOpenTransform();
            }
        }
    }

    public void CloseJournal()
    {
        if (_levelManager._gameIsPaused == false)
        {
            if (_journalOpen == true)
            {
                _journalOpen = false;
                JournalCloseTransform();
                Wait();
            }
        }
    }
    
    void JournalOpenTransform()
    {
        _animator.SetTrigger("Journal_In");

    }

    void JournalCloseTransform()
    {
        _animator.SetTrigger("Journal_Out");
    }
    
    async void Wait()
    {
        await Task.Delay(2 * 1000);
        openJournalButton.SetActive(true);
    }
}
