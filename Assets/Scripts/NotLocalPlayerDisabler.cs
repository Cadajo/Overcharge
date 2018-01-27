using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NotLocalPlayerDisabler : NetworkBehaviour {

	public GameObject target;

	void Start () {
		if (!isLocalPlayer) {
			target.SetActive(false);
		}
	}
}
