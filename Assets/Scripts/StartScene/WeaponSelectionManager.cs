using UnityEngine;

public class WeaponSelectionManager : MonoBehaviour
{
    public static WeaponSelectionManager Instance { get; private set; }

    private WeaponType _selectedWeaponType;
    private WeaponData _selectedWeaponData;

    public WeaponType SelectedWeaponType => _selectedWeaponType;
    public WeaponData SelectedWeaponData => _selectedWeaponData;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }


    public void SelectWeapon(
        WeaponType weaponType,
        WeaponData weaponData)
    {
        _selectedWeaponType = weaponType;
        _selectedWeaponData = weaponData;

        Debug.Log(
            $"武器を選択しました : " +
            $"{weaponType} / {weaponData.name}"
        );
    }
}
