using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private string _spawnPointId;

    public string SpawnPointId => _spawnPointId;
}
