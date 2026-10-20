using UnityEngine;

public class HealChoice : UpgradeChoice
{
    private readonly PlayerStatus _playerHealth;

    public HealChoice(PlayerStatus playerHealth)
    {
        _playerHealth = playerHealth;
    }

    public override void Apply()
    {
        _playerHealth.Heal();
    }
}
