using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BasicMovement : MonoBehaviour
{
    private Rigidbody rigidbody;

    public float AccelerationFactor = 20.0f;
    public float ThrusterForceFactor = 0.5f;

    public Transform LeftThruster;
    public Transform RightThruster;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            rigidbody.AddForceAtPosition(ThrusterForceFactor * transform.forward, LeftThruster.position);
        }
        if (Input.GetKey("d"))
        {
            rigidbody.AddForceAtPosition(ThrusterForceFactor * transform.forward, RightThruster.position);
        }
        rigidbody.AddForce(AccelerationFactor * transform.forward);
    }
}
