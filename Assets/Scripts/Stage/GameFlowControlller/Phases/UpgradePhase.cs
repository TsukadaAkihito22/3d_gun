using System;
using UnityEngine;

public class UpgradePhase : IGamePhase
{
    private readonly WeaponController _weaponController;
    private readonly UpgradeChoiceGenerator _generator;
    private readonly ShootingSystem _shootingSystem;

    private WeaponUpgrade[] _choices;

    private bool[] _selected;

    private int _selectedCount;
    private const int MaxSelectionCount = 2;

    public event Action Completed;

    public UpgradePhase(WeaponController weaponController, UpgradeChoiceGenerator upgradeChoiceGenerator, ShootingSystem shootingSystem)
    {
        _weaponController = weaponController;
        _generator = upgradeChoiceGenerator;
        _shootingSystem = shootingSystem;
    }

    public void Enter()
    {
        Debug.Log("Eventフェーズに入りました");

        _selectedCount = 0;
        GenerateChoices();

        _shootingSystem.ChangeEventingFlag(true);

        //選択肢UIを表示する
    }

    public void Update()
    {
        
    }

    public void Exit()
    {
        //選択肢UIを非表示にする
        _shootingSystem.ChangeEventingFlag(false);
    }

    private void GenerateChoices()
    {
        Weapon currentWeapon = _weaponController.CurrentWeapon;

        _choices = _generator.GenerateWeaponUpgrades(currentWeapon.WeaponType, 4);

        foreach (WeaponUpgrade upgrade in _choices)
        {
            Debug.Log($"Upgrade候補: {upgrade.UpgradeName}");
        }

        _selected = new bool[_choices.Length];
    }

    public void SelectUpgrade(int index)
    {
        if (_selectedCount >= MaxSelectionCount)
        {
            return;
        }

        if (index < 0 || index >= _choices.Length)
        {
            return;
        }

        if (_selected[index])
        {
            Debug.Log("選択済みです");
            return;
        }

        WeaponUpgrade selectedUpgrade = _choices[index];

        Weapon currentWeapon = _weaponController.CurrentWeapon;

        currentWeapon.UpgradeWeapon(selectedUpgrade);

        _selected[index] = true;
        _selectedCount++;

        Debug.Log($"Upgrade選択 : {selectedUpgrade.UpgradeName}");

        if (_selectedCount >= MaxSelectionCount)
        {
            Complete();
        }
    }

    public WeaponUpgrade[] GetChoices()
    {
        return _choices;
    }

    private void Complete()
    {
        Completed?.Invoke();
    }
}