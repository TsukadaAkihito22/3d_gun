using System;
using UnityEngine;

/// <summary> 一般的な敵の情報やイベント通知等を管理するクラス </summary>
public class ThrowZombieController : MonoBehaviour, IDamageable
{
    [Header("HP")]
    [SerializeField] private float _maxHP = 100f; // 敵の体力を指定するための変数

    [Header("参照")]
    [SerializeField] private Animator _throwZombieAnimator;
    [SerializeField] private GameObject _throwObj;


    private static readonly int IsDead = Animator.StringToHash("isDead");
    private static readonly int IsAttack = Animator.StringToHash("isAttack");

    private float _currentHP = 0f;
    private float _attackTimer = 0f;
    private int _attackSpan = 0;
    private Transform _playerTransform;

    public event Action<ThrowZombieController> EnemyDied;      //敵死亡通知イベント

    void Awake()
    {
        _currentHP = _maxHP;
    }

    void Start()
    {
        _playerTransform = Camera.main.transform;
        
        _attackSpan = UnityEngine.Random.Range(6, 8);
    }

    public void TakeDamage(DamageData damageData)
    {
        _currentHP -= damageData.Damage;

        Debug.Log(
            $"敵に {damageData.Damage} ダメージ。" +
            $" 部位: {damageData.HitPartTypes}" +
            $" 残りHP: {_currentHP}"
        );
        
        //死亡処理(変更予定)
        if (_currentHP <= 0)
        {
            _throwZombieAnimator.SetBool(IsDead, true);
            Destroy(gameObject, 1f);
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, _playerTransform.position);

        _attackTimer += Time.deltaTime;

        if (_attackTimer > _attackSpan)
        {
            _throwZombieAnimator.SetBool(IsAttack, true);
            _attackSpan = UnityEngine.Random.Range(6, 8);
            _attackTimer = 0f;
        }
    }

    public void OnThrowAttack()
    {
        Debug.Log("投げました");
        Instantiate(_throwObj, this.transform);
        _throwZombieAnimator.SetBool(IsAttack, false);
    }

    private void OnDestroy()
    {

        Die();
    }

    public void Die()
    {
        //Debug.Log($"{gameObject.name}が死亡しました。");

        EnemyDied.Invoke(this);
    }
}
