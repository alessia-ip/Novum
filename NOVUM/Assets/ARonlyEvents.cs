using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ARonlyEvents : MonoBehaviour {

    //This script is all the game logic, so win conditions, and what assets it's looking for
    //Not all assets are completed yet
    //Some delays are still missing to avoid re-triggering certain audio secions more than once
    //also need to fix the timing so they don't overlap one another
    //PlayerOne and PlayerTwo are using different marker cards, and those objects need to be differentiated on. 

    bool vid = true;
    bool instructionPlay = false;
    bool startcore = false;
    bool congrats = false;


    bool goal = false;
    bool answer;


    bool answOne;
    bool answTwo;
    bool answThree;

    bool completeOne = false;
    bool completeTwo = false;
    bool completeThree = false;

    int playerNum;

    //this audio source is attached to the main first person camera
    public AudioSource narrator;


    //These are the first two 'minigame' objects 
    public GameObject ship;
    public GameObject landingPad;

    int SCORE = 0;

    //This can be used for multiple choice answers based on which answer they flip over. 
    //See what object is active in the hierarchy to see  which answer is picked and score via that
   // public GameObject answerOne;
   // public GameObject answerTwo;
   // public GameObject answerThree;


    public AudioClip instructions;
   // public AudioClip multipleChoice;
    public AudioClip congratulations;


    public AudioClip instruct1;
    public AudioClip instruct2;
    public AudioClip instruct3;
    public AudioClip instruct4;
    public AudioClip instruct5;
    public AudioClip betrayal;
    public AudioClip nonReport;
    public AudioClip finalBest;
    public AudioClip finalMeh;
    public AudioClip finalWorst;

    // Update is called once per frame
    void Update () {

        if (GameObject.FindWithTag("videoPlayer") && startcore == false){
            Debug.Log("startcore");
            StartCoroutine(VideoLength());
            startcore = true;
        }

        //This was the original test code I had used to begin my connections. Leaving here to test for debugs in the future
        /*     if (GameObject.FindWithTag("videoPlayer") && instructionPlay == false && vid == false){
                 Debug.Log("playedaudio");
                 narrator.PlayOneShot(instructions);
                 GameObject.FindWithTag("videoHolder").SetActive(false);
                 instructionPlay = true;
             }

             if (GameObject.FindWithTag("Ship") && GameObject.FindWithTag("landing"))
             {
                 if (GameObject.FindWithTag("Ship").GetComponent<BoxCollider>().bounds.Intersects(GameObject.FindWithTag("landing").GetComponent<BoxCollider>().bounds))
                 {                 goal = true;             }         }

             if(goal == true && congrats == false){
                 narrator.PlayOneShot(congratulations);
                 congrats = true;
             } */

        //Setting the player based on the initial scene objects
        if (GameObject.FindWithTag("videoPlayer") && instructionPlay == false && vid == false)
        {
            if (GameObject.FindWithTag("PlayerOne")){
                playerNum = 1;
                narrator.PlayOneShot(instruct1);
            }
            if (GameObject.FindWithTag("PlayerOne"))
            {
                playerNum = 2;
                narrator.PlayOneShot(instruct1);
            }
        }

        //player One events
        if (playerNum == 1){
            TestOne();
            TestTwo();
            TestThree();
        }

        //player Two events
        if (playerNum == 2){
            TestOne();
            TestTwo();
            TestThree();
        }

    }

    IEnumerator VideoLength()
    {
        Debug.Log("core");
        yield return new WaitForSeconds(59);
        vid = false;
    }

    //All the items to test for are listed below!
    //each of these is looking for specific active items based on the player
    //depending on which items the player activates, the results change
    void TestOne(){
        if (playerNum == 1){
            if (GameObject.FindWithTag("correct1").activeSelf && completeOne == false)
            {
                answOne = true;
                completeOne = true;
                SCORE = SCORE + 1;
            } else if (GameObject.FindWithTag("incorrect1").activeSelf && completeOne == false){
                answOne = false;
                completeOne = true;
            }
        }
        if (playerNum == 2)
        {
            if (GameObject.FindWithTag("correct2").activeSelf && completeOne == false)
            {
                answOne = true;
                completeOne = true;
                SCORE = SCORE + 1;
            }
            else if (GameObject.FindWithTag("incorrect2").activeSelf && completeOne == false)
            {
                answOne = false;
                completeOne = true;
            }
        }

    }

    void TestTwo(){
        if (playerNum == 1)
        {
            if (GameObject.FindWithTag("correct3").activeSelf && completeTwo == false)
            {
                answOne = true;
                completeTwo = true;
                SCORE = SCORE + 1;
            }
            else if (GameObject.FindWithTag("incorrect3").activeSelf && completeTwo == false)
            {
                answOne = false;
                completeTwo = true;
            }
        }
        if (playerNum == 2)
        {
            if (GameObject.FindWithTag("correct4").activeSelf && completeTwo == false)
            {
                answOne = true;
                completeTwo = true;
                SCORE = SCORE + 1;
            }
            else if (GameObject.FindWithTag("incorrect4").activeSelf && completeTwo == false)
            {
                answOne = false;
                completeTwo = true;
            }
        }
    }

    //this last test is based on an item the other player gives to you
    //It's not up to the player playing which one they get!
    //see script for different options
    void TestThree(){
        if (playerNum == 1)
        {
            if (GameObject.FindWithTag("correct5").activeSelf && completeThree == false)
            {
                answOne = true;
                completeThree = true;
                SCORE = SCORE + 1;
            }
            else if (GameObject.FindWithTag("incorrect5").activeSelf && completeThree == false)
            {
                answOne = false;
                completeThree = true;
            }
        }
        if (playerNum == 2)
        {
            if (GameObject.FindWithTag("correct6").activeSelf && completeThree == false)
            {
                answOne = true;
                completeThree = true;
                SCORE = SCORE + 1;
            }
            else if (GameObject.FindWithTag("incorrect6").activeSelf && completeThree == false)
            {
                answOne = false;
                completeThree = true;
            }
        }
    }

    //this is what the player hears at the end of the game, depending on how they did, and what their partner did
    void FINAL(){
        if (completeOne == true && completeTwo == true && completeThree == true){
            if(answThree == false){
                narrator.PlayOneShot(nonReport);
            } else if (answThree == true){
                narrator.PlayOneShot(betrayal);
            }
        }
        if (SCORE == 3){
            narrator.PlayOneShot(finalBest);
        } else if (SCORE == 0){
            narrator.PlayOneShot(finalWorst);
        } else {
            narrator.PlayOneShot(finalMeh);
        }
    }

}
