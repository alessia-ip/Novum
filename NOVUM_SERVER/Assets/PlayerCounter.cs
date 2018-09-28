using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCounter : MonoBehaviour {

    private int playerNum;
    public Text countText;

    // Update is called once per frame
    void Update () {
        playerNum = GameObject.FindGameObjectsWithTag("Player").Length;
        countText.text = "We have " + playerNum.ToString() + " player(s).";
    }
}
