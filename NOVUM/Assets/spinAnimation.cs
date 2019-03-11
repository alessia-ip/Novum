using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinAnimation : MonoBehaviour {

    public int speed;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}
