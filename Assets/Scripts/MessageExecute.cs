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
    }
}
