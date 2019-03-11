using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerTest : MonoBehaviour
{

    public GameObject player;

    private int playerNum;
    public Text countText;



    void Start()
    {
        player = GameObject.FindWithTag("Player");
        player.SetActive(true);
        player.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
    }


    // Update is called once per frame
    void Update()
    {
        playerNum = GameObject.FindGameObjectsWithTag("Player").Length;
        countText.text = "We have " + playerNum.ToString() + " player(s).";
    }
}
