using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int _totalScore;
    private Rigidbody[] _allDices;
    [System.Serializable] private struct DiceFace
    {
        private Vector3 _localPosition;
        private int _value;
    }
    
    private void Start()
    {
        _allDices = GetComponentsInChildren<Rigidbody>();
        foreach (var dice in _allDices)
        {
            dice.isKinematic = true;
        }
    }

    private bool DiceStopped()
    {
        foreach (var dice in _allDices )
        {
            if (dice.linearVelocity.magnitude > 0.05f || dice.angularVelocity.magnitude > 0.05f)
            {
                return false;
            }
        }
        return true;
    }

    private void CalculateTotalScore()
    {

    }
    
}
