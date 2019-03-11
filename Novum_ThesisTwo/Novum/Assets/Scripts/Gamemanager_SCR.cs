using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PubNubAPI;

public class Gamemanager_SCR : MonoBehaviour
{

    //player one

    //PubNub setup
    PubNub pubnub;
    List<string> listChannelGroups;
    List<string> listChannels;

    //This is to log each OTHER's messages, and only each others. Will need 2 versions of this script
    //The second version will need to be for player 2
    public string pConf;

    public bool started = false;

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


        pubnub.SusbcribeCallback += Pubnub_SusbcribeCallback;
    }

    public void pubPublish(string pubText){
         pubnub.Publish()
           .Channel("Novum")
           .Message(pubText)
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
        }
    }



}
