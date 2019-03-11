using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Audio_SCR : MonoBehaviour
{

    public Gamemanager_SCR gamemanager;

    bool PlayedOnActive = false;

    public AudioSource thisObj;

    public AudioClip Audio;

    private void OnEnable()
    {
        if (PlayedOnActive == false)
        {
            thisObj.PlayOneShot(Audio);
            PlayedOnActive = true;
        }

        if(gamemanager.started == false){
            gamemanager.started = true;
            gamemanager.pubPublish(gamemanager.pConf + " has started their game."); 
        }

    }

    private void OnDisable()
    {
        PlayedOnActive = false;
    }
}
