using JetBrains.Annotations;
using System;
using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private string _spawnPointId;
    [SerializeField] private int _spawnCount = 1;

    [SerializeField] private float _spawnDelay = 0f;

    public Enemy EnemyPrefab => _enemyPrefab;
    public string SpawnPointId => _spawnPointId;
    public int SpawnCount => _spawnCount;
    public float SpawnDelay => _spawnDelay;
}

[CreateAssetMenu(fileName = "BattleEnemyGroup", menuName = "Game/Battle Enemy Group")]
public class BattleEnemyGroup : ScriptableObject
{
    [Header("敵グループ名")]
    [SerializeField] private string _enemyGroupName;

    [Header("Wave")]
    [SerializeField] private BattleWave[] _waves;


    public string EnemyGroupName => _enemyGroupName;
    public BattleWave[] Waves => _waves;
}
