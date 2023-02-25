using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDPListener : MonoBehaviour
{
    // Start is called once
    void Start()
    {
        Debug.Log("Does this even work?");
        Debug.Log("Does this even work?");
        Debug.Log("Does this even work?");
        Debug.Log("Does this even work?");
        Debug.Log("Does this even work?");
        StartListener();
    }

    private void StartListener()
    {
        Debug.Log("Entered function StartListener()");

        mUdpListener = new UdpClient(mListenPort);
        //mUdpListener.Client.Blocking = false;
        mUdpListener.Client.ReceiveTimeout = 1000;

        Debug.Log($"Just created a new UdpClient({mListenPort})");
        
        IPAddress designatedIP = IPAddress.Any;
        mGroupEP = new IPEndPoint(designatedIP, mListenPort);
        Debug.Log($"Just created a new IPEndPoint({designatedIP}, {mListenPort})");

        Debug.Log("At this point I believe both the UdpClient and the IPEndPoint have been set up.");
        Debug.Log($"We have IPEndPoint.Address = {mGroupEP.Address} and IPEndPoint.Port = {mGroupEP.Port}");

        // try
        // {
        //     while (true)
        //     {
        //         Debug.Log("Waiting for broadcast");
        //         byte[] bytes = Encoding.ASCII.GetBytes("lol"); //listener.Receive(ref groupEP);

        //         Debug.Log($"Received broadcast from {groupEP} :");
        //         Debug.Log($" {Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");
        //     }
        // }
        // catch (SocketException e)
        // {
        //     Debug.Log(e);
        // }
        // finally
        // {
        //     listener.Close();
        // }
    }

    void Update()
    {
        Debug.Log("Waiting for broadcast");
        try
        {
            byte[] bytes = mUdpListener.Receive(ref mGroupEP);
            Debug.Log($"Received broadcast from {mGroupEP} :");
            Debug.Log($" {Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");
        }
        catch(SocketException se)
        {
            Debug.Log(se);
        }
        // finally
        // {
        //     mUdpListener.Close();
        // }
    }
    
    private const int mListenPort = 11000;
    
    private UdpClient mUdpListener;
    private IPEndPoint mGroupEP;
}
