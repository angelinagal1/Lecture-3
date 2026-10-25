using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public event Action<int> OnScoreChanged;
    public event Action<string> OnResult;
    public event Action OnRandomize;
    [SerializeField] private DiceRoll _diceRoll;
    
    public int WinScore { get; private set; }
    public int DrawScore { get; private set; }
    public int LossScore { get; private set; }

    private void Awake()
    {
        RandomizeScore();
    }
    public void CountScore(List<Dice> allDices)
    {
        var currentScore = 0;
        foreach (var dice in allDices)
        {
            currentScore += dice.CurrentValue();
        }
        OnScoreChanged.Invoke(currentScore);
        GetResult(currentScore);
    }

    public void SetWinScore(string score)
    {
        WinScore = int.Parse(score);
    }
    
    public void SetDrawScore(string score)
    {
        DrawScore = int.Parse(score);
    }

    public void SetLossScore(string score)
    {
        LossScore = int.Parse(score);
    }

    private void GetResult(int currentScore)
    {
        if (currentScore >= WinScore)
        {
            OnResult?.Invoke("VICTORY");
        }
        else if (currentScore >= DrawScore)
        {
            OnResult?.Invoke("DRAW");
        }
        else
        {
            OnResult?.Invoke("LOSS");
        }
    }
    
    public void RandomizeScore()
    {
        var numberOfDice = _diceRoll.NumberOfDice;
        WinScore = UnityEngine.Random.Range(numberOfDice * 4, numberOfDice * 6);
        DrawScore = UnityEngine.Random.Range(numberOfDice * 2, numberOfDice * 4 - 1);
        LossScore = UnityEngine.Random.Range(0, numberOfDice * 2);
        OnRandomize?.Invoke();
    }
}