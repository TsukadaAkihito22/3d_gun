using UnityEngine;

[CreateAssetMenu(fileName = "WeaponUpgrade", menuName = "Game/Weapon Upgrade")]
public class WeaponUpgrade : ScriptableObject
{
    [Header("基本情報")]
    [SerializeField] private string _upgradeName;
    [SerializeField] private string _description;

    [Header("強化値")]
    [SerializeField] private float _damagedAdd;
    [SerializeField] private float _shootRangeAdd;
    [SerializeField] private float _fireRateSub;
    [SerializeField] private int _rayCountAdd;
    [SerializeField] private float _spreadAngleSub;
    [SerializeField] private int _magazineSizeAdd;
    [SerializeField] private float _reloadTimeSub;

    // プロパティ
    #region
    public string UpgradeName => _upgradeName;
    public string Description => _description;

    public float DamagedAdd => _damagedAdd;
    public float ShootRangeAdd => _shootRangeAdd;
    public float FireRateSub => _fireRateSub;
    public int RayCountAdd => _rayCountAdd;
    public float SpreadAngleSub => _spreadAngleSub;
    public int MagazineSizeAdd => _magazineSizeAdd;
    public float ReloadTimeSub => _reloadTimeSub;
    #endregion
}
