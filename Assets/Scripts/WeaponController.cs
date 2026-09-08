using UnityEngine;

/// <summary> 現在の武器の情報を管理するクラス </summary>
public class WeaponController : MonoBehaviour
{
    [SerializeField] private Weapon _currentWeapon;
    
    //プロパティ
    public Weapon CurrentWeapon => _currentWeapon;
}
