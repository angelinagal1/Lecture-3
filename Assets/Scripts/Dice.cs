using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    private Dictionary<Vector3, int> _dictValues;


    private void Awake()
    {
        _dictValues = new()
        {
            { transform.up, 2 },
            { -transform.up, 5 },
            { transform.right, 6 },
            { -transform.right, 1 },
            { transform.forward, 3 },
            { -transform.forward, 4 },
        };
    }
    
    public void Roll(Vector3 force, Vector3 torque)
    {
        _rb.isKinematic = false;
        _rb.AddForce(force, ForceMode.Impulse);
        _rb.AddTorque(torque, ForceMode.Impulse);
    }

    public bool IsStopped()
    {
        return _rb.angularVelocity.magnitude < 0.05f && _rb.linearVelocity.magnitude < 0.05f;
    }

    public int CurrentValue()
    {
        int diceValue = 0;
        float maxDot = -1;
        foreach (var element in _dictValues)
        {
            Vector3 faceVector = transform.TransformDirection(element.Key);
            float curDot = Vector3.Dot(faceVector, Vector3.up);
            if (curDot > maxDot)
            {
                maxDot = curDot;
                diceValue = element.Value;
            }
        }

        return diceValue;
    }
}