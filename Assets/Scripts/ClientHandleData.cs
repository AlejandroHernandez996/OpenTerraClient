using System.Collections;
using System.Collections.Generic;
using static DataReceiver;
using UnityEngine;

public class ClientHandleData
{
    private static ByteBuffer playerBuffer;
    public delegate void Packet(byte[] data);
    public static Dictionary<int, Packet> packets = new Dictionary<int, Packet>();

    public static void InitializePackets()
    {
        packets.Add((int)ServerPackets.SWelcomeMessage, DataReceiver.HandleWelcomeMsg);
        packets.Add((int)ServerPackets.SMatchMade, DataReceiver.HandleMatchMade);
        packets.Add((int)ServerPackets.SStartGame, DataReceiver.HandleStartGame);
    }
    public static void HandleData(byte[] data)
    {
        byte[] buffer = (byte[])data.Clone();
        int packetLength = 0;

        if (playerBuffer == null)
        {
            playerBuffer = new ByteBuffer();
        }

        playerBuffer.WriteBytes(buffer);

        if (playerBuffer.GetBufferSize() == 0)
        {

            playerBuffer.ClearBuffer();
            return;

        }

        if (playerBuffer.GetBufferSize() >= 4)
        {
            packetLength = playerBuffer.ReadInt(false);
            if (packetLength <= 0)
            {
                playerBuffer.ClearBuffer();
                return;
            }
        }

        while (packetLength > 0 & packetLength <= playerBuffer.GetBufferSize() - 4)
        {

            if (packetLength <= playerBuffer.GetBufferSize() - 4)
            {
                playerBuffer.ReadInt();
                data = playerBuffer.ReadBytes(packetLength);
                HandleDataPackets(data);
            }

            packetLength = 0;
            if (playerBuffer.GetRemainingBufferLength() >= 4)
            {
                packetLength = playerBuffer.ReadInt(false);
                if (packetLength <= 0)
                {
                    playerBuffer.ClearBuffer();
                    return;
                }
            }
        }
    }
    private static void HandleDataPackets(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);
        int packetID = buffer.ReadInt();
        buffer.Dispose();
        if (packets.TryGetValue(packetID, out Packet packet))
        {
            packet.Invoke(data);
        }
    }

}
