using UnityEngine;

public class myChickManager : MonoBehaviour
{
    public static myChickManager _instance { get; private set; }

    [SerializeField] public float _fadeDuration = 1f;
    [SerializeField] public float _targetScale = 3f;

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
}
