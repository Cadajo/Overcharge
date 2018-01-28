using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterTopHUD : MonoBehaviour {

	public MeshRenderer hpRenderer;
	public MeshRenderer energyRenderer;
	public Thruster thruster;
	// Use this for initialization
	private static readonly int maxFrame = 27;
	private static readonly int overchargeFrame = 16;
	
	// Update is called once per frame
	void Update () {
		// hp
		int n = 0;
		float hp = thruster.GetHP();
		if (hp <= 1.1f) {
			n = (int)Mathf.Floor(overchargeFrame * (hp / 1.1f));
		} else {
			n = overchargeFrame + (int)Mathf.Floor((maxFrame - overchargeFrame) * (hp - 1.1f)/.9f);
		}
		string hpTexName = "ThrusterTopHUD/thruster-top-hud-hp-"+n;
		Texture hpTex = (Texture) Resources.Load(hpTexName);
		hpRenderer.material.mainTexture = hpTex;

		// energy
		n = 0;
		float energy = thruster.GetEnergy();
		if (energy <= 1.1f) {
			n = (int)Mathf.Floor(overchargeFrame * (energy / 1.1f));
		} else {
			n = overchargeFrame + (int)Mathf.Floor((maxFrame - overchargeFrame) * (energy - 1.1f)/.9f);
		}
		string energyTexName = "ThrusterTopHUD/thruster-top-hud-energy-"+n;
		Texture energyTex = (Texture) Resources.Load(energyTexName);
		energyRenderer.material.mainTexture = energyTex;
	}
}