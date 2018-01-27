using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePanner : MonoBehaviour
{
    public float Offset = 0.1f;

    private MeshRenderer _renderer;

    // Use this for initialization
    void Start ()
    {
        _renderer = GetComponent<MeshRenderer>();
    }
    
    // Update is called once per frame
    void Update ()
    {
        Vector2 offset = _renderer.material.mainTextureOffset;
        offset.x = (offset.x + (Offset * Time.deltaTime)) % 1;
        _renderer.material.mainTextureOffset = offset;
    }
}
