using UnityEngine;

[System.Serializable]
public class BattleWave
{
    [SerializeField] private EnemySpawnData[] _spawnDatas;

    public EnemySpawnData[] SpawnDatas => _spawnDatas;
}
