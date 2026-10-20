using UnityEngine;

public class VoiceController : MonoBehaviour
{
    [SerializeField] private AudioSource _zombieVoice;
    [SerializeField] private float _minPlayTime;
    [SerializeField] private float _maxPlayTime;

    private float _timer;
    private float _voiceTime;

    void Start()
    {
        _voiceTime = Random.Range(_minPlayTime, _maxPlayTime);
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _voiceTime)
        {
            PlayVoice();
        }
    }

    private void PlayVoice()
    {
        _zombieVoice.Play();
        _voiceTime = Random.Range(_minPlayTime, _maxPlayTime);
        _timer = 0f;
    }
}
