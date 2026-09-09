using System.Collections;
using UnityEngine;

/// <summary> 現在の武器ステータスを管理するクラス </summary>
public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData _weaponData;    // 武器データを格納する変数

    private int _currentAmmo;    // 現在の弾薬数を格納する変数

    private float _damage;
    private float _shootRange;
    private float _fireRate;
    private int _rayCount;
    private float _spreadAngle;
    private int _magazineSize;
    private float _reloadTime;

    //フラグ
    private bool _isReloading = false;    // リロード中かどうかを判定するフラグ

    //プロパティ
    #region
    public WeaponData WeaponData => _weaponData;
    public int CurrentAmmo => _currentAmmo;

    public float Damage => _damage;
    public float ShootRange => _shootRange;
    public float FireRate => _fireRate;
    public int RayCount => _rayCount;
    public float SpreadAngle => _spreadAngle;
    public float ReloadTime => _reloadTime;
    public int MagazineSize => _magazineSize;
    public bool IsReloading => _isReloading;
    public bool CanShoot => !_isReloading && _currentAmmo > 0;    // 射撃可能かどうかを判定するプロパティ

    public bool CanReload => !_isReloading && _currentAmmo < _magazineSize;    // リロード可能かどうかを判定するプロパティ
    #endregion

    private void Awake()
    {
        _damage = _weaponData.Damage;
        _shootRange = _weaponData.ShootRange;
        _fireRate = _weaponData.FireRate;
        _rayCount = _weaponData.RayCount;
        _spreadAngle = _weaponData.SpreadAngle;
        _magazineSize = _weaponData.MagazineSize;
        _reloadTime = _weaponData.ReloadTime;

        _currentAmmo = _weaponData.MagazineSize;    // 初期弾薬数をマガジンサイズに設定
    }

    public void ConsumeAmmo()
    {
        if(!CanShoot) return;

        _currentAmmo--;

        if (_currentAmmo == 0)
        {
            StartReload();
        }
    }


    public void UpgradeWeapon(WeaponUpgrade upgrade)
    {
        _damage += upgrade.DamagedAdd;
        _shootRange += upgrade.ShootRangeAdd;
        _fireRate += upgrade.FireRateSub;
        _rayCount += upgrade.RayCountAdd;
        _spreadAngle -= upgrade.SpreadAngleSub;
        _magazineSize += upgrade.MagazineSizeAdd;
        _reloadTime -= upgrade.ReloadTimeSub;

        // 現在の弾薬数をマガジンサイズに合わせて調整
        if (_currentAmmo < _magazineSize)
        {
            _currentAmmo = _magazineSize;
        }
    }

    public void StartReload()
    {
        if (!CanReload) return;

        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        _isReloading = true;

        yield return new WaitForSeconds(_reloadTime);

        _currentAmmo = _magazineSize;

        _isReloading = false;
    }
}
