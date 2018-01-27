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
        Vector3 direction = collision.transform.position - transform.position;
        _rigidbody.AddForceAtPosition(direction.normalized * 100, collision.contacts[0].point, ForceMode.Impulse);
    }

}
