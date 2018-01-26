using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    private static readonly float MaxEnergy = 2;

    private float _energy = 0.0f;
    private float _damage = 0.0f;

    public Rigidbody SpaceshipRigidbody = null;

    void Start()
    {
        if (SpaceshipRigidbody == null)
        {
            Debug.LogError("Spaceship rigidbody not set! :(");
            enabled = false;
        }
    }

    public float AddEnergy(float delta)
    {
        float previous = _energy;
        _energy = Mathf.Min(MaxEnergy, _energy + delta);
        float added = _energy - previous;
        return added;
    }

    public float SubtractEnergy(float delta)
    {
        float previous = _energy;
        _energy = Mathf.Max(0, _energy - delta);
        float substracted = previous - _energy;
        return substracted;
    }

    public float GetEnergy () {
        return _energy;
    }

    public void TakeDamage(float damage)
    {
        _damage += damage;
    }

    public float GetDamage()
    {
        return _damage;
    }
    
    void FixedUpdate ()
    {
        SpaceshipRigidbody.AddForceAtPosition(transform.forward * _energy, transform.position);
    }
}
