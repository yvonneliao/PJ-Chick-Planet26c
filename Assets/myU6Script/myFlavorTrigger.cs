using UnityEngine;

public class myFlavorTrigger : MonoBehaviour
{
    [SerializeField] private string _flavorId;
    [SerializeField] private float _destroyDelay = 10f;

    private void Start()
    {
        string _idToRegister = string.IsNullOrEmpty(_flavorId) ? gameObject.name.Replace("(Clone)", "").Trim() : _flavorId;

        myFlavorManager _flavorManager = Object.FindFirstObjectByType<myFlavorManager>();
        if (_flavorManager != null)
        {
            _flavorManager._collectedFlavor(_idToRegister);
        }

        Destroy(gameObject, _destroyDelay);
    }
}

