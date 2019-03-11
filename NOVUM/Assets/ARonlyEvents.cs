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


    bool playing = false;

    //this audio source is attached to the main first person camera
    public AudioSource narrator;


    //These are the first two 'minigame' objects 
    //public GameObject ship;
    //public GameObject landingPad;

    int SCORE = 0;



    public AudioClip instruct1;
    public AudioClip instruct2;
    public AudioClip instruct3;
    public AudioClip instruct4;
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

       

        //Setting the player based on the initial scene objects
        if (GameObject.FindWithTag("videoPlayer") && instructionPlay == false && vid == false)
        {



            narrator.PlayOneShot(instruct1);
            instructionPlay = true;
            StartCoroutine(SetOne());
        }


        TestOne();
        TestTwo();
        TestThree();
        playing = false;

    }

    IEnumerator SetOne(){

        yield return new WaitForSeconds(1);
        if (playing == false)
        {
            narrator.PlayOneShot(instruct2, 0.7F);
            playing = true;
        }

        yield return new WaitForSeconds(5);
    }

    IEnumerator VideoLength()
    {
        Debug.Log("core");
        yield return new WaitForSeconds(59);
        vid = false;
    }

    void TestOne()
    {
        if (GameObject.FindWithTag("three").activeSelf && completeOne == false)
        {
            answOne = true;
            StartCoroutine(NextInstructions());
            SCORE = SCORE + 1;
        }
        else if (GameObject.FindWithTag("two").activeSelf && completeOne == false)
        {
            answOne = false;
            StartCoroutine(NextInstructions());
        }
        else if (GameObject.FindWithTag("one").activeSelf && completeOne == false)
        {
            answOne = false;
            StartCoroutine(NextInstructions());
        }
    }


    void TestTwo()
    {
        if (GameObject.FindWithTag("two").activeSelf && completeOne == false)
        {
            answTwo = true;
            StartCoroutine(NextInstructions2());
            SCORE = SCORE + 1;
        }
        else if (GameObject.FindWithTag("three").activeSelf && completeOne == false)
        {
            answOne = false;
            StartCoroutine(NextInstructions2());
        }
        else if (GameObject.FindWithTag("one").activeSelf && completeOne == false)
        {
            answOne = false;
            StartCoroutine(NextInstructions2());
        }
    }

    void TestThree()
    {
        if (GameObject.FindWithTag("one").activeSelf && completeOne == false)
        {
            answThree = true;
            StartCoroutine(NextInstructions3());
            SCORE = SCORE + 1;
        }
        else if (GameObject.FindWithTag("two").activeSelf && completeOne == false)
        {
            answOne = false;
            StartCoroutine(NextInstructions3());
        }
        else if (GameObject.FindWithTag("three").activeSelf && completeOne == false)
        {
            answOne = false;
            StartCoroutine(NextInstructions3());
        }
    }


    IEnumerator NextInstructions(){

        if (playing == false)
        {
            narrator.PlayOneShot(instruct3, 0.7F);
            playing = true;
        }
        completeOne = true;
        yield return new WaitForSeconds(5);
       
    }

    IEnumerator NextInstructions2()
    {
        if (playing == false)
        {
            narrator.PlayOneShot(instruct4, 0.7F);
            playing = true;
        }
        completeTwo = true;
        yield return new WaitForSeconds(5);

    }

    IEnumerator NextInstructions3()
    {

        completeThree = true;
        yield return new WaitForSeconds(5);
        FINAL();
    }

    void FINAL()
    {
       
        if (SCORE == 3)
        {
            if (playing == false)
            {
                narrator.PlayOneShot(finalBest);
                playing = true;
            }
        }
        else if (SCORE == 0)
        {
            if (playing == false)
            {
                narrator.PlayOneShot(finalWorst);
                playing = true;
            }
        }
        else
        {
            if (playing == false)
            {
                narrator.PlayOneShot(finalMeh);
                playing = true;
            }
        }
    }


    //All the items to test for are listed below!
    //each of these is looking for specific active items based on the player
    //depending on which items the player activates, the results change
    /*   void TestOne(){
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
        } */

}
