using UnityEngine;

public class myLaserShooter : MonoBehaviour
{
    [SerializeField] private OVRInput.Controller _controller;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _laserLength = 10f;

    private void Update()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, _controller))
        {
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, transform.position);

            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit _hit, _laserLength))
            {
                _lineRenderer.SetPosition(1, _hit.point);

                ParticleSystem _particles = _hit.collider.transform.parent.GetComponentInChildren<ParticleSystem>();

                if (_particles != null)
                {
                    _particles.Play();
                }
            }
            else
            {
                _lineRenderer.SetPosition(1, transform.position + transform.forward * _laserLength) ;
            }
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }
}