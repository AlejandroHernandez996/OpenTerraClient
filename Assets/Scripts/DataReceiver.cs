using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataReceiver
{
    public enum ServerPackets
    {
        SWelcomeMessage = 1, SMatchMade = 2, SStartGame = 3
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

    public static void HandleStartGame(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);

        int packetID = buffer.ReadInt();
        int size = buffer.ReadInt();

        Debug.Log("Game Has Started!\nDrawing " + size + " cards.");
        for (int x = 0;x < size; x++)
        {
            Debug.Log("Card ID: " + buffer.ReadString());
        }

        buffer.Dispose();

    }
}
