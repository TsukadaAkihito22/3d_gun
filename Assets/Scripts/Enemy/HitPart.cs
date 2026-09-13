using UnityEngine;

/// <summary> 敵の部位とダメージ倍率を管理するクラス </summary>
public class HitPart : MonoBehaviour
{
    [SerializeField] private HitPartTypes _hitPartType; // ヒットパートの種類を指定するための列挙型

    [SerializeField] private float _damageMultiplier = 1f; // ダメージ倍率を指定するための変数

    //プロパティ
    #region 
    public HitPartTypes hitPartType => _hitPartType;
    public float damageMultiplier => _damageMultiplier;

    #endregion
}
