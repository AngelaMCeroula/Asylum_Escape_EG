using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionsManager : MonoBehaviour
{
    public GameObject CrossFadeImg;

    private Animator crossfadeAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        CrossFadeImg = GameObject.Find("CrossFade");
        crossfadeAnimator = CrossFadeImg.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
