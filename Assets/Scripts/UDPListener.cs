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
        SaveIncomingUdpPackets();
        ParseReceivedUdpPackets();
    }

    public List<byte> NextMessage()
    {
        if(mReceivedMessagesList.Count != 0)
        {
            List<byte> t = mReceivedMessagesList[0];
            mReceivedMessagesList.Remove(t);

            return t;
        }
        else
        {
            return null;
        }
    }

    public int CountMessages()
    {
        return mReceivedMessagesList.Count;
    }

    private void StartUdpListener()
    {
        //Debug.Log($"Entered function {new StackFrame(1, false).GetMethod().Name}");

        // Configure our udp listener object
        mUdpListener = new UdpClient(mListenPort);
        //mUdpListener.Client.Blocking = false;
        mUdpListener.Client.ReceiveTimeout = 1000;

        Debug.Log($"Created a new UdpClient({mListenPort})");
        
        IPAddress designatedIP = IPAddress.Any;
        mGroupEP = new IPEndPoint(designatedIP, mListenPort);
        Debug.Log($"Created a new IPEndPoint({designatedIP}, {mListenPort})");

        Debug.Log("At this point both the UdpClient and the IPEndPoint have been set up.");
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

                // Start keeping track of the incoming bytes in a new buffer
                mIncomingMessageBuffer = new List<byte>();
            }

            // Detected end of packet
            if(mIncomingUdpBuffer[mIncomingUdpBufferReadIndex] == '\r' && mStartOfPacketDetected)
            {
                mStartOfPacketDetected = false; // reset flag and start looking for packet start again

                // Save the entire message
                mReceivedMessagesList.Add(mIncomingMessageBuffer);
            }

            // While in the middle of a packet
            if(mStartOfPacketDetected)
            {
                //Debug.Log("Character received: " + (char)mIncomingUdpBuffer[mIncomingUdpBufferReadIndex]);

                // Save this byte, we're still in the middle of processing a message
                mIncomingMessageBuffer.Add(mIncomingUdpBuffer[mIncomingUdpBufferReadIndex]);
            }

            mIncomingUdpBufferReadIndex++;
        }
    }

    // This function should run as quickly as possible to ensure the smallest amount of packet loss.
    private void SaveIncomingUdpPackets()
    {
        //Debug.Log("Waiting for broadcast");
        try
        {
            byte[] receivedBytes = mUdpListener.Receive(ref mGroupEP); // can block or timeout depending on how mUdpListener was configured

            System.Buffer.BlockCopy(receivedBytes, 0, mIncomingUdpBuffer, mIncomingUdpBufferWriteIndex, receivedBytes.Length);
            mIncomingUdpBufferWriteIndex += receivedBytes.Length;

            //Debug.Log($" {Encoding.ASCII.GetString(bytes, 0, receivedBytes.Length)}");
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

    // Incoming byte management
    private byte[] mIncomingUdpBuffer = new byte[2 * 1024]; // TODO: what should this size be?
    private int mIncomingUdpBufferReadIndex;
    private int mIncomingUdpBufferWriteIndex;
    private bool mStartOfPacketDetected = false;

    // Incoming message management
    private List<List<byte>> mReceivedMessagesList = new List<List<byte>>();
    private List<byte> mIncomingMessageBuffer;
}
