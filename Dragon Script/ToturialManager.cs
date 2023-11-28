using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToturialManager : MonoBehaviour
{
    public GameObject[] ToturialPanal;
    public GameObject[] ToturialHitbox;
    public int CurrentToturial = 0;
    public GameObject CloseButton;
    public void ShowPickLock()
    {
        ToturialPanal[CurrentToturial].gameObject.SetActive(true);
        CloseButton.SetActive(true);
    }   
    public void CloseTutorial()
    {
        ToturialPanal[CurrentToturial].gameObject.SetActive(false);        
        Destroy(ToturialHitbox[CurrentToturial].gameObject);
        CloseButton.SetActive(false);
        CurrentToturial++;
    }   
}
