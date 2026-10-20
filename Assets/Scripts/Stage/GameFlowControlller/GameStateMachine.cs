using System.Collections;
using UnityEngine;

/// <summary>
/// 現在のフェーズの動作を管理するクラス
/// </summary>
public class GameStateMachine
{
    private IGamePhase _currentPhase;

    public IGamePhase CurrentPhase => _currentPhase;


    public void ChangePhase(IGamePhase nextPhase)
    {
        _currentPhase?.Exit();      //Exit()があれば実行

        _currentPhase = nextPhase;      //Phase変更

        _currentPhase?.Enter();      //Enter()があれば実行
    }

    public void Update()
    {
        _currentPhase.Update();
    }
}
