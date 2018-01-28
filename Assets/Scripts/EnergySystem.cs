using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Networking;

public class EnergySystem : NetworkBehaviour {

    public Thruster thrusterTopLeft;
    public Thruster thrusterTopRight;
    public Thruster thrusterBottomLeft;
    public Thruster thrusterBottomRight;
    List<Thruster> thrusters;

    float energyInc = 0.1f;
    float energyTank = 4;
    new Rigidbody rigidbody;
    public float floatFactor = 30.0f;
    public float AccelerationFactor = 5.0f;
    private RaceController raceController;

    void Awake()
    {
        thrusters = new List<Thruster>();
        thrusters.Add(thrusterTopLeft);
        thrusters.Add(thrusterBottomLeft);
        thrusters.Add(thrusterBottomRight);
        thrusters.Add(thrusterTopRight);

        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        raceController = FindObjectOfType<RaceController>();
        raceController.AddRacer(gameObject);
    }

    public override void OnNetworkDestroy()
    {
        base.OnNetworkDestroy();
        
        raceController.RemoveRacer(gameObject);
    }

    void OnDisable()
    {
        raceController.RemoveRacer(gameObject);
    }

    [SyncVar]
    private float _e0 = 0f;
    [SyncVar]
    private float _hp0 = 2f;

    [SyncVar]
    private float _e1 = 0f;
    [SyncVar]
    private float _hp1 = 2f;

    [SyncVar]
    private float _e2 = 0f;
    [SyncVar]
    private float _hp2 = 2f;

    [SyncVar]
    private float _e3 = 0f;
    [SyncVar]
    private float _hp3 = 2f;

    [Command]
    void CmdUpdateThrusters(
        float e0,
        float hp0,

        float e1,
        float hp1,

        float e2,
        float hp2,

        float e3,
        float hp3) {

        _e0 = e0;
        _e1 = e1;
        _e2 = e2;
        _e3 = e3;

        _hp0 = hp0;
        _hp1 = hp1;
        _hp2 = hp2;
        _hp3 = hp3;

    }

    void Update () {
        if (!isLocalPlayer) {
            thrusterTopLeft.SetEnergy(_e0);
            thrusterTopLeft.SetHP(_hp0);

            thrusterBottomLeft.SetEnergy(_e1);
            thrusterBottomLeft.SetHP(_hp1);

            thrusterBottomRight.SetEnergy(_e2);
            thrusterBottomRight.SetHP(_hp2);

            thrusterTopRight.SetEnergy(_e3);
            thrusterTopRight.SetHP(_hp3);

            return;
        }

        bool changed = false;

        // Substract
        if (Input.GetKeyDown("a")) {
            energyTank += thrusterTopLeft.SubtractEnergy(energyInc);
            changed = true;
        }
        if (Input.GetKeyDown("s")) {
            energyTank += thrusterBottomLeft.SubtractEnergy(energyInc);
            changed = true;
        }

        if (Input.GetKeyDown("d")) {
            energyTank += thrusterBottomRight.SubtractEnergy(energyInc);
            changed = true;
        }
        if (Input.GetKeyDown("f")) {
            energyTank += thrusterTopRight.SubtractEnergy(energyInc);
            changed = true;
        }

        // Add
        if (Input.GetKeyDown("q")
            && energyTank >= energyInc) {
            energyTank -= thrusterTopLeft.AddEnergy(energyInc);
            changed = true;
        }
        if (Input.GetKeyDown("w")
            && energyTank >= energyInc) {
            energyTank -= thrusterBottomLeft.AddEnergy(energyInc);
            changed = true;
        }

        if (Input.GetKeyDown("e")
            && energyTank >= energyInc) {
            energyTank -= thrusterBottomRight.AddEnergy(energyInc);
            changed = true;
        }
        if (Input.GetKeyDown("r")
            && energyTank >= energyInc) {
            energyTank -= thrusterTopRight.AddEnergy(energyInc);
            changed = true;
        }

        if (changed) {
            CmdUpdateThrusters(
                thrusterTopLeft.GetEnergy(),
                thrusterTopLeft.GetHP(),
                
                thrusterBottomLeft.GetEnergy(),
                thrusterBottomLeft.GetHP(),

                thrusterBottomRight.GetEnergy(),
                thrusterBottomRight.GetHP(),

                thrusterTopRight.GetEnergy(),
                thrusterTopRight.GetHP()
            );
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 10.0f))
        {
            if (hit.transform.tag == "Boxes")
            {
                thrusters.ForEach((thruster) => thruster.RecoverDamage(1 * Time.deltaTime));
            }
        }
    }

    void FixedUpdate() {
        /* TODO: make tendency to unflip. this doesn't work
        rigidbody.rotation = Quaternion.Euler(rigidbody.rotation.x * 0.99f, rigidbody.rotation.y, rigidbody.rotation.z * 0.99f);
        */

        // float
        rigidbody.AddForce(transform.up * floatFactor * Mathf.Max(1 - transform.position.y, 0));

        thrusters.ForEach((Thruster thruster) => {
            // overcharge causes upwards bump
            float overcharge = thruster.GetOvercharge();
            float energy = thruster.GetEnergy();
            if (overcharge > 1) {
                rigidbody.AddForceAtPosition(transform.up * overcharge * AccelerationFactor / 10, thruster.transform.position);

            }

            // forward thrust
            rigidbody.AddForceAtPosition(transform.forward * energy * overcharge * AccelerationFactor, thruster.transform.position);
        });
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contactPoint in collision.contacts)
        {
            Thruster.DamageType type;
            if (collision.gameObject.tag == "Beam") // TODO: Create this tag
            {
                type = Thruster.DamageType.Beam;
            }
            else // Any other type will be trated as a wall
            {
                type = Thruster.DamageType.Wall;
            }

            contactPoint.thisCollider.gameObject.GetComponent<Thruster>()
                .TakeDamage(rigidbody.velocity.magnitude, type);
        }
    }

    void OnGUI () {
        if (!isLocalPlayer) return;

        string d = "F2";
        GUI.Label(new Rect(340, 220, 100, 20),
        thrusterTopLeft.GetEnergy().ToString(d)
        + "/" + thrusterTopLeft.GetHP().ToString(d));
        GUI.Label(new Rect(320, 250, 100, 20),
        thrusterBottomLeft.GetEnergy().ToString(d)
        + "/" + thrusterBottomLeft.GetHP().ToString(d));
        GUI.Label(new Rect(535, 250, 100, 20),
        thrusterBottomRight.GetEnergy().ToString(d)
        + "/" + thrusterBottomRight.GetHP().ToString(d));
        GUI.Label(new Rect(515, 220, 100, 20),
        thrusterTopRight.GetEnergy().ToString(d)
        + "/" + thrusterTopRight.GetHP().ToString(d));
    }

    public void SetupColor(string color)
    {
        Material material = GetComponentInChildren<MeshRenderer>().material;


        string basePath = "Spaceship/Cockpit/" + color;
        Texture baseColor = Resources.Load<Texture>(basePath + "/Cockpit_DefaultMaterial_AlbedoTransparency");
        material.SetTexture("_MainTex", baseColor);

        Texture emissive = Resources.Load<Texture>(basePath + "/Cockpit_DefaultMaterial_Emission");
        material.SetTexture("_EmissionMap", emissive);

        thrusters.ForEach(thruster => thruster.SetupColor(color));
    }
    
    public void RestartEngine()
    {
        thrusters.ForEach(thruster => thruster.Restart());
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
}
