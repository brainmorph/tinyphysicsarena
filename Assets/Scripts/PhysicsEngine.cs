using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour
{
    public GameObject go;
    
    // Start is called before the first frame update
    void Start()
    {
        Transform t = go.GetComponent<Transform>();
        f = new ForceVector(t.position.x, 
                            t.position.y, 
                            t.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        f.vec = go.GetComponent<Transform>().position;
        Debug.Log("f.vec: " + f.vec);
    }

    private ForceVector f;
}
