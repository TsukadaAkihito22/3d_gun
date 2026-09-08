using UnityEngine;

/// <summary> ダメージを受けるオブジェクトが実装するインターフェース </summary>
public interface IDamageable
{
    void TakeDamage(DamageData damageData);
}
