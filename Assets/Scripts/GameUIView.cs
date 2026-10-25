using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIView : MonoBehaviour
{
    [SerializeField] private TMP_InputField _diceCountInput;
    [SerializeField] private TMP_InputField _winScoreInput;
    [SerializeField] private TMP_InputField _drawScoreInput;
    [SerializeField] private TMP_InputField _lossScoreInput;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private Button _throwButton;
    [SerializeField] private ScoreCounter _scoreCounter;

    private void OnEnable()
    {
        _scoreCounter.OnScoreChanged += HandleScoreChanged;
        _scoreCounter.OnResult +=  HandleResult;
        _scoreCounter.OnRandomize += UpdateTexts;
        UpdateTexts();
    }
    
    private void OnDisable()
    {
        _scoreCounter.OnScoreChanged -= HandleScoreChanged;
        _scoreCounter.OnResult -=  HandleResult;
        _scoreCounter.OnRandomize -= UpdateTexts;

    }
    
    private void HandleScoreChanged(int currentScore)
    {
        _scoreText.text = $"SCORE: {currentScore}";
    }

    private void HandleResult(string result)
    {
        _resultText.text = result;
    }

    private void UpdateTexts()
    {
        _winScoreInput.text = _scoreCounter.WinScore.ToString();
        _drawScoreInput.text = _scoreCounter.DrawScore.ToString();
        _lossScoreInput.text = _scoreCounter.LossScore.ToString();
    }
}