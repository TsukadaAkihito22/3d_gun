using System;
using System.Collections.Generic;
using UnityEngine;

public class BattlePhase : IGamePhase
{
    private readonly int _requiredDefeatCount;      //コンストラクタでのみ値の代入を受け付けたいのでreadonly
    private readonly BattleEnemyGroup _enemyGroup;
    private readonly BattleEnemySpawner _enemySpawner;

    private int _currentWaveIndex;
    //private Enemy[] _currentWaveEnemies;
    private List<Enemy> _currentWaveEnemies;

    private int _defeatCount;

    private float _spawnTimer;      //次の敵が出るまでの時間
    private int _spawnDataIndex;    //今何番目のSpawnDataを処理しているか
    private bool _isSpawning;       //敵を出現させてるなうか否か

    public event Action Completed;      //撃破完了通知

    public BattlePhase(BattleEnemyGroup enemyGroup, BattleEnemySpawner enemySpawner)
    {
        _enemyGroup = enemyGroup;
        _enemySpawner = enemySpawner;
    }

    public void Enter()
    {
        _defeatCount = 0;
        _currentWaveIndex = 0;

        StartCurrentWave();
    }

    
    private void StartCurrentWave()
    {
        BattleWave[] waves = _enemyGroup.Waves;

        if (_currentWaveIndex >= waves.Length)
        {
            CompleteBattle();
            return;
        }

        _currentWaveEnemies = new List<Enemy>();

        _spawnDataIndex = 0;
        _spawnTimer = 0f;
        _isSpawning = true;

        Debug.Log($"Battleフェーズに入りました");
    }

    

    public void Update()
    {
        if (!_isSpawning) return;

        BattleWave wave = _enemyGroup.Waves[_currentWaveIndex];

        EnemySpawnData[] spawnDatas = wave.SpawnDatas;

        if (_spawnDataIndex >= spawnDatas.Length)
        {
            _isSpawning = false;

            CheckWaveClear();

            return;
        }

        _spawnTimer += Time.deltaTime;

        EnemySpawnData spawnData = spawnDatas[_spawnDataIndex];

        if (_spawnTimer < spawnData.SpawnDelay)
        {
            return;
        }

        SpawnEnemy(spawnData);

        _spawnTimer = 0f;

        _spawnDataIndex++;
    }


    private void SpawnEnemy(EnemySpawnData spawnData)
    {
        Enemy[] spawnedEnemies = _enemySpawner.Spawn(spawnData);

        foreach (Enemy enemy in spawnedEnemies)
        {
            _currentWaveEnemies.Add(enemy);

            RegisterEnemy(enemy);
        }
    }



    public void Exit()
    {
        if (_currentWaveEnemies == null) return;

        foreach (Enemy enemy in _currentWaveEnemies)
        {
            UnRegisterEnemy(enemy);
        }

        _currentWaveEnemies.Clear();
    }

    public void RegisterEnemy(Enemy enemy)
    {
        if (enemy == null) return;

        enemy.EnemyDied += OnEnemyDied;
    }

    public void UnRegisterEnemy(Enemy enemy)
    {
        if (enemy == null) return;

        enemy.EnemyDied -= OnEnemyDied;
    }

    public void OnEnemyDied(Enemy enemy)
    {
        _defeatCount++;

        Debug.Log("敵を撃破しました");

        UnRegisterEnemy(enemy);

        _currentWaveEnemies.Remove(enemy);

        CheckWaveClear();
    }

    private void CheckWaveClear()
    {
        if (_isSpawning)
        {
            return;
        }

        if (_currentWaveEnemies.Count > 0)
        {
            return;
        }

        MoveToNextWave();
    }

    private void MoveToNextWave()
    {
        _currentWaveIndex++;

        if (_currentWaveIndex >= _enemyGroup.Waves.Length)
        {
            CompleteBattle();
            return;
        }

        StartCurrentWave();
    }

    private void CompleteBattle()
    {
        Debug.Log("撃破完了！");

        Completed?.Invoke();
    }

    
}
