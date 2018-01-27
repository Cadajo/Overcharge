using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

	// Use this for initialization
	public GameObject target;

	// Update is called once per frame
	void Update () {
		transform.LookAt(target.transform);
	}
}
