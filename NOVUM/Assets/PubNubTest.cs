using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PubNubAPI;

public class PubNubTest : MonoBehaviour
{


    PubNub pubnub;
    List<string> listChannelGroups;
    List<string> listChannels;

    string pConf = "Player2";

    // Use this for initialization
    void Start()
    {
        PNConfiguration pnConfiguration = new PNConfiguration();
        pnConfiguration.SubscribeKey = "sub-c-623edbe8-29cc-11e9-9220-327a8a7fb65a";
        pnConfiguration.PublishKey = "pub-c-8bf55402-8177-4404-afec-f9b9257b41d8";
        pnConfiguration.Secure = true;
        pnConfiguration.UUID = pConf;

        pubnub = new PubNub(pnConfiguration);

        pubnub.Subscribe().Channels(new List<string>() { "Novum" }).WithPresence().Execute();

        StartCoroutine(Example());


    }

  

    IEnumerator Example()
    {

        pubnub.SusbcribeCallback += Pubnub_SusbcribeCallback;
        print(Time.time);
        yield return new WaitForSeconds(1);
        print(Time.time);
        pubnub.Publish()
       .Channel("Novum")
       .Message("YOTE")
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
        Debug.Log("PUB PRINTED");
        yield return new WaitForSeconds(1);


    }





    void Pubnub_SusbcribeCallback(object sender, System.EventArgs e)
    {
        SusbcribeEventEventArgs mea = e as SusbcribeEventEventArgs;


        if (mea.MessageResult != null)
        {
            //Debug.Log("SusbcribeCallback in message" + mea.MessageResult.Channel + mea.MessageResult.Payload);
            //Debug.Log(mea.MessageResult.Payload);
            string RESULT = mea.MessageResult.Payload.ToString();
            if (RESULT == "YOTE"){
                Debug.Log("YEET");
            }
        }

    }

    

}