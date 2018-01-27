using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityDebugger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Transform transform = GetComponent<Transform>();
		Rigidbody rigidbody = GetComponent<Rigidbody>();
		Debug.DrawRay(transform.position, rigidbody.velocity, Color.blue, 0.1f);
	}
}
