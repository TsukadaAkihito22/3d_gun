using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// カメラの流れ(動き)の情報を持つスクリプタブルオブジェクトクラス
/// </summary>
[CreateAssetMenu(fileName = "CameraSequenceData", menuName = "Game/Camera Sequence Data")]
public class CameraSequenceData : ScriptableObject
{
    [SerializeField] private PlayableAsset _timeline;

    public PlayableAsset Timeline => _timeline;
}