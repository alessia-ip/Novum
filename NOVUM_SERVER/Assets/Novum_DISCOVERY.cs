#if ENABLE_UNET
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.Networking
{
    public struct NetworkBroadcastResult
    {
        public string serverAddress;
        public byte[] broadcastData;
    }

    [DisallowMultipleComponent]
    [AddComponentMenu("Network/NetworkDiscovery")]

    public class Novum_DISCOVERY : MonoBehaviour
    {
        const int k_MaxBroadcastMsgSize = 1024;

        // config data
        [SerializeField]
        int m_BroadcastPort = 47777;

        [SerializeField]
        int m_BroadcastKey = 2222;

        [SerializeField]
        int m_BroadcastVersion = 1;

        [SerializeField]
        int m_BroadcastSubVersion = 1;

        [SerializeField]
        int m_BroadcastInterval = 1000;

        [SerializeField]
        bool m_UseNetworkManager = true;

        [SerializeField]
        string m_BroadcastData = "NOVUM";


        // runtime data
        int m_HostId = -1;
        bool m_Running;

        bool m_IsServer;
        bool m_IsClient;

        byte[] m_MsgOutBuffer;
        byte[] m_MsgInBuffer;
        HostTopology m_DefaultTopology;
        Dictionary<string, NetworkBroadcastResult> m_BroadcastsReceived;

        public int broadcastPort
        {
            get { return m_BroadcastPort; }
            set { m_BroadcastPort = value; }
        }

        public int broadcastKey
        {
            get { return m_BroadcastKey; }
            set { m_BroadcastKey = value; }
        }

        public int broadcastVersion
        {
            get { return m_BroadcastVersion; }
            set { m_BroadcastVersion = value; }
        }

        public int broadcastSubVersion
        {
            get { return m_BroadcastSubVersion; }
            set { m_BroadcastSubVersion = value; }
        }

        public int broadcastInterval
        {
            get { return m_BroadcastInterval; }
            set { m_BroadcastInterval = value; }
        }

        public bool useNetworkManager
        {
            get { return m_UseNetworkManager; }
            set { m_UseNetworkManager = value; }
        }

        public string broadcastData
        {
            get { return m_BroadcastData; }
            set
            {
                m_BroadcastData = value;
                m_MsgOutBuffer = StringToBytes(m_BroadcastData);
                if (m_UseNetworkManager)
                {
                    if (LogFilter.logWarn) { Debug.LogWarning("NetworkDiscovery broadcast data changed while using NetworkManager. This can prevent clients from finding the server. The format of the broadcast data must be 'NetworkManager:IPAddress:Port'."); }
                }
            }
        }


        public int hostId
        {
            get { return m_HostId; }
            set { m_HostId = value; }
        }

        public bool running
        {
            get { return m_Running; }
            set { m_Running = value; }
        }

        public bool isServer
        {
            get { return m_IsServer; }
            set { m_IsServer = value; }
        }

        public bool isClient
        {
            get { return m_IsClient; }
            set { m_IsClient = value; }
        }

        public Dictionary<string, NetworkBroadcastResult> broadcastsReceived
        {
            get { return m_BroadcastsReceived; }
        }

        static byte[] StringToBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string BytesToString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public bool Initialize()
        {
            if (m_BroadcastData.Length >= k_MaxBroadcastMsgSize)
            {
                if (LogFilter.logError) { Debug.LogError("NetworkDiscovery Initialize - data too large. max is " + k_MaxBroadcastMsgSize); }
                return false;
            }

            if (!NetworkTransport.IsStarted)
            {
                NetworkTransport.Init();
            }

            if (m_UseNetworkManager && NetworkManager.singleton != null)
            {
                m_BroadcastData = "NetworkManager:" + NetworkManager.singleton.networkAddress + ":" + NetworkManager.singleton.networkPort;
                if (LogFilter.logInfo) { Debug.Log("NetwrokDiscovery set broadbast data to:" + m_BroadcastData); }
            }

            m_MsgOutBuffer = StringToBytes(m_BroadcastData);
            m_MsgInBuffer = new byte[k_MaxBroadcastMsgSize];
            m_BroadcastsReceived = new Dictionary<string, NetworkBroadcastResult>();

            ConnectionConfig cc = new ConnectionConfig();
            cc.AddChannel(QosType.Unreliable);
            m_DefaultTopology = new HostTopology(cc, 1);

            if (m_IsServer)
                StartAsServer();

            if (m_IsClient)
                StartAsClient();

            return true;
        }

        // listen for broadcasts
        public bool StartAsClient()
        {
            if (m_HostId != -1 || m_Running)
            {
                if (LogFilter.logWarn) { Debug.LogWarning("NetworkDiscovery StartAsClient already started"); }
                return false;
            }

            m_HostId = NetworkTransport.AddHost(m_DefaultTopology, m_BroadcastPort);
            if (m_HostId == -1)
            {
                if (LogFilter.logError) { Debug.LogError("NetworkDiscovery StartAsClient - addHost failed"); }
                return false;
            }

            byte error;
            NetworkTransport.SetBroadcastCredentials(m_HostId, m_BroadcastKey, m_BroadcastVersion, m_BroadcastSubVersion, out error);

            m_Running = true;
            m_IsClient = true;
            if (LogFilter.logDebug) { Debug.Log("StartAsClient Discovery listening"); }
            return true;
        }

        // perform actual broadcasts
        public bool StartAsServer()
        {
            if (m_HostId != -1 || m_Running)
            {
                if (LogFilter.logWarn) { Debug.LogWarning("NetworkDiscovery StartAsServer already started"); }
                return false;
            }

            m_HostId = NetworkTransport.AddHost(m_DefaultTopology, 0);
            if (m_HostId == -1)
            {
                if (LogFilter.logError) { Debug.LogError("NetworkDiscovery StartAsServer - addHost failed"); }
                return false;
            }

            byte err;
            if (!NetworkTransport.StartBroadcastDiscovery(m_HostId, m_BroadcastPort, m_BroadcastKey, m_BroadcastVersion, m_BroadcastSubVersion, m_MsgOutBuffer, m_MsgOutBuffer.Length, m_BroadcastInterval, out err))
            {
                if (LogFilter.logError) { Debug.LogError("NetworkDiscovery StartBroadcast failed err: " + err); }
                return false;
            }

            m_Running = true;
            m_IsServer = true;
            if (LogFilter.logDebug) { Debug.Log("StartAsServer Discovery broadcasting"); }
            DontDestroyOnLoad(gameObject);
            return true;
        }

        public void StopBroadcast()
        {
            if (m_HostId == -1)
            {
                if (LogFilter.logError) { Debug.LogError("NetworkDiscovery StopBroadcast not initialized"); }
                return;
            }

            if (!m_Running)
            {
                Debug.LogWarning("NetworkDiscovery StopBroadcast not started");
                return;
            }
            if (m_IsServer)
            {
                NetworkTransport.StopBroadcastDiscovery();
            }

            NetworkTransport.RemoveHost(m_HostId);
            m_HostId = -1;
            m_Running = false;
            m_IsServer = false;
            m_IsClient = false;
            m_MsgInBuffer = null;
            m_BroadcastsReceived = null;
            if (LogFilter.logDebug) { Debug.Log("Stopped Discovery broadcasting"); }
        }

        void Update()
        {
            if (m_HostId == -1)
                return;

            if (m_IsServer)
                return;

            NetworkEventType networkEvent;
            do
            {
                int connectionId;
                int channelId;
                int receivedSize;
                byte error;
                networkEvent = NetworkTransport.ReceiveFromHost(m_HostId, out connectionId, out channelId, m_MsgInBuffer, k_MaxBroadcastMsgSize, out receivedSize, out error);

                if (networkEvent == NetworkEventType.BroadcastEvent)
                {
                    NetworkTransport.GetBroadcastConnectionMessage(m_HostId, m_MsgInBuffer, k_MaxBroadcastMsgSize, out receivedSize, out error);

                    string senderAddr;
                    int senderPort;
                    NetworkTransport.GetBroadcastConnectionInfo(m_HostId, out senderAddr, out senderPort, out error);

                    var recv = new NetworkBroadcastResult();
                    recv.serverAddress = senderAddr;
                    recv.broadcastData = new byte[receivedSize];
                    Buffer.BlockCopy(m_MsgInBuffer, 0, recv.broadcastData, 0, receivedSize);
                    m_BroadcastsReceived[senderAddr] = recv;

                    OnReceivedBroadcast(senderAddr, BytesToString(m_MsgInBuffer));
                }
            }
            while (networkEvent != NetworkEventType.Nothing);

            Initialize();
            StartAsServer();
        }

        void OnDestroy()
        {
            if (m_IsServer && m_Running && m_HostId != -1)
            {
                NetworkTransport.StopBroadcastDiscovery();
                NetworkTransport.RemoveHost(m_HostId);
            }

            if (m_IsClient && m_Running && m_HostId != -1)
            {
                NetworkTransport.RemoveHost(m_HostId);
            }
        }

        public virtual void OnReceivedBroadcast(string fromAddress, string data)
        {
            //Debug.Log("Got broadcast from [" + fromAddress + "] " + data);
        }





    }
}
#endif