using UnityEngine;

public interface IGamePhase
{
    /// <summary>
    /// フェーズ開始時に実行する処理
    /// </summary>
    void Enter();

    /// <summary>
    /// フェーズ中に実行する処理
    /// </summary>
    void Update();


    /// <summary>
    /// フェーズ終了時に実行する処理
    /// </summary>
    void Exit();
}
