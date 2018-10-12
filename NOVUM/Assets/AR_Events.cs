using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Networking;


public class AR_Events : NetworkBehaviour {

    bool isPlayer;

	void Start () {
        //Is this object a local player object or not
        if (!isLocalPlayer){
            isPlayer = false;
        } else {
            isPlayer = true;
        }
	}
	
	void Update () {
        //only run local player commands if it is the player. Only run server commands if it is the server
        if (isPlayer == true){

        } else if (isPlayer == false){

        }
	}




}
