using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceVector : MonoBehaviour
{
    public Vector3 vec;

    public ForceVector(float x, float y, float z)
    {
        vec = new Vector3(x, y, z);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("vec: " + vec);
    }
}
