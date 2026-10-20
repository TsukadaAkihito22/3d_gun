using UnityEngine;

public class GameFlowTest : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemies;

    private GameStateMachine _stateMachine;
    private BattlePhase _battlePhase;

    private void Start()
    {
        _stateMachine = new GameStateMachine();

        //_battlePhase = new BattlePhase(3);

        //_stateMachine.ChangePhase(_battlePhase);

        //foreach (Enemy enemy in _enemies)
        //{
        //    _battlePhase.RegisterEnemy(enemy);
        //}
    }

    private void Update()
    {
        _stateMachine.Update();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _enemies[0].Die();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _enemies[1].Die();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _enemies[2].Die();
        }

        _stateMachine.Update();
    }
}