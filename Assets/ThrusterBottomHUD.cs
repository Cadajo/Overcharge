using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterBottomHUD : MonoBehaviour {

	public new MeshRenderer renderer;
	public Thruster thruster;
	// Use this for initialization
	private static readonly int textureCount = 23;

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		int n = 0;
		float energy = thruster.GetEnergy();
		if (energy <= 1.1f) {
			n = (int)Mathf.Floor(15 * (energy / 1.1f));
		} else {
			n = 15 + (int)Mathf.Floor(8 * (energy - 1.1f)/.9f);
		}
		string textureName = "ThrusterBottomHUD/thruster-bottom-hud-"+n;
		Texture texture = (Texture) Resources.Load(textureName);
		renderer.material.mainTexture = texture;
	}
}