using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PubNubAPI;
using UnityEngine.UI;
using UnityEngine.iOS;

public class GameControllerAndPubPublisher_SCR : MonoBehaviour {

    //Pubnub setup variables
    PubNub pubnub;
    List<string> listChannelGroups;
    List<string> listChannels;
    public GameObject answered;
    private Text answer;

    //PubNub player number - set this up in editor
    public string playerNumber;


    // ___________________________________________________________________

    //Game variables

    //question one answer. If they answered with option 1, it's a 1. If option 2, it's a 2. 0 is for no answer yet.
    [HideInInspector]
    public int questionOne = 0;

    [HideInInspector]
    public int questionTwo = 0;

    [HideInInspector]
    public int touchScreen = 0;

    [HideInInspector]
    public bool snitchedOn;

    // ___________________________________________________________________

    //Non-object game audio. This is audio not attached to a specific game object

    public AudioSource mainAudioSource;

    public AudioClip doNotTouch;



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
        if(Input.touchCount > 0){
            Handheld.Vibrate();
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
        }

    }


}
