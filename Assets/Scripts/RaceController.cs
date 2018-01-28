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

    private float _currentCount;
    private bool _countdown = false;
    private bool _sempaphoreDisappear = false;

    public Semaphore Semaphore;

    void Awake()
    {
        _racers = new Dictionary<GameObject, int>();
    }

    public override void OnNetworkDestroy()
    {
        _racers.Clear();
        _raceStarted = false;
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

        EnergySystem engine = racer.GetComponent<EnergySystem>();
        engine.SetupColor(Colours[_racers.Count % Colours.Length]);
        engine.IsLocked = true;

        _racers.Add(racer, 0);
    }

    public void RemoveRacer(GameObject racer)
    {
        _racers.Remove(racer);
    }

    void Update()
    {
        if (_raceStarted && _sempaphoreDisappear && isServer)
        {
            _currentCount -= Time.deltaTime;

            int ceil = Mathf.CeilToInt(_currentCount);
            if (ceil == 0)
            {
                RpcSetSemaphoreNumber(-1);
                _sempaphoreDisappear = false;
            }
        }

        if (_raceStarted && isServer && _countdown)
        {
            _currentCount -= Time.deltaTime;

            int ceil = Mathf.CeilToInt(_currentCount);
            RpcSetSemaphoreNumber(ceil);

            if (ceil == 0)
            {
                _countdown = false;
                _currentCount = 5.0f;
                _sempaphoreDisappear = true;

                foreach (GameObject racer in _racers.Keys)
                {
                    racer.GetComponent<EnergySystem>().IsLocked = false;
                }
            }
        }

        if (!_raceStarted && isServer)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                RpcResetRacers();
                _countdown = true;
                Semaphore.gameObject.SetActive(true);
                _currentCount = 3.0f;
                RpcSetSemaphoreNumber(3);
            }
            else
            {
                RpcSetSemaphoreNumber(4);
            }
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
            EnergySystem engine = racer.Key.GetComponent<EnergySystem>();
            engine.RestartEngine();
            engine.IsLocked = true;
            ++i;
        }
    }

    [ClientRpc]
    void RpcSetSemaphoreNumber(int number)
    {
        if (number < 0)
        {
            Semaphore.gameObject.SetActive(false);
        }
        else
        {
            Semaphore.gameObject.SetActive(true);
            Semaphore.SetNumber(number);
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
