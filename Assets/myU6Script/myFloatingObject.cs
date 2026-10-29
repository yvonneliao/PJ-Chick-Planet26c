using UnityEngine;

public class myFloatingObject : MonoBehaviour
{
    [SerializeField] private float _motionScale = 1.0f;

    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private float _phaseOffset;
    private float _frequencyOffset;

    private void Start()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;

        // Randomize offsets based on position and instance ID so movements never sync up.
        _phaseOffset = (transform.position.x * 1.5f + transform.position.z * 1.5f) + Random.Range(0f, Mathf.PI * 2f);
        _frequencyOffset = Random.Range(0.85f, 1.15f);

        myFloatingManager._instance?._registerObject(this);
    }

    private void OnDstroy()
    {
        myFloatingManager._instance?._unregisterObjects(this);
    }

    public void _updateFloatMotion(float _globalTime, float _bobSpeed, float _bobHeight, float _tiltSpeed, float _maxTiltAngle)
    {
        float _tBob = (_globalTime * _bobSpeed * _frequencyOffset) + _phaseOffset;
        float _tTilt = (_globalTime * _tiltSpeed * _frequencyOffset) + _phaseOffset;

        // Vertical bobbing (Sine)
        float _yOffset = Mathf.Sin(_tTilt) * _bobHeight * _motionScale;
        transform.position = _startPosition + new Vector3(0f, _yOffset, 0f);

        // Natural compound pendulum tilt across X (Pitch) and Z (Roll)
        float _pitchAngle = Mathf.Sin(_tTilt) * _maxTiltAngle * _motionScale;
        float _rollAngle = Mathf.Cos(_tTilt * 0.85f) * (_maxTiltAngle * 0.75f) * _motionScale;

        Quaternion _tiltRotation = Quaternion.Euler(_pitchAngle, 0f,_rollAngle);
        transform.rotation = _startRotation * _tiltRotation;
    }
}
