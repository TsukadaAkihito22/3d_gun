using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の生成処理を行うクラス
/// </summary>
public class BattleEnemySpawner
{
    private readonly EnemySpawnPointManager _spawnPointManager;

    public BattleEnemySpawner(EnemySpawnPointManager spawnPointManager)
    {
        _spawnPointManager = spawnPointManager;
    }

    public Enemy[] Spawn(EnemySpawnData spawnData)
    {
        List<Enemy> spawnedEnemies = new List<Enemy>();

        Transform spawnPoint = _spawnPointManager.GetSpawnPoint(spawnData.SpawnPointId);

        if (spawnPoint == null)
        {
            return spawnedEnemies.ToArray();
        }

        for (int i = 0; i < spawnData.SpawnCount; i++)
        {
            Enemy enemy = Object.Instantiate(spawnData.EnemyPrefab, spawnPoint.position, spawnPoint.rotation);

            spawnedEnemies.Add(enemy);
        }
        
        return spawnedEnemies.ToArray();
    }
}
