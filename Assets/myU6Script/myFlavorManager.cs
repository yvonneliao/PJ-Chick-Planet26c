using System;
using System.Collections.Generic;
using UnityEngine;

public class myFlavorManager : MonoBehaviour
{
    public static event Action<int, int> _onFlavorUpdated;

    [SerializeField] private int _totalFlavors = 13;
    private HashSet<string> _collectedFlavors = new HashSet<string>();

    private void Start()
    {
        _onFlavorUpdated?.Invoke(_getRemainingCount(), _totalFlavors);
    }

    public void _collectedFlavor(string _flavorId)
    {
        if (string.IsNullOrEmpty(_flavorId)) return;

        if (_collectedFlavors.Add(_flavorId))
        {
            _onFlavorUpdated?.Invoke(_getRemainingCount(), _totalFlavors);
        }
    }

    private int _getRemainingCount()
    {
        return Mathf.Max(0, _totalFlavors - _collectedFlavors.Count);
    }
}
