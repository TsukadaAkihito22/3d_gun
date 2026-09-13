using UnityEngine;

public class BulletHole : MonoBehaviour, IPoolable
{
    [SerializeField] private float _lifeTime = 5f;

    private ObjectPool _objectPool;
    private float _timer = 0;

    public void SetPool(ObjectPool objectPool)
    {
        _objectPool = objectPool;
    }

    private void OnEnable()
    {
        _timer = 0f;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _lifeTime)
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        if (_objectPool == null) return;

        _objectPool.Release(gameObject);
    }
}
