using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NotLocalPlayerDisabler : NetworkBehaviour {
	void Start () {
		if (!isLocalPlayer) {
			gameObject.SetActive(false);
		}
	}

	void Update () {
		if (!isLocalPlayer) {
			gameObject.SetActive(false);
		}
	}
}
