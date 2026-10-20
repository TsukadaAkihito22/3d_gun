using System.Collections.Generic;
using UnityEngine;

public class UpgradeChoiceGenerator
{
    private readonly UpgradeDatabase _upgradeDatabase;

    public UpgradeChoiceGenerator(UpgradeDatabase upgradeDatabase)
    {
        _upgradeDatabase = upgradeDatabase;
    }

    /// <summary> 現在の武器に応じたアップグレードを4つ抽選する </summary>
    /// <param name="weaponType"> 現在装備している武器 </param>
    /// <param name="choiceCount"> 選ぶ数 </param>
    /// <returns></returns>
    public WeaponUpgrade[] GenerateWeaponUpgrades(WeaponType weaponType, int choiceCount)
    {
        List<WeaponUpgrade> candidates = GetCandidates(weaponType);

        if (candidates.Count < choiceCount)
        {
            Debug.LogWarning("抽選候補が足りません");

            return candidates.ToArray();
        }

        //抽選結果をシャッフル
        ShuffleList(candidates);

        return candidates.GetRange(0, choiceCount).ToArray();
    }

    /// <summary> 現在の武器に応じて抽選対象を取得 </summary>
    public List<WeaponUpgrade> GetCandidates(WeaponType weaponType)
    {
        List<WeaponUpgrade> candidates = new List<WeaponUpgrade>();     //抽選用List

        UpgradeGroup weaponGroup = ConvertToUpgradeGroup(weaponType);   //武器の種類を確認

        foreach (WeaponUpgrade upgrade in _upgradeDatabase.Upgrades)    //所持武器、共通、その他に分類されるUpgradeを抽選用リストに格納
        {
            if (upgrade.UpgradeGroup == weaponGroup ||
                upgrade.UpgradeGroup == UpgradeGroup.UCommon ||
                upgrade.UpgradeGroup == UpgradeGroup.UOther)
            {
                candidates.Add(upgrade);
            }
        }

        return candidates;
    }

    /// <summary> WeaponTypeをUpgradeGroupに変換 </summary>
    private UpgradeGroup ConvertToUpgradeGroup(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.AssaultRifle:
                return UpgradeGroup.UAssaultRifle;

            case WeaponType.Magnum:
                return UpgradeGroup.UMagnum;

            case WeaponType.Shotgun:
                return UpgradeGroup.UShotgun;

            case WeaponType.SubMachinegun:
                return UpgradeGroup.USubmachinegun;

            default:
                Debug.LogError($"未対応のWeaponTypeです : {weaponType}");
                return UpgradeGroup.UCommon;
        }
    }

    /// <summary> Listをランダムにシャッフルする </summary>
    private void ShuffleList(List<WeaponUpgrade> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            WeaponUpgrade temp = list[i];

            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}