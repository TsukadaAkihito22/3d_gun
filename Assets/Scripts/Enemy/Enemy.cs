using System;
using System.Security.Cryptography;
using UnityEngine;

/// <summary> 一般的な敵の情報やイベント通知等を管理するクラス </summary>
public class Enemy : MonoBehaviour, IDamageable
{
    [Header("HP")]
    [SerializeField] private float _maxHP = 100f; // 敵の体力を指定するための変数

    [Header("移動")]
    [SerializeField] private float _walkSpeed = 2f;
    [SerializeField] private float _rageSpeed = 3f;
    [SerializeField] private float _runSpeed = 5f;

    [Header("攻撃")]
    [SerializeField] private float _attackArea = 1f;

    [Header("参照")]
    [SerializeField] private Animator _normalZombieAnimator;
    [SerializeField] private Animator _throwZombieAnimator;
    [SerializeField] private Animator _mutantAnimator;
    [SerializeField] private GameObject _throwObj;
    private PlayerUIController _playerUIcontroller;

    [SerializeField] private int _modeNum = 0;

    private PlayerStatus _playerStatus;

    private static readonly int IsHalfHP = Animator.StringToHash("isHalfHP");
    private static readonly int IsDead = Animator.StringToHash("isDead");
    private static readonly int IsClosePlayer = Animator.StringToHash("isClosePlayer");
    private static readonly int IsAttack = Animator.StringToHash("isAttack");
    private static readonly int IsLevel1 = Animator.StringToHash("isLevel1");
    private static readonly int IsLevel2 = Animator.StringToHash("isLevel2");

    private float _currentHP = 0f;
    private bool _isRunnig = false;
    private bool _isRaging = false;
    private bool _isDead = false;
    private Transform _playerTransform;

    private float _attackTimer = 0f;
    private int _attackSpan = 0;

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

        // modeNumが1の場合の初期化
        if (_modeNum == 1)
        {
            if (_normalZombieAnimator != null) _normalZombieAnimator.SetBool(IsHalfHP, true);
            _isRunnig = true;
        }

        // modeNumが2の場合の初期化
        if (_modeNum == 2)
        {
            _attackSpan = UnityEngine.Random.Range(1, 2);
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

        if (_modeNum == 4)
        {
            if (_currentHP <= _maxHP * 0.333f)
            {
                _normalZombieAnimator.SetBool(IsLevel2, true);
                _isRunnig = true;
                Avoid();
            }
            else if (_currentHP <= _maxHP * 0.666f)
            {
                _normalZombieAnimator.SetBool(IsLevel1, true);
                _isRaging = true;
                Avoid();
            }
        }

        //体力半減処理
        if (_modeNum == 0 && _currentHP <= _maxHP / 2)
        {
            if (_normalZombieAnimator != null) _normalZombieAnimator.SetBool(IsHalfHP, true);
            _isRunnig = true;
        }

        //死亡処理
        if (_currentHP <= 0)
        {
            _isDead = true;

            // モードに応じて死亡アニメーションを切り替え
            if (_modeNum == 2 && _throwZombieAnimator != null)
            {
                _throwZombieAnimator.SetBool(IsDead, true);
            }
            else if (_normalZombieAnimator != null)
            {
                _normalZombieAnimator.SetBool(IsDead, true);
            }

            Destroy(gameObject, 1f);
        }
    }

    void Update()
    {
        if (_isDead) return;

        // プレイヤーの方向を向く（投擲モードでもプレイヤーを向かせるため）
        LookAtPlayer();

        if (_modeNum == 2)
        {
            _attackTimer += Time.deltaTime;

            if (_attackTimer > _attackSpan)
            {
                if (_throwZombieAnimator != null) _throwZombieAnimator.SetBool(IsAttack, true);
                _attackSpan = UnityEngine.Random.Range(6, 8);
                _attackTimer = 0f;
            }
        }
        else
        {
            float distance = Vector3.Distance(transform.position, _playerTransform.position);
            bool isClose = distance <= _attackArea;

            if (_normalZombieAnimator != null) _normalZombieAnimator.SetBool(IsClosePlayer, isClose);

            if (!isClose)
            {
                MoveToPlayer();
            }
        }
    }

    private void LookAtPlayer()
    {
        Vector3 direction = _playerTransform.position - transform.position;
        direction.y = 0f;
        if (direction.sqrMagnitude > 0.001f)
        {
            direction.Normalize();
            transform.forward = direction;
        }
    }

    private void MoveToPlayer()
    {
        Vector3 direction = _playerTransform.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude <= 0.001f) return;

        direction.Normalize();
        float speed = _isRunnig ? _runSpeed : _isRaging ? _rageSpeed : _walkSpeed;
        transform.position += direction * speed * Time.deltaTime;
    }

    public void OnAttackHit()
    {
        Debug.Log("負傷しました");
        _playerStatus.TakeDamage();
        _playerUIcontroller.PlayUIAnimation();
    }

    public void OnThrowAttack()
    {
        Debug.Log("投げました");
        if (_throwObj != null)
        {
            Instantiate(_throwObj, new Vector3(transform.position.x + 1, transform.position.y + 2, transform.position.z), Quaternion.identity);
        }
        if (_throwZombieAnimator != null)
        {
            _throwZombieAnimator.SetBool(IsAttack, false);
        }
    }

    private void Avoid()
    {
        int avoidDistance = UnityEngine.Random.Range(1, 3);
        transform.position = new Vector3(transform.position.x,
                                        transform.position.y,
                                        transform.position.z + (UnityEngine.Random.Range(0, 2) == 1 ? -avoidDistance : avoidDistance));
    }

    private void OnDestroy()
    {
        Die();
    }

    public void Die()
    {
        EnemyDied?.Invoke(this); // Null条件演算子で安全に呼び出し
    }
}
