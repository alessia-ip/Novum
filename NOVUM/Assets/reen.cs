using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.GetComponent<GoogleARCore.Examples.AugmentedImage.AugmentedImageVisualizer>().enabled = true;
    }
}
