using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary> ボタンの表示と機能を持たせるクラス </summary>
public class UpgradeChoiceUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _upgradeNameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Button _upgradeButton;

    private int _index;
    private UpgradeUI _upgradeUI;

    public void SetUp(WeaponUpgrade upgrade, int index, UpgradeUI upgradeUI)
    {
        _index = index;
        _upgradeUI = upgradeUI;

        _upgradeNameText.text = upgrade.UpgradeName;
        _descriptionText.text = upgrade.Description;

        _upgradeButton.interactable = true;     //押せないやつリセット

        _upgradeButton.onClick.RemoveAllListeners();
        _upgradeButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _upgradeUI.SelectUpgrade(_index);
    }

    public void SetSelected()
    {
        _upgradeButton.interactable = false;
    }
}
