using UnityEngine;

public class myChickManager : MonoBehaviour
{
    public static myChickManager _instance { get; private set; }

    [SerializeField] public float _fadeDuration = 1f;
    [SerializeField] public float _targetScale = 3f;
    [SerializeField] public float _flavorYOffset = 0.5f;

    [SerializeField] private GameObject[] _flavorPrefabs;

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

    public GameObject _getRandomFlavor()
    {
        if (_flavorPrefabs == null || _flavorPrefabs.Length == 0) return null;
        return _flavorPrefabs[Random.Range(0, _flavorPrefabs.Length)];
    }
}
