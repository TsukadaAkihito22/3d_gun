using UnityEngine;

/// <summary> 現在の武器の情報を管理するクラス </summary>
public class WeaponController : MonoBehaviour
{
    [SerializeField] private Weapon _currentWeapon;
    
    //プロパティ
    public Weapon CurrentWeapon => _currentWeapon;

    private void Start()
    {
        ApplySelectedWeapon();
    }


    /// <summary>
    /// 武器選択シーンで選択された武器を現在のWeaponに反映する
    /// </summary>
    private void ApplySelectedWeapon()
    {
        WeaponSelectionManager selectionManager = WeaponSelectionManager.Instance;

        if (selectionManager == null)
        {
            Debug.LogError("WeaponSelectionManagerが存在しません。");
            return;
        }

        if (selectionManager.SelectedWeaponData == null)
        {
            Debug.LogError("選択されたWeaponDataが存在しません。");
            return;
        }

        _currentWeapon.SetWeaponData(
            selectionManager.SelectedWeaponType,
            selectionManager.SelectedWeaponData
        );

        Debug.Log(
            $"現在の武器を変更しました : " +
            $"{selectionManager.SelectedWeaponType}"
        );
    }

    /// <summary> 現在の武器をアップグレードする </summary>
    public void UpgradeCurrentWeapon(WeaponUpgrade upgrade)
    {
        _currentWeapon.UpgradeWeapon(upgrade);
    }
}
