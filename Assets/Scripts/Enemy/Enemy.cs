using System;
using UnityEngine;

/// <summary> 一般的な敵の情報やイベント通知等を管理するクラス </summary>
public class Enemy : MonoBehaviour, IDamageable
{
    [Header("HP")]
    [SerializeField] private float _maxHP = 100f; // 敵の体力を指定するための変数

    [Header("移動")]
    [SerializeField] private float _walkSpeed = 2f;
    [SerializeField] private float _runSpeed = 5f;

    [Header("攻撃")]
    [SerializeField] private float _attackArea = 1f;

    [Header("参照")]
    [SerializeField] private Animator _normalZombieAnimator;
    private PlayerUIController _playerUIcontroller;

    [SerializeField] private int _modeNum = 0;

    private PlayerStatus _playerStatus;

    private static readonly int IsHalfHP = Animator.StringToHash("isHalfHP");

    private static readonly int IsDead = Animator.StringToHash("isDead");

    private static readonly int IsClosePlayer = Animator.StringToHash("isClosePlayer");

    private float _currentHP = 0f;
    private bool _isRunnig = false;
    private bool _isDead = false;
    private Transform _playerTransform;

    public event Action<Enemy> EnemyDied;      //敵死亡通知イベント

    void Awake()
    {
        _currentHP = _maxHP;
    }

    void Start()
    {
        _playerTransform = Camera.main.transform;

        _playerStatus = FindFirstObjectByType<PlayerStatus>();

        _playerUIcontroller = FindFirstObjectByType<PlayerUIController>();

        if (_modeNum == 1)
        {
            _normalZombieAnimator.SetBool(IsHalfHP, true);
            _isRunnig = true;
        }
    }

    public void TakeDamage(DamageData damageData)
    {
        _currentHP -= damageData.Damage;

        Debug.Log(
            $"敵に {damageData.Damage} ダメージ。" +
            $" 部位: {damageData.HitPartTypes}" +
            $" 残りHP: {_currentHP}"
        );

        if (_currentHP <= _maxHP / 2)
        {
            _normalZombieAnimator.SetBool(IsHalfHP, true);
            _isRunnig = true;
        }

        //死亡処理(変更予定)
        if (_currentHP <= 0)
        {
            _isDead = true;
            _normalZombieAnimator.SetBool(IsDead, true);
            Destroy(gameObject, 1f);
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, _playerTransform.position);

        bool isClose = distance <= _attackArea;

        _normalZombieAnimator.SetBool(IsClosePlayer, isClose);

        if (!isClose)
        {
            MoveToPlayer();
        }
    }

    private void MoveToPlayer()
    {
        if (_isDead) return;

        Vector3 direction = _playerTransform.position - transform.position;

        direction.y = 0f;

        if (direction.sqrMagnitude <= 0.001f)
        {
            return;
        }

        direction.Normalize();

        float speed = _isRunnig ? _runSpeed : _walkSpeed;

        transform.position += direction * speed * Time.deltaTime;

        transform.forward = direction;
    }

    public void OnAttackHit()
    {
        Debug.Log("負傷しました");
        _playerStatus.TakeDamage();
        _playerUIcontroller.PlayUIAnimation();
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
