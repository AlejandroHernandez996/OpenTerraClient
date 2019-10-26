using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataReceiver
{
    public enum ServerPackets
    {
        SWelcomeMessage = 1, SMatchMade = 2
    }
    public static void HandleWelcomeMsg(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);

        int packetID = buffer.ReadInt();
        string msg = buffer.ReadString();
        buffer.Dispose();

        DataSender.SendHelloServer();

        Debug.Log(msg);
    }

    public static void HandleMatchMade(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);

        int packetID = buffer.ReadInt();
        string msg = buffer.ReadString();
        buffer.Dispose();

        Debug.Log("Facing: " + msg);
    }
}
