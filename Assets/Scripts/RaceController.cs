using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

struct Racer
{
    public GameObject entity;
    public int lap;
}

public class RaceController : NetworkBehaviour
{
    private static readonly string[] Colours = {"Blue", "Green", "Grey", "Orange", "Pink", "Purple", "Red", "Yellow"};

    private Dictionary<GameObject, int> _racers;

    void Start()
    {
        _racers = new Dictionary<GameObject, int>();
    }

    public void AddRacer(GameObject racer)
    {
        _racers.Add(racer, 0);

        Vector3 p = racer.transform.position = transform.position + (transform.forward * 3 * _racers.Count);

        racer.transform.LookAt(transform);

        // set on the ground so that it lifts off
        racer.transform.position = new Vector3(p.x, 0.13f, p.z);

        racer.GetComponent<EnergySystem>().SetupColor(Colours[(_racers.Count - 1) % Colours.Length]);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            int lap = ++_racers[other.gameObject.transform.parent.gameObject];
        }
    }
}
