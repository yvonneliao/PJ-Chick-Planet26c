using System;
using UnityEngine;

public class myFlavorManager : MonoBehaviour
{
    public static event Action<int, int> _onFlavorUpdated;
    private int _remaining = 13;
    private int _total = 13;

    private void Start() => _onFlavorUpdated?.Invoke(_remaining, _total);

    public void _collectedFlavor()
    {
        if (_remaining > 0) _remaining--;
        _onFlavorUpdated?.Invoke(_remaining, _total);
    }
}
