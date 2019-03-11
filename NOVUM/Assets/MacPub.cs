using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PubNubAPI;
using UnityEngine.UI;

public class MacPub : MonoBehaviour {

    PubNub pubnub;
    List<string> listChannelGroups;
    List<string> listChannels;

    string pConf = "Listener";

    public GameObject answered;

    private Text answer;

    

    int reports = 0;
    int noncompliance = 0;

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

        answer = answered.GetComponent<Text>();
    }


    private void Update()
    {
        Debug.Log("WORK");
        answer.text = "There have been " + reports + " reports made and " + noncompliance + " nonreports.";
    }


    IEnumerator Example()
    {
        pubnub.SusbcribeCallback += Pubnub_SusbcribeCallback;
        yield return new WaitForSeconds(1);
    }





    void Pubnub_SusbcribeCallback(object sender, System.EventArgs e)
    {
        SusbcribeEventEventArgs mea = e as SusbcribeEventEventArgs;
        if (mea.MessageResult != null && mea.MessageResult.IssuingClientId.ToString() != pConf)
        {
            string RESULT = mea.MessageResult.Payload.ToString();
            if (RESULT == "REPORTED")
            {
                reports += 1;
            }
            if (RESULT == "UNREPORTED")
            {
                noncompliance += 1;
            }
        }
    }
}
