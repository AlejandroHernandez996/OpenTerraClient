using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UnityThread.initUnityThread();
        ClientHandleData.InitializePackets();
        ClientTCP.InitializingNetworking();
        DontDestroyOnLoad(this);
    }

    private void OnApplicationQuit()
    {
        ClientTCP.Disconnect();
    }
}
