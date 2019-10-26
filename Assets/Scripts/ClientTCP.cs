using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public static class ClientTCP 
{
    private static TcpClient clientSocket;
    private static NetworkStream clientStream;
    private static byte[] recBuffer;

    public static void InitializingNetworking()
    {
        clientSocket = new TcpClient();
        clientSocket.ReceiveBufferSize = 4096;
        clientSocket.SendBufferSize = 4096;
        recBuffer = new byte[4096 * 2];
        clientSocket.BeginConnect("127.0.0.1", 6969, new AsyncCallback(ClientConnectCallback), clientSocket);
    }

    public static void ClientConnectCallback(IAsyncResult ar)
    {
        clientSocket.EndConnect(ar);

        if(!clientSocket.Connected)
        {
            return;
        }
        clientSocket.NoDelay = true;
        clientStream = clientSocket.GetStream();
        clientStream.BeginRead(recBuffer, 0, 4096 * 2, ReceiveCallback, null);
    }

    private static void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            int length = clientStream.EndRead(ar);

            if(length <= 0)
            {
                return;
            }

            byte[] newBytes = new byte[length];
            Array.Copy(recBuffer, newBytes, length);
            UnityThread.executeInFixedUpdate(() =>
            {
                ClientHandleData.HandleData(newBytes);
            });
            clientStream.BeginRead(recBuffer, 0, 4096 * 2, ReceiveCallback, null);
        }
        catch (Exception)
        {
            return;
        }

    }
    public static void SendData(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteInt((data.GetUpperBound(0) - data.GetLowerBound(0) + 1));
        buffer.WriteBytes(data);
        clientStream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null);

        buffer.Dispose();
    }

    public static void Disconnect()
    {
        clientSocket.Close();
    }
}
