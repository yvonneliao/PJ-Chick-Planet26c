using System.Collections;
using UnityEngine;
using TMPro;

public class myScorePumpAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform _scoreIcon;
    [SerializeField] private TextMeshProUGUI _scoreNumber;

    [SerializeField] private float _beatDuration = 0.08f;

    private Vector3 _iconBaseScale;
    private Vector3 _numberBaseScale;
    private Coroutine _pumpCoroutine;
    private bool _isInitialized = false;

    private void Awake()
    {
        if (_scoreIcon != null) _iconBaseScale = _scoreIcon.localScale;
        if (_scoreNumber != null) _numberBaseScale = _scoreNumber.rectTransform.localScale;
    }

    private void OnEnable()
    {
        myScoreManager._onScoreUpdated += _onScoreChanged;
    }

    private void OnDisable()
    {
        myScoreManager._onScoreUpdated -= _onScoreChanged;
        _resetScales();
    }

    private void _onScoreChanged(int _score)
    {
        // Skip the initial start event invocation to avoid animating on load
        if (!_isInitialized)
        {
            _isInitialized = true;
            return;
        }

        if (_pumpCoroutine != null)
        {
            StopCoroutine(_pumpCoroutine);
            _resetScales();
        }

        _pumpCoroutine = StartCoroutine(_playHeartbeatAnimation());
    }

    private IEnumerator _playHeartbeatAnimation()
    {
        // First pump: +20% -> Normal
        yield return StartCoroutine(_scaleTo(1.35f, _beatDuration));
        yield return StartCoroutine(_scaleTo(1.00f, _beatDuration));

        // Second pump: +10% -> Normal
        yield return StartCoroutine(_scaleTo(1.15f, _beatDuration));
        yield return StartCoroutine(_scaleTo(1.00f, _beatDuration));

        _pumpCoroutine = null;
    }

    private IEnumerator _scaleTo(float _multiplier, float _duration)
    {
        float _elapsedTime = 0f;

        Vector3 _currentIconScale = _scoreIcon != null ? _scoreIcon.localScale : Vector3.one;
        Vector3 _targetIconScale = _iconBaseScale * _multiplier;

        Vector3 _currentNumberScale = _scoreNumber != null ? _scoreNumber.rectTransform.localScale : Vector3.one;
        Vector3 _targetNumberScale = _numberBaseScale * _multiplier;

        while (_elapsedTime < _duration)
        {
            _elapsedTime += Time.deltaTime;
            float _t = Mathf.Clamp01(_elapsedTime / _duration);

            if (_scoreIcon != null)
            {
                _scoreIcon.localScale = Vector3.Lerp(_currentIconScale, _targetIconScale, _t);
            }

            if (_scoreNumber != null)
            {
                _scoreNumber.rectTransform.localScale = Vector3.Lerp(_currentNumberScale, _targetNumberScale, _t);
            }

            yield return null;
        }

        if (_scoreIcon != null) _scoreIcon.localScale = _targetIconScale;
        if (_scoreNumber != null) _scoreNumber.rectTransform.localScale = _targetNumberScale;
    }

    private void _resetScales()
    {
        if (_scoreIcon != null) _scoreIcon.localScale = _iconBaseScale;
        if (_scoreNumber != null) _scoreNumber.rectTransform.localScale = _numberBaseScale;
    }
}
