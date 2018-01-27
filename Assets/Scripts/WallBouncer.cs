using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WallBouncer : MonoBehaviour
{
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 reflection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
        _rigidbody.AddForce(reflection * 10, ForceMode.VelocityChange);
        //Debug.DrawLine(transform.position, transform.position + reflection * 1000, Color.green, 1000, false);
    }

}
