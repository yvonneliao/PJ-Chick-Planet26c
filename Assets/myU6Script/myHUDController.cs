using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class myHUDController : MonoBehaviour
{
    public TextMeshProUGUI _scoreNumber;
    public TextMeshProUGUI _flavorNumber;
    public Image _flavorFill;

    private void OnEnable()
    {
        myScoreManager._onScoreUpdated += _updateScore;
        myFlavorManager._onFlavorUpdated += _updateFlavor;
    }

    private void OnDisable()
    {
        myScoreManager._onScoreUpdated -= _updateScore;
        myFlavorManager._onFlavorUpdated -= _updateFlavor;
    }

    private void _updateScore(int _score) => _scoreNumber.text = _score.ToString();

    private void _updateFlavor(int _remaining, int _total)
    {
        _flavorNumber.text = _remaining.ToString();
        _flavorFill.fillAmount = 1f - ((float)_remaining / _total);
    }
}
