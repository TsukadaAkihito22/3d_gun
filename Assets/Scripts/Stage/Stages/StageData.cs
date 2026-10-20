using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Game/Stage Data")]
public class StageData : ScriptableObject
{
    [SerializeField] private StageStep[] _steps;

    public StageStep[] Steps => _steps;
}
