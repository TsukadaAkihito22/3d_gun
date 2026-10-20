using System;
using System.ComponentModel;
using UnityEngine;

public class StartPhase : IGamePhase
{
    //private GameFlowController _gameFlowController;

    //public StartPhase(GameFlowController gameFlowController)
    //{
    //    _gameFlowController = gameFlowController;
    //}

    public event Action Completed;

    public void Enter()
    {
        Debug.Log("StartPhaseに入りました");

        
    }

    public void Update()
    {

    }

    public void Exit()
    {
        Debug.Log("StartPhaseから抜けます");
    }

    private void Complete()
    {
        Completed?.Invoke();
    }
}
