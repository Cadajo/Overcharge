using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semaphore : MonoBehaviour
{
    public Texture[] States = new Texture[4];

    private Material _material;

    void Awake()
    {
        _material = GetComponent<MeshRenderer>().sharedMaterial;
    }

    public void SetNumber(int number)
    {
        _material.mainTexture = States[number];
    }
}
