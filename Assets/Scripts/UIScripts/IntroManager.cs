using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [SerializeField]private GameObject intro1, intro2, intro3;
    [SerializeField]private CanvasGroup canvas1, canvas2, canvas3;
    [SerializeField]private float timeInSeconds = 5;
    [SerializeField]private float timeInBetweenFade = 2;
    
    
    // Start is called before the first frame update
    void Start()
    {
        canvas1 = intro1.GetComponent<CanvasGroup>();
        canvas2 = intro2.GetComponent<CanvasGroup>();
        canvas3 = intro3.GetComponent<CanvasGroup>();
        
        StartCoroutine(Delay1());
    }
    
    

    IEnumerator Delay1()
    {
        yield return new WaitForSeconds(timeInSeconds);
        canvas1.alpha -= Time.deltaTime;
        StartCoroutine(Delay2());
    }
    IEnumerator Delay2()
    {
        yield return new WaitForSeconds(timeInSeconds);
        canvas2.alpha += Time.deltaTime;
        //SceneManager.LoadScene("MainMenu");


    }
    
    
}
