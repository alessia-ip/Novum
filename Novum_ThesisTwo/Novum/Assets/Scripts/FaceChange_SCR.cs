using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceChange_SCR : MonoBehaviour
{

    public Texture RegularTexture;
    public Texture BlinkingTexture;
    public Texture WinkingTexture;

    private Renderer Head;
   
    void Start()
    {
        Head = this.gameObject.GetComponent<Renderer>();
        StartCoroutine(Blinking());
    }

    IEnumerator Blinking(){
        int i = Random.Range(3, 7);
        yield return new WaitForSeconds(i);
        if (i == 5){
            Head.material.SetTexture("_MainTex", WinkingTexture);
        } else {
            Head.material.SetTexture("_MainTex", BlinkingTexture);
        }
        yield return new WaitForSeconds(0.5f);
        Head.material.SetTexture("_MainTex", RegularTexture);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Blinking());
    }

}
