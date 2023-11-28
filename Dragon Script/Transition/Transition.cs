using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Transition : MonoBehaviour
{
    public float transitionDelay;     
    public Animator transition;
    public bool Start = false;    
    // public int Sceennumber = 0;
    

    // Update is called once per frame
    void Update()
    {
        if (Start)
        {
            LoadNextLevel();            
        }
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1));
    }  

    IEnumerator LoadLevel(int levelIndex)
    {
        //play
        transition.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(transitionDelay);
        //load scene (Use in case that we are use transition in many scene)
        //  switch(Sceennumber)
        //  {
        //     case 0:
        //     UnityEngine.SceneManagement.SceneManager.LoadScene("Start Menu");
        //     break;
                
        //     case 1:
        //     UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        //     break;

        //     case 2:
        //     UnityEngine.SceneManagement.SceneManager.LoadScene("Map");
        //     break;

        //     case 3:
        //     UnityEngine.SceneManagement.SceneManager.LoadScene("Null");
        //     break;

        // } 
        SceneManager.LoadScene(levelIndex);

    }     

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    /*
    public void PlayClickbottonSFX()
    {
        this.GetComponent<AudioSource>().PlayOneShot(ClickSFX);
    }
    */
}

