using UnityEngine;
using UnityEngine.UIElements;

public class test : MonoBehaviour
{
    [SerializeField] private WeaponController _weaponController;

    [SerializeField] UpgradeDatabase _upgradeDatabase;

    private UpgradePhase _eventPhase;

    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Weapon currentWeapon = _weaponController.CurrentWeapon;

            UpgradeChoiceGenerator generator = new UpgradeChoiceGenerator(_upgradeDatabase);


            //_eventPhase =
            //new UpgradePhase(
            //    _weaponController,
            //    generator,
            //    _shootingSystem
            //);

            _eventPhase.Enter();

            _eventPhase.SelectUpgrade(0);
            _eventPhase.SelectUpgrade(2);
        }
    }
}
