using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    private static readonly float MaxEnergy = 2;
    private static readonly float WallHitDamageFactor = 0.01f;
    private static readonly float BeamHitDamageFactor = 0.05f;

    public enum DamageType
    {
        Wall,
        Beam
    }

    private float _energy = 0.0f;
    private float _damage = 0.0f;

    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = transform.parent.GetComponent<Rigidbody>();
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

    public void TakeDamage(float damage, DamageType damageType)
    {
        float multiplier;
        switch (damageType)
        {
            case DamageType.Wall:
            {
                multiplier = WallHitDamageFactor;
                break;
            }
            case DamageType.Beam:
            {
                multiplier = BeamHitDamageFactor;
                break;
            }
            default:
            {
                multiplier = WallHitDamageFactor;
                Debug.LogError("Unknown damage type");
                break;
            }
        }
        _damage += damage * multiplier;
        _damage = Mathf.Min(MaxEnergy, _damage);
    }

    public float GetDamage()
    {
        return _damage;
    }

    public bool IsOvercharged() {
        return _energy > 1f;
    }
}
