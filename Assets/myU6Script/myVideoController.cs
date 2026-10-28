using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class myVideoController : MonoBehaviour
{
    public VideoPlayer _video;
    public AudioSource _audio;
    public Button _playPauseButton;
    public TextMeshProUGUI _buttonText;
    public Slider _progressSlider;
    public TextMeshProUGUI _timeText;

    private void Start()
    {
        _playPauseButton.onClick.AddListener(_TogglePlayPause);
    }

    private void Update()
    {
        if (_video.length > 0)
        {
            _progressSlider.value = (float)(_video.time / _video.length);
            double _timeRemaining = _video.length - _video.time;
            _timeText.text = _FormatTime(_timeRemaining);

            // Syncs the text automatically whether triggered by the wall or a click.
            _buttonText.text = _video.isPlaying ? "pause" : "play";
        }
    }

    private void _TogglePlayPause()
    {
        if (_video.isPlaying)
        {
            _video.Pause();
            _audio.Pause();
        }
        else
        {
            _video.Play();
            _audio.Play();
        }
    }

    private string _FormatTime(double time)
    {
        int _m = (int)(time / 60);
        int _s = (int)(time % 60);
        return $"{_m:00}:{_s:00}";
    }
}
