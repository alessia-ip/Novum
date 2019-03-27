using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtistIntro_SCR : MonoBehaviour {

    //Audio
    public AudioSource thisAudio;

    public AudioClip FirstTimeInstruct;
    public AudioClip RepeatInstruct;

    //Texture Set
    public Texture Reg;
    public Texture Blinking;
    public Texture Winking;
    public Texture MouthClosed;

    //Texture Placement
    public GameObject ArtistHead;
    private Renderer headRend;

    //Variables
    bool executedOne = false;

	void Start () {
        headRend = ArtistHead.GetComponent<Renderer>();
	}


    private void OnEnable()
    {
        if (executedOne == false){
            thisAudio.PlayOneShot(FirstTimeInstruct);
            executedOne = true;
        } else {
            thisAudio.PlayOneShot(RepeatInstruct);
        }

        StartCoroutine(Talking());
    }

    IEnumerator Talking(){
        yield return new WaitForSeconds(0.5f);
        headRend.material.mainTexture = MouthClosed;
        yield return new WaitForSeconds(0.5f);

        int i = Random.Range(0, 10);

        if (i > 1){
            headRend.material.mainTexture = Reg;
        } else if (0.1 <= i && i <= 1){
            headRend.material.mainTexture = Blinking;
        } else {
            headRend.material.mainTexture = Winking;
        }

        if (thisAudio.isPlaying == true){
            StartCoroutine(Talking());
        } else {
            StartCoroutine(Regular());
        }
    }

    IEnumerator Regular(){
        yield return new WaitForSeconds(0.5f);
        int i = Random.Range(0, 10);

        if (i > 1)
        {
            headRend.material.mainTexture = Reg;
        }
        else if (0.1 <= i && i <= 1)
        {
            headRend.material.mainTexture = Blinking;
        }
        else
        {
            headRend.material.mainTexture = Winking;
        }
    }

}
