using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class myChickHealth : MonoBehaviour
{
    private int _hitCount = 0;
    private bool _isDying = false;

    public void _takeHit()
    {
        if (_isDying) return;

        _hitCount++;
        if (_hitCount >= 3)
        {
            _isDying = true;
            StartCoroutine(_deathAnimation());
        }
    }

    private IEnumerator _deathAnimation()
    {
        Transform _parentTransform = transform.parent;
        Vector3 _startScale = _parentTransform.localScale;
        
        float _duration = myChickManager._instance._fadeDuration;
        Vector3 _finalScale = _startScale * myChickManager._instance._targetScale;
        
        Renderer[] _renderers = _parentTransform.GetComponentsInChildren<Renderer>();
        float _elapsedTime = 0f;

        while (_elapsedTime < _duration)
        {
            _elapsedTime += Time.deltaTime;
            float _t = Mathf.Clamp01(_elapsedTime / _duration);

            _parentTransform.localScale = Vector3.Lerp(_startScale, _finalScale, _t);

            foreach (Renderer _renderer in _renderers)
            {
                foreach (Material _mat in _renderer.materials)
                {
                    if (_mat.HasProperty("_BaseColor"))
                    {
                        Color _color = _mat.GetColor("_BaseColor");
                        _color.a = Mathf.Lerp(1f, 0f, _t);
                        _mat.SetColor("_BaseColor", _color);
                    }
                }
            }

            yield return null;
        }

        Destroy(_parentTransform.gameObject);
    }
}