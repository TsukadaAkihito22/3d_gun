using UnityEngine;

public class WeaponUpgradeChoice : UpgradeChoice
{
    private readonly Weapon _weapon;
    private readonly WeaponUpgrade _upgrade;

    public WeaponUpgradeChoice(Weapon weapon, WeaponUpgrade weaponUpgrade)
    {
        _weapon = weapon;
        _upgrade = weaponUpgrade;
    }

    public override void Apply()
    {
        _weapon.UpgradeWeapon(_upgrade);
    }
}
