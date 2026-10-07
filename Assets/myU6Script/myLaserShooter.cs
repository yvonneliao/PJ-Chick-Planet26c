using UnityEngine;

public class myLaserShooter : MonoBehaviour
{
    [SerializeField] private OVRInput.Controller _controller;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private BoxCollider _laserCollider;
    [SerializeField] private float _laserLength = 10f;

    private void Start()
    {
        _laserCollider.isTrigger = true;
        _laserCollider.size = new Vector3(0.01f, 0.01f, _laserLength);
        _laserCollider.center = new Vector3(0, 0, _laserLength / 2f);
        _laserCollider.enabled = false;
    }

    private void Update()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, _controller))
        {
            _lineRenderer.enabled = true;
            _laserCollider.enabled = true;

            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, transform.position + transform.forward * _laserLength);
        }
        else
        {
            _lineRenderer.enabled = false;
            _laserCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        ParticleSystem _particles = _other.transform.parent.GetComponentInChildren<ParticleSystem>();
        
        if (_particles != null)
        {
            _particles.Play();
        }
    }
}