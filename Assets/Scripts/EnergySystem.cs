using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySystem : MonoBehaviour {

	public Thruster thrusterTopLeft;
	public Thruster thrusterTopRight;
	public Thruster thrusterBottomLeft;
	public Thruster thrusterBottomRight;
	List<Thruster> thrusters;

	float energyInc = 0.1f;
	float energyTank = 4;
    new Rigidbody rigidbody;
    public float AccelerationFactor = 5.0f;
	
	void Start() {
		rigidbody = GetComponent<Rigidbody>();
		thrusters = new List<Thruster>();
		thrusters.Add(thrusterTopLeft);
		thrusters.Add(thrusterBottomLeft);
		thrusters.Add(thrusterBottomRight);
		thrusters.Add(thrusterTopRight);
	}

	void Update () {
		// Substract
		if (Input.GetKeyDown("a")) {
			energyTank += thrusterTopLeft.SubtractEnergy(energyInc);
		}
		if (Input.GetKeyDown("s")) {
			energyTank += thrusterBottomLeft.SubtractEnergy(energyInc);
		}

		if (Input.GetKeyDown("d")) {
			energyTank += thrusterBottomRight.SubtractEnergy(energyInc);
		}
		if (Input.GetKeyDown("f")) {
			energyTank += thrusterTopRight.SubtractEnergy(energyInc);
		}

		// Add
		if (Input.GetKeyDown("q")
		    && energyTank >= energyInc) {
			energyTank -= thrusterTopLeft.AddEnergy(energyInc);
		}
		if (Input.GetKeyDown("w")
		    && energyTank >= energyInc) {
			energyTank -= thrusterBottomLeft.AddEnergy(energyInc);
		}

		if (Input.GetKeyDown("e")
			&& energyTank >= energyInc) {
			energyTank -= thrusterBottomRight.AddEnergy(energyInc);
		}
		if (Input.GetKeyDown("r")
			&& energyTank >= energyInc) {
			energyTank -= thrusterTopRight.AddEnergy(energyInc);
		}
	}

	void FixedUpdate() {
		thrusters.ForEach((Thruster thruster) => {
			rigidbody.AddForceAtPosition(transform.forward * thruster.GetEnergy() * AccelerationFactor, thruster.transform.position);
		});
	}

	void OnGUI () {
		GUI.Label(new Rect(10, 10, 100, 20), thrusterTopLeft.GetEnergy().ToString());
		GUI.Label(new Rect(10, 20, 100, 20), thrusterBottomLeft.GetEnergy().ToString());
		GUI.Label(new Rect(10, 30, 100, 20), thrusterBottomRight.GetEnergy().ToString());
		GUI.Label(new Rect(10, 40, 100, 20), thrusterTopRight.GetEnergy().ToString());
	}
}
