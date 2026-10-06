using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class DiceRoll : MonoBehaviour
{
    [SerializeField] private List<Dice> _allDices;
    [SerializeField] private float _minForce;
    [SerializeField] private float _maxForce;
    [SerializeField] private float _minTorque;
    [SerializeField] private float _maxTorque;
    
    private void RollDices()
    { 
        foreach (var dice in _allDices)
        {
            float randomForce = Random.Range(_minForce, _maxForce);
            float randomTorque = Random.Range(_minTorque, _maxTorque);
            
            Vector3 forceDirection = Random.onUnitSphere;
            Vector3 torqueDirection = Random.onUnitSphere;
            
            forceDirection.y = Mathf.Abs(forceDirection.y);
            forceDirection.z = Mathf.Abs(forceDirection.z);
            
            Vector3 forceVector = randomForce * forceDirection;
            Vector3 torqueVector = randomTorque * torqueDirection;
            
            dice.Roll(forceVector, torqueVector);
        }
        
    }

    public void OnRoll(InputValue input)
    {
        RollDices();
    }
}
