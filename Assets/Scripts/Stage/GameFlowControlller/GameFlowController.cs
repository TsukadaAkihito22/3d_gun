using UnityEngine;

/// <summary>
/// StageDataを読み、次のPhaseに切り替える処理
/// </summary>
public class GameFlowController : MonoBehaviour
{
    [SerializeField] private StageData _stageData;

    [SerializeField] private CameraSequencePlayer _cameraSequencePlayer;

    [SerializeField] private WeaponController _weaponController;
    [SerializeField] private ShootingSystem _shootingSystem;
    [SerializeField] private UpgradeDatabase _upgradeDatabase;
    [SerializeField] private UpgradeUI _upgradeUI;

    [SerializeField] private EnemySpawnPointManager _enemySpawnPointManager;
    private BattleEnemySpawner _battleEnemySpawner;

    private GameStateMachine _gameStateMachine;

    private int _currentStepIndex;

    private void Awake()
    {
        _gameStateMachine = new GameStateMachine();

        _battleEnemySpawner = new BattleEnemySpawner(_enemySpawnPointManager);
    }

    private void Start()
    {
        _currentStepIndex = 0;

        ChangeToCurrentStep();
    }
    private void Update()
    {
        _gameStateMachine.Update();
    }

    private void ChangeToCurrentStep()
    {
        StageStep step = _stageData.Steps[_currentStepIndex];

        switch (step.PhaseType)
        {
            case GamePhase.Start:

                StartPhase startPhase = new StartPhase();

                startPhase.Completed += MoveToNextStep;

                _gameStateMachine.ChangePhase(startPhase);

                break;


            case GamePhase.CameraMove:

                CameraMovePhase cameraMovePhase = new CameraMovePhase(step.CameraSequenceData, _cameraSequencePlayer);

                cameraMovePhase.Completed += MoveToNextStep;

                _gameStateMachine.ChangePhase(cameraMovePhase);

                break;


            case GamePhase.Battle:

                BattlePhase battlePhase = new BattlePhase(step.BattleEnemyGroup, _battleEnemySpawner);

                battlePhase.Completed += MoveToNextStep;        //BattlePhaseの撃破完了イベントにMoveToNextStep()を追加

                _gameStateMachine.ChangePhase(battlePhase);

                break;



            case GamePhase.Event:

                //Upgrade候補を生成するためのGeneratorを作成
                UpgradeChoiceGenerator upgradeChoiceGenerator = new UpgradeChoiceGenerator(_upgradeDatabase);

                UpgradePhase upgradePhase = new UpgradePhase(_weaponController, upgradeChoiceGenerator, _shootingSystem);

                upgradePhase.Completed += MoveToNextStep;

                _gameStateMachine.ChangePhase(upgradePhase);

                Debug.Log("EventPhase変更完了");

                //UpgradeUIにUpgradePhaseを渡す
                _upgradeUI.SetUp(upgradePhase);

                Debug.Log("UpgradeUI.Setup完了");

                break;

                //case GamePhase.BossBattle:
                //    _gameStateMachine.ChangePhase(new BossBattle());
                //    break;

                //case GamePhase.Goal:
                //    _gameStateMachine.ChangePhase(new GoalPhase());
                //    break;

        }
    }

    public void MoveToNextStep()
    {
        _currentStepIndex++;

        if (_currentStepIndex >= _stageData.Steps.Length)
        {
            Debug.Log("ステージ終了");
            return;
        }

        ChangeToCurrentStep();
    }
}
