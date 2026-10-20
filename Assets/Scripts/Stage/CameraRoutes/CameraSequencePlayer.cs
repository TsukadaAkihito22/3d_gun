using System;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// CameraSequence(カメラの一連の流れ)を再生する処理
/// </summary>
public class CameraSequencePlayer : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector;

    public event Action Completed;

    private bool _isPlaying;
    private bool _completed;

    //private void Update()
    //{
    //    if (!_isPlaying) return;

    //    if (_playableDirector.time >= _playableDirector.duration)
    //    {
    //        CompleteSequence();
    //    }
    //}

    public void Play(CameraSequenceData sequenceData)
    {
        //なんもないからなんもしないが？
        if (sequenceData == null) return;
        if (sequenceData.Timeline == null) return;

        //_playableDirector.Stop();

        _playableDirector.playableAsset = sequenceData.Timeline;

        _playableDirector.stopped -= OnTimelineStopped;     //イベント登録
        _playableDirector.stopped += OnTimelineStopped;

        //_completed = false;
        //_isPlaying = true;

        _playableDirector.Play();       //再生
    }

    //private void CompleteSequence()
    //{
    //    if(_completed) return;

    //    _completed = true;
    //    _isPlaying = false;

    //    Completed?.Invoke();
    //}

    private void OnTimelineStopped(PlayableDirector director)
    {
        Completed?.Invoke();
    }
}
