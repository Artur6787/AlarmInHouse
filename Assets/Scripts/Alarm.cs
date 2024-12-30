using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _volumeMaxDelta;
    [SerializeField] private float _maxVolume;

    private AudioSource _audioSource;
    private bool _isWork;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = true;
        _audioSource.volume = 0;
    }

    private void Update()
    {
        if (_isWork)
        {
            _audioSource.volume += Mathf.MoveTowards(0, _maxVolume, _volumeMaxDelta * Time.deltaTime);
        }
        else
        {
            _audioSource.volume -= Mathf.MoveTowards(0, _audioSource.volume, _volumeMaxDelta * Time.deltaTime);

            if (_audioSource.volume <= 0)
            {
                _audioSource.Stop();
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Enemy enemy))
        {
            _isWork = true;

            if (_audioSource.isPlaying == false)
            {
                _audioSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Enemy enemy))
        {
            _isWork = false;
        }
    }
}