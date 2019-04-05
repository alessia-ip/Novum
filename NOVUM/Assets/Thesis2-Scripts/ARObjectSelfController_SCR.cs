using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARObjectSelfController_SCR : MonoBehaviour {

    public bool PubTrigger;

    public int answerNum = 0;

    public string PubString = "NULL";

    private AudioSource audioSource_;

    public AudioClip audioClip_;
    public AudioClip audioClip2_;

    public int qNum = 0;

    public int timer = 5;

    private GameControllerAndPubPublisher_SCR gameControllerAndPubPublisher;

    private void Start()
    {
        gameControllerAndPubPublisher = GameObject.FindWithTag("GameController").GetComponent<GameControllerAndPubPublisher_SCR>();    
    }

    private void OnEnable()
    {
        audioSource_ = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();


        gameControllerAndPubPublisher = GameObject.FindWithTag("GameController").GetComponent<GameControllerAndPubPublisher_SCR>();
        if (qNum == 1 && gameControllerAndPubPublisher.q1answered == false)
        {
            if (PubString != "NULL" && PubTrigger == true && gameControllerAndPubPublisher.q1answered == false)
            {
                gameControllerAndPubPublisher.ieCaller(PubString);
            }
            audioSource_.PlayOneShot(audioClip_);
            gameControllerAndPubPublisher.counter = gameControllerAndPubPublisher.counter + answerNum;
            gameControllerAndPubPublisher.q1answered = true;

        } else if (qNum == 2 && gameControllerAndPubPublisher.q2answered == false)
        {
            if (PubString != "NULL" && PubTrigger == true && gameControllerAndPubPublisher.q2answered == false)
            {
                gameControllerAndPubPublisher.ieCaller(PubString);
            }
            audioSource_.PlayOneShot(audioClip_);
            gameControllerAndPubPublisher.counter = gameControllerAndPubPublisher.counter + answerNum;
            gameControllerAndPubPublisher.q2answered = true;

        }
        else {
            if(audioSource_.isPlaying == false){
                audioSource_.PlayOneShot(audioClip2_);
            }
        }

    }

    private void Update()
    {
        if(audioSource_.isPlaying != true){
            StartCoroutine(CountDown());
        }
    }

    IEnumerator CountDown(){
        yield return new WaitForSeconds(timer);
        this.gameObject.SetActive(false);
    }
}
