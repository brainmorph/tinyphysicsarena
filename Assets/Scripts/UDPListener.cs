using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using Debug = UnityEngine.Debug; // <<<<<< This was needed to differentiate between System.Diagnostics.Debug and Unity's version.

public class UDPListener : MonoBehaviour
{
    public GameObject myPrefab;

    // Start is called once
    void Start()
    {
        Debug.Log("Does this even work?");
        Debug.Log("Does this even work?");
        Debug.Log("Does this even work?");
        Debug.Log("Does this even work?");
        Debug.Log("Does this even work?");
        StartUdpListener();
    }

    // Update is called every frame
    void Update()
    {
        StoreIncomingUdpPackets();
        ParseReceivedUdpPackets();
    }

    private void StartUdpListener()
    {
        Debug.Log($"Entered function {new StackFrame(1, false).GetMethod().Name}");

        mUdpListener = new UdpClient(mListenPort);
        //mUdpListener.Client.Blocking = false;
        mUdpListener.Client.ReceiveTimeout = 1000;

        Debug.Log($"Just created a new UdpClient({mListenPort})");
        
        IPAddress designatedIP = IPAddress.Any;
        mGroupEP = new IPEndPoint(designatedIP, mListenPort);
        Debug.Log($"Just created a new IPEndPoint({designatedIP}, {mListenPort})");

        Debug.Log("At this point I believe both the UdpClient and the IPEndPoint have been set up.");
        Debug.Log($"We have IPEndPoint.Address = {mGroupEP.Address} and IPEndPoint.Port = {mGroupEP.Port}");
    }

    private void ParseReceivedUdpPackets()
    {
        Debug.Log($"IncomingUdpBuffer contains {mIncomingUdpBufferWriteIndex - mIncomingUdpBufferReadIndex} elements.");

        // Parse for packet
        while(mIncomingUdpBufferReadIndex < mIncomingUdpBufferWriteIndex)
        {
            if( mIncomingUdpBufferReadIndex >= mIncomingUdpBufferWriteIndex)
            {
                return; // exit function immediately
            }
            
            // Detected a start of packet
            if(mIncomingUdpBuffer[mIncomingUdpBufferReadIndex] == '`' && !mStartOfPacketDetected)
            {
                mStartOfPacketDetected = true;
            }

            // Detected end of packet
            if(mIncomingUdpBuffer[mIncomingUdpBufferReadIndex] == '\r' && mStartOfPacketDetected)
            {
                mStartOfPacketDetected = false; // reset flag and start looking for packet start again
            }

            // While in the middle of a packet
            if(mStartOfPacketDetected)
            {
                Debug.Log("Character received: " + (char)mIncomingUdpBuffer[mIncomingUdpBufferReadIndex]);
            }

            mIncomingUdpBufferReadIndex++;
        }
    }

    // This function should only be used for storing the UDP packets, no parsing should be done here.
    // This function should run as quickly as possible to ensure the smallest amount of packet loss.
    private void StoreIncomingUdpPackets()
    {
        Debug.Log("Waiting for broadcast");
        try
        {
            byte[] bytes = mUdpListener.Receive(ref mGroupEP);
            System.Buffer.BlockCopy(bytes, 0, mIncomingUdpBuffer, mIncomingUdpBufferWriteIndex, bytes.Length);
            mIncomingUdpBufferWriteIndex += bytes.Length;

            //Debug.Log($" {Encoding.ASCII.GetString(bytes, 0, bytes.Length)}");
        }
        catch(SocketException se)
        {
            Debug.Log(se);
        }
        catch(ArgumentException ee)
        {
            // TODO: revist this.  The most likely reason for entering here is a buffer index overrun.
            Debug.Log(ee);
            mIncomingUdpBufferReadIndex = 0;
            mIncomingUdpBufferWriteIndex = 0; 
        }
    }
    
    private const int mListenPort = 11000;
    
    private UdpClient mUdpListener;
    private IPEndPoint mGroupEP;

    private byte[] mIncomingUdpBuffer = new byte[2 * 1024]; // maybe I should force a max length of bytes here?  Not sure yet.
    private int mIncomingUdpBufferReadIndex;
    private int mIncomingUdpBufferWriteIndex;
    private bool mStartOfPacketDetected = false;
}
