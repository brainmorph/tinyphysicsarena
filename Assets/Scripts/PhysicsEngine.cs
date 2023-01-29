using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour
{
    public GameObject go;

    public GameObject force1;
    
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


        // calculate velocity from force and move position accordingly
        velocity += force1.GetComponent<ForceVector>().vec * Time.deltaTime;
        velocity -= velocity * dragCoefficient;

        this.transform.parent.position += velocity * Time.deltaTime;

        // try to do the same for rotations
        netTorque = Vector3.Cross(force1.GetComponent<ForceVector>().vec, force1.transform.position);
        angularVelocity += netTorque * Time.deltaTime;
        angularVelocity -= angularVelocity * dragCoefficient; // fictituous drag term
        angularPositionDelta = angularVelocity * Time.deltaTime;
        this.transform.parent.rotation *=  Quaternion.Euler(angularPositionDelta); // you have to multiply quaternions together to combine them
    }

    public Vector3 velocity = new Vector3(0, 0, 0);

    public Vector3 netTorque = new Vector3(0, 0, 0);
    public Vector3 angularVelocity = new Vector3(0, 0, 0);
    public Vector3 angularPositionDelta = new Vector3(0, 0, 0);

    public float dragCoefficient = 0.002f;

    private ForceVector f;
}
