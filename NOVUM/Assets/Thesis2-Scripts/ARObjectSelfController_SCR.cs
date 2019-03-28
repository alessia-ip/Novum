using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARObjectSelfController_SCR : MonoBehaviour {

    public bool PubTrigger;

    public int answerNum = 0;

    public string PubString = "NULL";

    public AudioSource audioSource_;

    public AudioClip audioClip_;
    public AudioClip audioClip2_;

    private bool triggerOnce = false;

    public int timer = 5;

    public GameControllerAndPubPublisher_SCR gameControllerAndPubPublisher;

    private void OnEnable()
    {
        if (triggerOnce == false){
            if (PubString != "NULL" && PubTrigger == true)
            {
                gameControllerAndPubPublisher.ieCaller(PubString);
            }
            audioSource_.PlayOneShot(audioClip_);
            gameControllerAndPubPublisher.counter = gameControllerAndPubPublisher.counter + answerNum;
            triggerOnce = true;
        } else {
            audioSource_.PlayOneShot(audioClip2_);
        }

    }

    private void Update()
    {
        if(audioSource_.isPlaying != true){

        }
    }

    IEnumerator CountDown(){
        yield return new WaitForSeconds(timer);
        this.gameObject.SetActive(false);
    }
}
