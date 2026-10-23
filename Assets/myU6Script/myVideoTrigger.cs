using UnityEngine;
using UnityEngine.Video;

public class myVideoTrigger : MonoBehaviour
{
    public VideoPlayer _video;
    public AudioSource _audio;
    private bool _hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger hit by: " + other.gameObject.name);

        // only trigger if the object walking through is the VR Player.
        if (other.CompareTag("Player"))
        {
            // only play if it hasn't been triggered yet.
            if (!_hasPlayed)
            {
                if (_video != null) _video.Play();
                if (_audio != null) _audio.Play();
                _hasPlayed = true;
            }
        }
    }
}
