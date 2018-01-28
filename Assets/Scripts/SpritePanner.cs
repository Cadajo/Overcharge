using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePanner : MonoBehaviour
{
    public float Offset = 0.1f;

    public enum Axis
    {
        X,
        Y
    }

    public Axis PanAxis = Axis.X;

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
        float deltaMovement = (Offset * Time.deltaTime);

        if (PanAxis == Axis.X)
        {
            offset.x = (offset.x + deltaMovement) % 1;
        }
        else
        {
            offset.y = (offset.y + deltaMovement) % 1;
        }

        _renderer.material.mainTextureOffset = offset;
    }
}
