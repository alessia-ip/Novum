using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PubNubAPI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerAndPubPublisher_SCR : MonoBehaviour {

    //Pubnub setup variables
    PubNub pubnub;
    List<string> listChannelGroups;
    List<string> listChannels;

    //PubNub player number - set this up in editor
    public string playerNumber;


    // ___________________________________________________________________

    //Game variables

    //bad answer = 3 good answer = 2
    public int counter = 0;

    // best - 4
    // second - 5
    // worst = 6

    [HideInInspector]
    public bool snitchedOn;

    public AudioClip doubt;
    public bool heardDoubt = false;

    // ___________________________________________________________________

    //Non-object game audio. This is audio not attached to a specific game object

    public AudioSource mainAudioSource;

    public AudioClip doNotTouch;

    public AudioClip waitforend;

    //perfect answers, and not snitched on
    public AudioClip ending_one;

    //wrong answers or snitched on
    public AudioClip ending_two;

    public bool q1answered = false;

    public bool q2answered = false;

    private bool otherFinished;
    private bool heardWait;


    private bool heardFinalEnding = false;


    private bool finishInvoke = false;


    public bool instructOneHeard = false;



    // ___________________________________________________________________

    public GameObject No;
    public GameObject Win;
    public GameObject frown;

    void Start () {

        //PubNub Setup
        PNConfiguration pnConfiguration = new PNConfiguration();
        pnConfiguration.SubscribeKey = "sub-c-623edbe8-29cc-11e9-9220-327a8a7fb65a";
        pnConfiguration.PublishKey = "pub-c-8bf55402-8177-4404-afec-f9b9257b41d8";
        pnConfiguration.Secure = true;

        //This is the player's name - will either be player 1 or player 2
        pnConfiguration.UUID = playerNumber;

        pubnub = new PubNub(pnConfiguration);

        //Subscribe to the Novum channel
        pubnub.Subscribe().Channels(new List<string>() { "Novum" }).WithPresence().Execute();

        //Get callback messages  - see function
        pubnub.SusbcribeCallback += Pubnub_SusbcribeCallback;

    }
	

	void Update () {
        //If the screen is touched at any point, do this
        if(Input.touchCount > 0 && mainAudioSource.isPlaying == false){
            Handheld.Vibrate();
            mainAudioSource.PlayOneShot(doNotTouch);
        }

        if (mainAudioSource.isPlaying == false)
        {
            if (snitchedOn == true && heardDoubt == false)
            {
                mainAudioSource.PlayOneShot(doubt);
                frown.SetActive(true);
                heardDoubt = true;
            }
        }

        if(counter >= 4){
            if (finishInvoke == false){
                ieCaller("Finished");
                finishInvoke = true;
            }
            if(mainAudioSource.isPlaying == false){
                if (otherFinished == false && heardWait == false)
                {
                    mainAudioSource.PlayOneShot(waitforend);
                    heardWait = true;
                }
                else if (otherFinished == true && heardFinalEnding == false)
                {
                    if (snitchedOn == true || counter > 4)
                    {
                        mainAudioSource.PlayOneShot(ending_two);
                        heardFinalEnding = true;
                        StartCoroutine(EndingTimer());
                        No.SetActive(true);
                    }
                    else
                    {
                        mainAudioSource.PlayOneShot(ending_one);
                        heardFinalEnding = true;
                        StartCoroutine(EndingTimer());
                        Win.SetActive(true);
                    }
                }
            }
            
        }
	}



    void Pubnub_SusbcribeCallback(object sender, System.EventArgs e)
    {

        SusbcribeEventEventArgs mea = e as SusbcribeEventEventArgs;

        if (mea.MessageResult != null && mea.MessageResult.IssuingClientId.ToString() != playerNumber)
        {
            string RESULT = mea.MessageResult.Payload.ToString();
            if (RESULT == "REPORTED")
            {
                snitchedOn = true;
            }
            if (RESULT == "UNREPORTED")
            {
                snitchedOn = false;
            }
            if (RESULT == "Finished")
            {
                otherFinished = true;
            }
        }

    }


    public void ieCaller(string pubNubMessage){
        Debug.Log("Invoked");
        StartCoroutine(SendingPubMessage(pubNubMessage));
    }

    private IEnumerator SendingPubMessage(string PubMessage)
    {
        Debug.Log("MessageSent");
        pubnub.SusbcribeCallback += Pubnub_SusbcribeCallback;
        print(Time.time);
        yield return new WaitForSeconds(1);
        print(Time.time);
        pubnub.Publish()
       .Channel("Novum")
     .Message(PubMessage)
       .Async((result, status) =>
       {
           if (!status.Error)
           {
               Debug.Log(string.Format("Publish Timetoken: {0}", result.Timetoken));
           }
           else
           {
               Debug.Log(status.Error);
               Debug.Log(status.ErrorData.Info);
           }
       });

    }

    IEnumerator EndingTimer(){
        yield return new WaitForSeconds(10);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
