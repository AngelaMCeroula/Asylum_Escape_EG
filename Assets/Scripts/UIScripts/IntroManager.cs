using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [SerializeField]private GameObject intro1, intro2, intro3;
    [SerializeField]private Animator transition;
    
    [SerializeField]private float time1 = 3;
    [SerializeField]private float time2 = 5;
    [SerializeField]private float time3 = 5;
    
    
    // Start is called before the first frame update
    void Start()
    {
        transition = GameObject.Find("CrossFade").GetComponent<Animator>();
        intro1.SetActive(true);
        intro2.SetActive(false);
        intro3.SetActive(false);
        FadeIn();
    }

    void FadeIn()
    {
        StartCoroutine(Fade1());
    }
    

    IEnumerator Fade1()
    {
        transition.SetTrigger("FULL");
        yield return new WaitForSeconds(time1);
        intro1.SetActive(false);
        intro2.SetActive(true);
        StartCoroutine(Next1());
    }

    IEnumerator Next1()
    {
        yield return new WaitForSeconds(time2);
        intro2.SetActive(false);
        intro3.SetActive(true);
        StartCoroutine(Next2());
        
    }
    IEnumerator Next2()
    {
        yield return new WaitForSeconds(time3);
        SceneManager.LoadScene("MainMenu");

    }
   
}
