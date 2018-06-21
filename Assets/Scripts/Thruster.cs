using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class Thruster : MonoBehaviour
{
    private static readonly float MaxEnergy = 2.0f;
    private static readonly float WallHitDamageFactor = 0.01f;
    private static readonly float BeamHitDamageFactor = 0.05f;
    private static readonly float OverchargeDamageFactor = 0.01f;

    public enum DamageType
    {
        Wall,
        Beam,
        Overcharge
    }

    private float _energy = 0.0f;

    private float _hp = MaxEnergy;

    private AudioSource source;
    private TrailRenderer _trail;

    void Start () {
        source = GetComponent<AudioSource>();
        _trail = GetComponentInChildren<TrailRenderer>();
        _trail.enabled = false;
    }

    void Update () {
        _trail.enabled = (_energy >= 0.1f);
        _trail.startWidth = 0.3f * (_energy/1);
        _trail.endWidth = _trail.startWidth * 1.2f;
        _trail.time = 1f;
        _trail.startColor = _trail.endColor = (_energy > 1.1f
            ? Color.red
            : Color.cyan);
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

    /* GOD MODE */
    public void SetEnergy(float energy) {
        _energy = energy;
    }

    public void SetHP(float hp) {
        _hp = hp;
    }
    /* GOD MODE */

    public float GetEnergy () {
        return _energy;
    }

    public float GetOvercharge () {
        if (_energy > 1.01f) {
            float overcharge = Random.Range(1f, _energy * 2);
            if (overcharge > 2f && Random.Range(0f, 1f) < 0.1f) {
                TakeDamage(overcharge, DamageType.Overcharge);
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
            case DamageType.Overcharge:
            {
                multiplier = OverchargeDamageFactor;
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

        if (_hp < _energy) {
            _energy = Mathf.Min(_energy, _hp);
        }
    }

    public void RecoverDamage(float recovery)
    {
        _hp = Mathf.Min(MaxEnergy, recovery + _hp);
    }

    public float GetHP()
    {
        return _hp;
    }

    public bool IsOvercharged() {
        return _energy > 1.01f;
    }

    public void SetupColor(string color)
    {
        Material material = GetComponent<MeshRenderer>().material;


        string basePath = "Spaceship/Thruster/" + color;
        Texture baseColor = Resources.Load<Texture>(basePath + "/Thruster_DefaultMaterial_AlbedoTransparency");
        material.SetTexture("_MainTex", baseColor);

        Texture emissive = Resources.Load<Texture>(basePath + "/Thruster_DefaultMaterial_Emission");
        material.SetTexture("_EmissionMap", emissive);
    }

    public void Restart()
    {
        _hp = MaxEnergy;
        _energy = 0;
        _trail.enabled = false;
        source.Stop();
    }
}
