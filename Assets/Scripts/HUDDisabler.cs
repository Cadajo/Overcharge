using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HUDDisabler : NetworkBehaviour {

	public List<GameObject> thrusterHUDs;
	void Start () {
		if (!isLocalPlayer) {
			thrusterHUDs.ForEach((GameObject hud) => {
				hud.SetActive(false);
			});
		}
	}
}
