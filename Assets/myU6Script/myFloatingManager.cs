using System.Collections.Generic;
using UnityEngine;

public class myFloatingManager : MonoBehaviour
{
    public static myFloatingManager _instance { get; private set; }

    [Header("Vertical Bobbing")]
    [SerializeField] public float _bobSpeed = 1.2f;
    [SerializeField] public float _bobHeight = 0.08f;

    [Header("Pendulum Titl (Pitch / Roll)")]
    [SerializeField] public float _tiltSpeed = 1.0f;
    [SerializeField] public float _maxTiltAngle = 0.0f;

    private readonly List<myFloatingObject> _floatingObjects = new List<myFloatingObject>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        float _time = Time.time;
        for (int _i = 0; _i < _floatingObjects.Count; _i++)
        {
            _floatingObjects[_i]._updateFloatMotion(_time, _bobSpeed, _bobHeight, _tiltSpeed, _maxTiltAngle);
        }
    }

    public void _registerObject(myFloatingObject _obj)
    {
        if (!_floatingObjects.Contains(_obj))
        {
            _floatingObjects.Add(_obj);
        }
    }

    public void _unregisterObjects(myFloatingObject _obj)
    {
        _floatingObjects.Remove(_obj);
    }
}
