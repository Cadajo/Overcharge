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

    public void AddEnergy(float delta)
    {
        _energy = Mathf.Min(MaxEnergy, _energy + delta);
    }

    public void SubtractEnergy(float delta)
    {
        _energy = Mathf.Max(0, _energy - delta);
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
