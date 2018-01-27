using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    AudioSource source;
    private static readonly float MaxEnergy = 2;
    private static readonly float WallHitDamageFactor = 0.01f;
    private static readonly float BeamHitDamageFactor = 0.05f;
    private static readonly float OverchargeDamageFactor = 0.05f;

    public enum DamageType
    {
        Wall,
        Beam,
        Overcharge
    }

    private float _energy = 0.0f;
    private float _hp = 2.0f;

    void Start () {
        source = GetComponent<AudioSource>();
    }

    public float AddEnergy(float delta)
    {
        float previous = _energy;
        _energy = Mathf.Min(_hp, _energy + delta);
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

    public float GetOvercharge () {
        if (_energy > 1) {
            float overcharge = Random.Range(1f, 1.4f);
            if (overcharge > 1.38f) {
                overcharge += overcharge;
                TakeDamage(1f, DamageType.Overcharge);
                source.Play();
            }
            return overcharge;
        } else {
            return 1;
        }
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
        _hp -= damage * multiplier;
        _hp = Mathf.Max(_hp, 0f);
    }

    public float GetHP()
    {
        return _hp;
    }

    public bool IsOvercharged() {
        return _energy > 1f;
    }
}
