using UnityEngine;

public class myChickTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider _myOther)
    {
        ParticleSystem _myParticles = transform.parent.GetComponentInChildren<ParticleSystem>();

        if (_myParticles != null)
        {
            _myParticles.Play();
        }
    }
}
