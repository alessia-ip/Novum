using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ARonlyEvents : MonoBehaviour {

    bool vid;
    bool instructionPlay = false;
    bool congrats = false;

    bool goal = false;

    //this audio source is attached to the main first person camera
    public AudioSource narrator;

    public GameObject video;
    public VideoPlayer vidFinish;
    public GameObject ship;
    public GameObject landingPad;
    public AudioClip instructions;
    public AudioClip congratulations;

	
	// Update is called once per frame
	void Update () {
        //gets the video component
        if (video == null){
            video = GameObject.FindWithTag("videoPlayer");
        }
        //gets the renderer
        if (video != null && vidFinish == null){
            vidFinish = video.GetComponent<VideoPlayer>();
        }
        //checks if the video is playing or not
        if(vidFinish != null && (int)vidFinish.frame == (int)vidFinish.frameCount){
            vid = false;
        } else if (vidFinish != null && (int)vidFinish.frame != (int)vidFinish.frameCount)
        {
            vid = true;
        }
        //when the video has stopped playing
        if (vid == false && instructionPlay == false){
            narrator.PlayOneShot(instructions);
            instructionPlay = true;
        }
        //after you've heard the instructions, look for these game objects
        if (vid == false && instructionPlay == true)
        {
            if (ship == null)
            {
                ship = GameObject.FindWithTag("Ship");
            }
            if (landingPad == null)
            {
                landingPad = GameObject.FindWithTag("landing");
            }
        }
        if (ship != null && landingPad != null){
            //basically are they toutching each other 
            if(ship.GetComponent<BoxCollider>().bounds.Intersects(landingPad.GetComponent<BoxCollider>().bounds)){
                goal = true;
            }
        }
        if (goal == true && congrats == false){
            narrator.PlayOneShot(congratulations);
            congrats = true;
        }
    }
}
