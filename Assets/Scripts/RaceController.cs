using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RaceController : NetworkBehaviour
{
    private static readonly string[] Colours = {"Blue", "Green", "Grey", "Orange", "Pink", "Purple", "Red", "Yellow"};

    private Dictionary<GameObject, int> _racers;
    [SyncVar] private bool _raceStarted = false;

    void Awake()
    {
        _racers = new Dictionary<GameObject, int>();
    }

    public override void OnNetworkDestroy()
    {
        _racers.Clear();
    }

    public void AddRacer(GameObject racer)
    {
        if (_raceStarted)
        {
            Destroy(racer);
            Debug.Log("A racer tried to register while the race has already started");
            return;
        }

        racer.transform.position = transform.position + (transform.forward * 3 * (_racers.Count + 1));
        racer.transform.LookAt(transform);
        Vector3 p = racer.transform.position;
        racer.transform.position = new Vector3(p.x, 0.13f, p.z);

        racer.GetComponent<EnergySystem>().SetupColor(Colours[_racers.Count % Colours.Length]);

        _racers.Add(racer, 0);
    }

    public void RemoveRacer(GameObject racer)
    {
        _racers.Remove(racer);
    }

    void Update()
    {
        if (!_raceStarted && isServer && Input.GetKeyUp(KeyCode.Space))
        {
            RpcResetRacers();
        }
    }

    [ClientRpc]
    void RpcResetRacers()
    {
        _raceStarted = true;

        int i = 1;
        foreach (KeyValuePair<GameObject, int> racer in _racers)
        {
            racer.Key.transform.position = transform.position + (transform.forward * 3 * i);
            racer.Key.transform.LookAt(transform);
            Vector3 p = racer.Key.transform.position;
            racer.Key.transform.position = new Vector3(p.x, 0.13f, p.z);
            racer.Key.GetComponent<EnergySystem>().RestartEngine();
            ++i;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (_raceStarted && other.tag == "Finish")
        {
            ++_racers[other.gameObject.transform.parent.gameObject];
        }
    }
}
