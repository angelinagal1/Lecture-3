using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    public void Roll(Vector3 force, Vector3 torque)
    {
        _rb.isKinematic = false;
        _rb.AddForce(force, ForceMode.Impulse);
        _rb.AddTorque(torque, ForceMode.Impulse);
        Debug.Log(_rb.isKinematic);
    }

}
