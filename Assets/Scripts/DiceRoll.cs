using System.Collections;
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
    [SerializeField] private ScoreCounter _scoreCounter;
    
    private Coroutine _activeRoutine;
    
    public void OnRoll(InputValue input)
    {
        if (_activeRoutine == null)
            _activeRoutine = StartCoroutine(RollDicesRoutine());
    }
    
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

    private IEnumerator RollDicesRoutine()
    {
        RollDices();
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(AreAllStopped); ;
        _scoreCounter.CountScore(_allDices);
        
        _activeRoutine = null;
    }
    
    private bool AreAllStopped()
    {
        foreach (Dice dice in _allDices)
        {
            if (!dice.IsStopped())
            {
                return false;
            }
        }
        
        return true;
    }
}
