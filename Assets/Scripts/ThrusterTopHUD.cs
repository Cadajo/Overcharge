using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterTopHUD : MonoBehaviour {

	public new MeshRenderer renderer;
	public Thruster thruster;
	// Use this for initialization
	private static readonly int textureCount = 27;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		int n = 0;
		float energy = thruster.GetEnergy();
		if (energy <= 1.1f) {
			n = (int)Mathf.Floor(17 * (energy / 1.1f));
		} else {
			n = 18 + (int)Mathf.Floor(9 * (energy - 1.1f)/.9f);
		}
		string textureName = "ThrusterTopHUD/thruster-top-hud-"+n;
		Texture texture = (Texture) Resources.Load(textureName);
		renderer.material.mainTexture = texture;
	}
}
