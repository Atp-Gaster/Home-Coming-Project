using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private IEnumerator coroutine;
    public bool enableNextScene = false;
    public Animator SceneAnimator;
    public bool FirstCut = false;

    public IEnumerator DelayTransition()
    {
        yield return new WaitForSeconds(3f);
        FirstCut = true;
    }

    public IEnumerator DelayNextScene()
    {
        SceneAnimator.SetInteger("State", 2);
        Debug.Log(SceneAnimator.GetInteger("State"));
        yield return new WaitForSeconds(2.3f);
        enableNextScene = true;
    }

    public void LoadGameScene()
    {        
        SceneManager.LoadScene(1);        
    }

    private void Update()
    {
        if (Input.anyKey)
        {            
            SceneAnimator.SetInteger("State", 1);
            StartCoroutine(DelayTransition());
        }
        if(FirstCut)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Test");
                
                StartCoroutine(DelayNextScene());
            }
        }
      
        

        if (enableNextScene) LoadGameScene();       
    }
}
