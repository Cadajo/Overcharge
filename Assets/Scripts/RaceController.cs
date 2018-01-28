using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RaceController : NetworkBehaviour
{
    private static readonly string[] Colours = {"Blue", "Green", "Grey", "Orange", "Pink", "Purple", "Red", "Yellow"};

    private Dictionary<GameObject, int> _racers;
    private bool _raceStarted = false;

    void Start()
    {
        _racers = new Dictionary<GameObject, int>();
    }

    public void AddRacer(GameObject racer)
    {
        if (_raceStarted)
        {
            Destroy(racer);
            Debug.Log("A racer tried to register while the race has already started");
            return;
        }

        _racers.Add(racer, 0);

        Vector3 p = racer.transform.position = transform.position + (transform.forward * 3 * _racers.Count);

        racer.transform.LookAt(transform);

        // set on the ground so that it lifts off
        racer.transform.position = new Vector3(p.x, 0.13f, p.z);

        racer.GetComponent<EnergySystem>().SetupColor(Colours[(_racers.Count - 1) % Colours.Length]);
    }

    void Update()
    {
        if (!_raceStarted && isServer && Input.GetKeyUp(KeyCode.Space))
        {
            _raceStarted = true;

            int i = 0;
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
    }

    void OnTriggerEnter(Collider other)
    {
        if (_raceStarted && other.tag == "Finish")
        {
            ++_racers[other.gameObject.transform.parent.gameObject];
        }
    }
}
