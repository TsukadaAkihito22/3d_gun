using UnityEngine;

/// <summary> Upgradeの選択し全体を管理するクラス </summary>
public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private UpgradeChoiceUI[] _choiceUIs;
    [SerializeField] private GameObject _panel;

    private UpgradePhase _upgradePhase;

    private void Awake()
    {
        _panel.SetActive(false);
    }

    public void SetUp(UpgradePhase upgradePhase)
    {
        Debug.Log("Setupします");

        if (_upgradePhase != null)
        {
            _upgradePhase.Completed -= OnUpgradeCompleted;
        }

        _upgradePhase = upgradePhase;

        _upgradePhase.Completed += OnUpgradeCompleted;

        WeaponUpgrade[] choices = _upgradePhase.GetChoices();

        Debug.Log($"候補数 : {choices.Length}");
        Debug.Log($"UI数 : {_choiceUIs.Length}");

        for (int i = 0; i < _choiceUIs.Length; i++)
        {
            Debug.Log($"ChoiceUI {i} をSetupします");
            _choiceUIs[i].SetUp(choices[i], i, this);
        }

        _panel.SetActive(true);
        Debug.Log("UpgradeUIをActiveにしました");
    }


    public void SelectUpgrade(int index)
    {
        _upgradePhase.SelectUpgrade(index);

        _choiceUIs[index].SetSelected();
    }

    private void OnUpgradeCompleted()
    {
        Close();
    }

    public void Close()
    {
        _panel.SetActive(false);
    }

    private void OnDestroy()
    {
        if (_upgradePhase != null)
        {
            _upgradePhase.Completed -= OnUpgradeCompleted;
        }
    }
}
