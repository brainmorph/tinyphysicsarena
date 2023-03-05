using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MessageExecute : MonoBehaviour
{
    UDPListener udpListener;

    // Awake is called before Start
    void Awake()
    {
        udpListener = gameObject.GetComponent<UDPListener>();
    }

    // Update is called once per frame
    void Update()
    {
        ASCIIEncoding encoding = new ASCIIEncoding();
        string msg = encoding.GetString(udpListener.NextMessage().ToArray());

        Debug.Log($"Executing message: { msg }");

        Debug.Log($"{udpListener.CountMessages()} messages left.");

        // What I really want to do is extract the coordinates out of 'msg' at this point.
        // msg looks something like this: "`POS0.00,0.34,0.00\r"
    }
}
