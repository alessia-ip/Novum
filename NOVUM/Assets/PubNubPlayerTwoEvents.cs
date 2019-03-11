using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PubNubAPI;

public class PubNubPlayerTwoEvents : MonoBehaviour {

    //PubNub setup
    PubNub pubnub;
    List<string> listChannelGroups;
    List<string> listChannels;

    //What Activity Has Been Completed. Must be done by BOTH Players
    int thisPlayerActivityCounter = 1;
    int otherPlayerActivityCounter = 2;
    int allPlayerActivityCounter = 1;

    delegate void allActivitiesMethod();

    List<allActivitiesMethod> allActivities;

    bool snitch;

    //This is to log each OTHER's messages, and only each others. Will need 2 versions of this script
    //The second version will need to be for player 2
    string pConf = "Player2";


    //required assets/checks
    bool videoPlay = false;
    bool activityOneOneshot = false;
    bool activityTwoOneshot = false;

    //audios
    public AudioSource instructionSource;
    //One
    public AudioClip activityOneClip;
    public AudioClip activityOneAnswerOne;
    public AudioClip activityOneAnswerTwo;


    void Start()
    {
        //PubNub Setup
        PNConfiguration pnConfiguration = new PNConfiguration();
        pnConfiguration.SubscribeKey = "sub-c-623edbe8-29cc-11e9-9220-327a8a7fb65a";
        pnConfiguration.PublishKey = "pub-c-8bf55402-8177-4404-afec-f9b9257b41d8";
        pnConfiguration.Secure = true;
        //This is the player's name - will either be player 1 or player 2
        pnConfiguration.UUID = pConf;

        pubnub = new PubNub(pnConfiguration);

        //Subscribe to the Novum channel
        pubnub.Subscribe().Channels(new List<string>() { "Novum" }).WithPresence().Execute();

        //Creating a list of events for easy cycling of events
        allActivities = new List<allActivitiesMethod>();

        allActivities.Add(activity1);

        pubnub.SusbcribeCallback += Pubnub_SusbcribeCallback;
    }

    void Update()
    {

        activity1();


        if (GameObject.Find("OptionThree").activeSelf == true)
        {
            Debug.Log("YE");
        }
        if (GameObject.Find("OptionFour").activeSelf == true)
        {
            Debug.Log("HAW");
        }

    }




    void activity1()
    {
        Debug.Log("Next Activity Start");
        if (activityOneOneshot == false)
        {
            instructionSource.PlayOneShot(activityOneClip);
            activityOneOneshot = true;
        }
        if (activityOneOneshot == true && instructionSource.isPlaying == false && activityTwoOneshot == false)
        {
            activity2();
        }
    }

    void activity2()
    {

        Debug.Log("check1");
        if (GameObject.Find("OptionThree").activeSelf == true)
        {
            instructionSource.PlayOneShot(activityOneAnswerOne);
            Debug.Log(thisPlayerActivityCounter);
            pubActivityPublish1();
            activityTwoOneshot = true;
        }


        Debug.Log("check2");


        if (GameObject.Find("OptionFour").activeSelf == true)
        {
            instructionSource.PlayOneShot(activityOneAnswerTwo);
            Debug.Log(thisPlayerActivityCounter);
            pubActivityPublish2();
            activityTwoOneshot = true;

        }
        else
        {

        }
    }


    //A standardized message
    void pubActivityPublish1()
    {

        pubnub.Publish()
       .Channel("Novum")
       .Message("REPORTED")
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
    //A standardized message
    void pubActivityPublish2()
    {

        pubnub.Publish()
       .Channel("Novum")
       .Message("UNREPORTED")
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

    void Pubnub_SusbcribeCallback(object sender, System.EventArgs e)
    {

        SusbcribeEventEventArgs mea = e as SusbcribeEventEventArgs;

        if (mea.MessageResult != null && mea.MessageResult.IssuingClientId.ToString() != pConf)
        {
            Debug.Log("THIS WORKS");

            otherPlayerActivityCounter += 1;


            Debug.Log(thisPlayerActivityCounter);
            Debug.Log(otherPlayerActivityCounter);

        }

    }
}
