using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _volumeMaxDelta;
    [SerializeField] private float _maxVolume;

    private AudioSource _audioSource;
    private Coroutine _volumeCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = true;
        _audioSource.volume = 0;

        var enemyDetector = GetComponent<EnemyDetector>();

        if (enemyDetector != null)
        {
            enemyDetector.OnEnemyEntered += HandleEnemyEntered;
            enemyDetector.OnEnemyExited += HandleEnemyExited;
        }
    }

    private void HandleEnemyEntered()
    {
        if (_volumeCoroutine != null)
        {
            StopCoroutine(_volumeCoroutine);
        }

        _volumeCoroutine = StartCoroutine(ChangeVolume(0, _maxVolume));

        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }

    private void HandleEnemyExited()
    {
        if (_volumeCoroutine != null)
        {
            StopCoroutine(_volumeCoroutine);
        }

        _volumeCoroutine = StartCoroutine(ChangeVolume(_audioSource.volume, 0));
    }

    private IEnumerator ChangeVolume(float startVolume, float targetVolume)
    {
        float timeElapsed = 0f;
        float duration = Mathf.Abs(targetVolume - startVolume) / _volumeMaxDelta;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            _audioSource.volume = Mathf.Lerp(startVolume, targetVolume, timeElapsed / duration);
            yield return null;
        }

        _audioSource.volume = targetVolume;

        if (targetVolume <= 0)
        {
            _audioSource.Stop();
            _volumeCoroutine = null;
        }
    }
}
//using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
//public class Alarm : MonoBehaviour
//{
//    [SerializeField] private float _volumeMaxDelta;
//    [SerializeField] private float _maxVolume;

//    private AudioSource _audioSource;
//    private bool _isWork;

//    private void Awake()
//    {
//        _audioSource = GetComponent<AudioSource>();
//        _audioSource.playOnAwake = false;
//        _audioSource.loop = true;
//        _audioSource.volume = 0;

//        var enemyDetector = GetComponent<EnemyDetector>();

//        if (enemyDetector != null)
//        {
//            enemyDetector.OnEnemyEntered += HandleEnemyEntered;
//            enemyDetector.OnEnemyExited += HandleEnemyExited;
//        }
//    }

//    private void Update()
//    {
//        if (_isWork)
//        {
//            _audioSource.volume += Mathf.MoveTowards(0, _maxVolume, _volumeMaxDelta * Time.deltaTime);
//        }
//        else
//        {
//            _audioSource.volume -= Mathf.MoveTowards(0, _audioSource.volume, _volumeMaxDelta * Time.deltaTime);

//            if (_audioSource.volume <= 0)
//            {
//                _audioSource.Stop();
//            }
//        }
//    }

//    private void HandleEnemyEntered()
//    {
//        _isWork = true;

//        if (!_audioSource.isPlaying)
//        {
//            _audioSource.Play();
//        }
//    }

//    private void HandleEnemyExited()
//    {
//        _isWork = false;
//    }
//}