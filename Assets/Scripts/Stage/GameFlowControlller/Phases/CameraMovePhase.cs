using System;
using UnityEngine;

/// <summary>
/// CameraMovePhaseの処理
/// </summary>
public class CameraMovePhase : IGamePhase
{
    private readonly CameraSequenceData _sequenceData;      //コンストラクタでのみ変更するデータはreadonly
    private readonly CameraSequencePlayer _cameraSequencePlayer;

    public event Action Completed;

    public CameraMovePhase(CameraSequenceData cameraSequenceData, CameraSequencePlayer cameraSequencePlayer)
    {
        _sequenceData = cameraSequenceData;
        _cameraSequencePlayer = cameraSequencePlayer;
    }

    public void Enter()
    {
        _cameraSequencePlayer.Completed += OnSequenceCompleted;

        _cameraSequencePlayer.Play(_sequenceData);

        Debug.Log("CameraMovePhaseに入りました");
    }

    public void Update()
    {

    }

    public void Exit()
    {
        _cameraSequencePlayer.Completed -= OnSequenceCompleted;
    }

    private void OnSequenceCompleted()
    {
        Completed?.Invoke();
    }
}
