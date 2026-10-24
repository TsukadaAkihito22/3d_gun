using UnityEngine;
using DG.Tweening;

public class AxController : MonoBehaviour , IDamageable
{
    private Vector3 _playerPos = default;
    private PlayerUIController _playerUIcontroller;
    private PlayerStatus _playerStatus;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _durability = 25f;

    void Start()
    {
        _playerPos = Camera.main.transform.position;

        _playerStatus = FindFirstObjectByType<PlayerStatus>();

        _playerUIcontroller = FindFirstObjectByType<PlayerUIController>();

        StartMovement();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _playerPos, _moveSpeed * Time.deltaTime);

        if (_durability < 0)
        {
            Destroy(gameObject);
        }

        if (Vector3.Distance(transform.position, _playerPos) < 0.5f)
        {
            PlayerTakeDamage();
            Destroy(gameObject);
        }
    }

    private void StartMovement()
    {
        ////Playerの方向を向く
        transform.LookAt(Camera.main.transform.position);

        //縦回転
        transform.DOLocalRotate(
            new Vector3(360f, 0f, 0f),
            0.5f,
            RotateMode.LocalAxisAdd              // 現在の角度を基準に、ローカル軸で加算回転するモード
        )
        .SetEase(Ease.InCirc)                    // 等速で滑らかに回転させる
        .SetLoops(-1, LoopType.Incremental);
    }

    public void TakeDamage(DamageData data)
    {
        _durability -= data.Damage;
    }

    private void PlayerTakeDamage()
    {
        _playerStatus.TakeDamage();
        _playerUIcontroller.PlayUIAnimation();
    }
}
