using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceVector : MonoBehaviour
{
    public Vector3 vec;


    
    public GameObject TailObject;
    public GameObject TipObject;
    public float magnitude = 1;


    public Vector3 Tail;
    public Vector3 Tip;
    

    public ForceVector(float x, float y, float z)
    {
        vec = new Vector3(x, y, z);
        magnitude = 1;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Tail = TailObject.transform.parent.localPosition;
        Tip = Tail + TipObject.transform.localPosition;

        vec = (Tip - Tail) * magnitude;
        Debug.Log("vec: " + vec);
    }
}
