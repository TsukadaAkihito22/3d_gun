using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPointManager : MonoBehaviour
{
    [SerializeField] private EnemySpawnPoint[] _spawnPoints;

    private Dictionary<string, EnemySpawnPoint> _spawnPointDictionary;

    private void Awake()
    {
        _spawnPointDictionary = new Dictionary<string, EnemySpawnPoint>();

        foreach (EnemySpawnPoint spawnPoint in _spawnPoints)
        {
            if (spawnPoint == null) continue;

            if (_spawnPointDictionary.ContainsKey(spawnPoint.SpawnPointId))
            {
                Debug.Log($"IDが重複しています : {spawnPoint.SpawnPointId}");

                continue;
            }

            _spawnPointDictionary.Add(spawnPoint.SpawnPointId, spawnPoint);
        }
    }

    public Transform GetSpawnPoint(string pointId)
    {
        if (_spawnPointDictionary.TryGetValue(pointId, out EnemySpawnPoint spawnPoint))
        {
            return spawnPoint.transform;
        }

        Debug.Log($"SpawnPointが見つかりませんでした : {pointId}");

        return null;
    }
}
