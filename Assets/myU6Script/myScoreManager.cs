using System;
using UnityEngine;

public class myScoreManager : MonoBehaviour
{
    public static event Action<int> _onScoreUpdated;
    private int _score = 0;

    private void Start() => _onScoreUpdated?.Invoke(_score);

    public void _addScore()
    {
        _score++;
        _onScoreUpdated?.Invoke(_score);
    }
}
