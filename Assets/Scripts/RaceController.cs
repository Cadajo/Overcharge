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
    private Dictionary<GameObject, int> _racers;

    void Start()
    {
        _racers = new Dictionary<GameObject, int>();
    }

    public void AddRacer(GameObject racer)
    {
        _racers.Add(racer, 0);

        racer.transform.position = transform.position + (transform.forward * 3 * _racers.Count);
        racer.transform.LookAt(transform);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            int lap = ++_racers[other.gameObject.transform.parent.gameObject];
        }
    }
}
