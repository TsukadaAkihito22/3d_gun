using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponSelectButton : MonoBehaviour
{
    [Header("選択する武器")]
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private WeaponData _weaponData;

    [Header("遷移先")]
    [SerializeField] private string _gameSceneName;


    public void SelectWeapon()
    {
        WeaponSelectionManager.Instance.SelectWeapon(
            _weaponType,
            _weaponData
        );

        SceneManager.LoadScene(_gameSceneName);
    }
}
