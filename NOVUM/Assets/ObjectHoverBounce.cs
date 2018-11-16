using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHoverBounce : MonoBehaviour {

    int counter = 0;

	void Update () {
        if (counter < 200){
            transform.Translate(0,(Time.deltaTime/3), 0);
            counter++;
        } else if (counter >= 200 && counter < 400){
            transform.Translate(0, -(Time.deltaTime / 3), 0);
            counter++;
        } else {
            counter = 0;
        }
    }
}
