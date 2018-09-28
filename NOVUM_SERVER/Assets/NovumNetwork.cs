using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

namespace UnityEngine.Networking
{
    public class NovumNetwork : MonoBehaviour
    {
        //Defines the NetworkManager as this item
        public NetworkManager manager;

        private void Awake()
        {
            //Now we get the correct 'NetworkManager' component
            manager = GetComponent<NetworkManager>();
        }

        // Use this for initialization
        void Start()
        {
            //All this program does is start the server
            manager.StartServer();
        }
    }
}
