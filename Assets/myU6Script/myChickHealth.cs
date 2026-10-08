using UnityEngine;
using System.Collections;

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

            AudioSource _audio = transform.parent.GetComponentInChildren<AudioSource>(true);
            if (_audio != null && _audio.clip != null)
            {
                GameObject _tempAudio = new GameObject("TempAudio");
                AudioSource _newSource = _tempAudio.AddComponent<AudioSource>();
                _newSource.clip = _audio.clip;
                _newSource.spatialBlend = 0f; 
                _newSource.volume = _audio.volume;
                _newSource.Play();
                Destroy(_tempAudio, _audio.clip.length);
            }

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
        
        // Switch materials to Transparent only when fading begins
        foreach (Renderer _renderer in _renderers)
        {
            foreach (Material _mat in _renderer.materials)
            {
                _mat.SetFloat("_Surface", 1f); // 1 = Transparent
                _mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                _mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                _mat.SetInt("_ZWrite", 0);
                _mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                _mat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
            }
        }
        
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