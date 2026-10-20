using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeDataBase", menuName = "Game/Upgrade Database")]
public class UpgradeDatabase : ScriptableObject
{
    [SerializeField] private List<WeaponUpgrade> _upgrades = new List<WeaponUpgrade>();

    public List<WeaponUpgrade> Upgrades => _upgrades;
}
