using UnityEngine;

/// <summary> 武器のデータを管理するクラス </summary>
[CreateAssetMenu(fileName = "WeaponData", menuName = "Game/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("基本性能")]
    [SerializeField] private float _damage = 10f; // 武器の基本ダメージ
    [SerializeField] private float _shootRange = 100f; // 武器の射程距離
    [SerializeField] private float _fireRate = 0.1f; // 武器の連射速度

    [Header("射撃性能")]
    [SerializeField] private int _rayCount = 1; // レイの本数
    [SerializeField] private float _spreadAngle = 0f; // レイの拡散角度

    [Header("マガジン")]
    [SerializeField] private int _magazineSize = 30; // マガジンの容量
    [SerializeField] private float _reloadTime = 2f; // リロード時間

    [Header("効果音")]
    [SerializeField] private AudioClip _shotSound; // 射撃音
    [SerializeField] private AudioClip _reloadSound; // リロード音

    // プロパティ
    #region
    public float Damage => _damage;
    public float ShootRange => _shootRange;
    public float FireRate => _fireRate;

    public int RayCount => _rayCount;
    public float SpreadAngle => _spreadAngle;

    public float ReloadTime => _reloadTime;
    public int MagazineSize => _magazineSize;

    public AudioClip ShotSound => _shotSound;
    public AudioClip ReloadSound => _reloadSound;
    #endregion

}
