using System;
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
    [SerializeField] private Dice _dicePrefab;
    [SerializeField] private Transform _diceSpawnPoint;
    public int NumberOfDice { get; private set; } = 5;
    
    private Coroutine _activeRoutine;
    
    public void OnRoll(InputValue input)
    {
        StartRoll();
    }

    public void StartRoll()
    {
        if (_activeRoutine == null)
            _activeRoutine = StartCoroutine(RollDicesRoutine());
    }
    
    private void RollDices()
    {
        DestroyAllDice();
        for (var i = 0; i < NumberOfDice; i++)
        {
            var newDice = Instantiate(_dicePrefab, _diceSpawnPoint.position, _diceSpawnPoint.rotation);
            _allDices.Add(newDice);
        
            var randomForce = Random.Range(_minForce, _maxForce);
            var randomTorque = Random.Range(_minTorque, _maxTorque);
        
            var forceDirection = Random.onUnitSphere;
            var torqueDirection = Random.onUnitSphere;
        
            forceDirection.y = Mathf.Abs(forceDirection.y);
            forceDirection.z = Mathf.Abs(forceDirection.z);
        
            var forceVector = randomForce * forceDirection;
            var torqueVector = randomTorque * torqueDirection;
        
            newDice.Roll(forceVector, torqueVector);
        }
    }

    private IEnumerator RollDicesRoutine()
    {
        RollDices();
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(AreAllStopped); 
        _scoreCounter.CountScore(_allDices);
        
        _activeRoutine = null;
    }
    
    private bool AreAllStopped()
    {
        foreach (var dice in _allDices)
        {
            if (!dice.IsStopped())
            {
                return false;
            }
        }
        
        return true;
    }

    public void SetNumberOfDices(string number)
    {
        NumberOfDice  = int.Parse(number);
    }

    private void DestroyAllDice()
    {
        foreach (var dice in _allDices)
        {
            Destroy(dice.gameObject);
        }
        _allDices.Clear();
    }
}
