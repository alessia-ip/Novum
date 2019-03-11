using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameEvents : NetworkBehaviour {

    //isDirty is a commonly accepted variable to check if a GameObject has been updated yet or not
    //bool isDirty = false;
    bool flagOne = false;

	void Update () {
        if (Input.anyKey)
        {
            firstFlag();
            Debug.Log("CLICK");
        }
        if (flagOne == true) {
            Debug.Log("Flag one is true now!");
        }
	}


    //This is checking for the first flag to be reached!
    void firstFlag() {
        //If the first flag has been reached (in this case, time passing), do the thingu
        //if (Time.realtimeSinceStartup > 2000){
            CmdFirstFlagClear(true);
        //}
    }

    //This sends the command to the server!
    [Command]
    void CmdFirstFlagClear(bool flagOneServ)
    {
        RpcFirstFlagClear(flagOneServ);
        Debug.Log("sent first flag to server");
    }

    //This returns the command back to the client
    [ClientRpc]
    void RpcFirstFlagClear(bool flagOneServ)
    {
        flagOne = flagOneServ;
        Debug.Log("recieved first flag from server");
    }
}
