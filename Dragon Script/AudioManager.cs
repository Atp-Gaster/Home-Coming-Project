using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] SoundList;
    public AudioSource Source;
    public AudioSource Source2; //Handle with repeating SFx

    /*
     * 0 to 5 suppose to use as walk SFX
     * 6 to 11 suppose to use as Running SFX
     * 
     * 
     * 
     * 
     * 
     * 
     */

    public void Playsound(int index)
    {
        if(index == 0)
        {
            Source.clip = SoundList[0];
            Source.Play();
        }

        if (index > 0)
        {
            Source2.clip = SoundList[index];
            Source2.Play();
        }

    

    }    
}
