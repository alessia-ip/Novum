using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artist_Dialogue_SCR : MonoBehaviour
{

    bool PlayedOnce = false;
    bool PlayedOnActive = false;

    public AudioSource Artist;

    public AudioClip LongIntro;
    public AudioClip AbridgedInstructions;

    private void OnEnable()
    {
        if(PlayedOnce == false && PlayedOnActive == false){
            Artist.PlayOneShot(LongIntro);
            PlayedOnce = true;
            PlayedOnActive = true;
        } else if (PlayedOnce == true && PlayedOnActive == false){
            Artist.PlayOneShot(AbridgedInstructions);
            PlayedOnActive = true;
        }
    }

    private void OnDisable()
    {
        PlayedOnActive = false;
    }

}
