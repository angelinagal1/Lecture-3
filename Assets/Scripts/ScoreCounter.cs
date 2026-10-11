using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public void CountScore(List<Dice> allDices)
    {
        int currentScore = 0;
        foreach (var dice in allDices)
        {
            currentScore += dice.CurrentValue();
        }
        
        Debug.Log(currentScore);
    }
}