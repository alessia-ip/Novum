using System;
using System.ComponentModel;
using UnityEngine.SceneManagement;

#if ENABLE_UNET

namespace UnityEngine.Networking
{
    [AddComponentMenu("Network/NetworkManagerHUD")]
    [RequireComponent(typeof(NetworkManager))]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class Main_HUD : MonoBehaviour
    {
        public NetworkManager manager;
        [SerializeField] public bool showGUI = true;
        [SerializeField] public int offsetX;
        [SerializeField] public int offsetY;

        // Runtime variable
        bool m_ShowServer;

        public GUIStyle mainStyle;

        void Awake()
        {
            manager = GetComponent<NetworkManager>();
        }

        void Update()
        {
            if (!showGUI)
                return;

            if (!manager.IsClientConnected() && !NetworkServer.active && manager.matchMaker == null)
            {
                if (UnityEngine.Application.platform != RuntimePlatform.WebGLPlayer)
                {
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        manager.StartServer();
                    }
                    if (Input.GetKeyDown(KeyCode.H))
                    {
                        manager.StartHost();
                    }
                }
                if (Input.GetKeyDown(KeyCode.C))
                {
                    manager.StartClient();
                }
            }
            if (NetworkServer.active && manager.IsClientConnected())
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    manager.StopHost();
                }
            }
        }

        void OnGUI()
        {
            if (!showGUI)
                return;


            int xpos = Screen.width / 3;
            int ypos = Screen.height / 3;
            // int xpos = 10 + offsetX;
            // int ypos = 40 + offsetY;
            const int spacing = 100;

            bool noConnection = (manager.client == null || manager.client.connection == null ||
                                 manager.client.connection.connectionId == -1);

            if (!manager.IsClientConnected() && !NetworkServer.active && manager.matchMaker == null)
            {
                if (noConnection)
                {

                    if (GUI.Button(new Rect(xpos, ypos, 200, 300), "LAN Client(C)", mainStyle))
                    {
                        manager.StartClient();
                    }

                    manager.networkAddress = GUI.TextField(new Rect(xpos + 400, ypos, 200, 300), manager.networkAddress, mainStyle);
                    ypos += spacing;

                }
                else
                {
                    GUI.Label(new Rect(xpos, ypos, 200, 20), "Connecting to " + manager.networkAddress + ":" + manager.networkPort + "..");
                    ypos += spacing;


                    if (GUI.Button(new Rect(xpos, ypos, 200, 20), "Cancel Connection Attempt", mainStyle))
                    {
                        manager.StopClient();
                    }
                }
            }
            else
            {
                if (NetworkServer.active)
                {
                    string serverMsg = "Server: port=" + manager.networkPort;
                    if (manager.useWebSockets)
                    {
                        serverMsg += " (Using WebSockets)";
                    }
                    GUI.Label(new Rect(xpos, ypos, 300, 20), serverMsg);
                    ypos += spacing;
                }
                if (manager.IsClientConnected())
                {
                    GUI.Label(new Rect(xpos, ypos, 300, 20), "Client: address=" + manager.networkAddress + " port=" + manager.networkPort);
                    ypos += spacing;
                }
            }

            if (manager.IsClientConnected() && !ClientScene.ready)
            {
                if (GUI.Button(new Rect(xpos, ypos, 200, 300), "Client Ready", mainStyle))
                {
                    ClientScene.Ready(manager.client.connection);
                    SceneManager.LoadScene("ConnectionScene");

                    if (ClientScene.localPlayers.Count == 0)
                    {
                        ClientScene.AddPlayer(0);
                    }
                }
                ypos += spacing;
            }

            if (NetworkServer.active || manager.IsClientConnected())
            {

                /*if (GUI.Button(new Rect(xpos, ypos, 200, 300), "Stop (X)", mainStyle))
                {
                    manager.StopHost();
                }
                ypos += spacing;*/
            }

            if (!NetworkServer.active && !manager.IsClientConnected() && noConnection)
            {
                ypos += 10;

                if (UnityEngine.Application.platform == RuntimePlatform.WebGLPlayer)
                {
                    GUI.Box(new Rect(xpos - 5, ypos, 220, 25), "(WebGL cannot use Match Maker)");
                    return;
                }


            }
        }
    }
}

#endif //ENABLE_UNET