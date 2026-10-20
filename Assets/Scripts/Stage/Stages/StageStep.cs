using System;
using UnityEngine;

/// <summary>
/// 各フェーズの持たせる情報の集い
/// </summary>
[Serializable]
public class StageStep
{
    [SerializeField] private GamePhase _phaseType;

    //[SerializeField] private int _requiredDefeatCount;

    [SerializeField] private BattleEnemyGroup _battleEnemyGroup;

    [SerializeField] private CameraSequenceData _cameraSequenceData;

    [SerializeField] private int _reauiredSelectionCount;

    //プロパティ
    #region
    public GamePhase PhaseType => _phaseType;
    //public int RequiredDefeatCount => _requiredDefeatCount;

    public BattleEnemyGroup BattleEnemyGroup => _battleEnemyGroup;
    public CameraSequenceData CameraSequenceData => _cameraSequenceData;

    public int ReauiredSelectionCount => _reauiredSelectionCount;
    #endregion
}
