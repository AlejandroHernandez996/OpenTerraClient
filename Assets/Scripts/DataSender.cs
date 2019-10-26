using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClientPackets
{
    CHelloServer =1, CNameDeckServer =2
}
public class DataSender
{
    public static void SendHelloServer()
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteInt((int)ClientPackets.CHelloServer);
        buffer.WriteString("I'am now connected to you!");
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }

    public static void SendNameDeckServer(string name, string deck)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteInt((int)ClientPackets.CNameDeckServer);
        buffer.WriteString(name);
        buffer.WriteString(deck);
        Debug.Log("Sending Name and Deck to server...");
        ClientTCP.SendData(buffer.ToArray());
        buffer.Dispose();
    }
}
