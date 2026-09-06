using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float _hp = 100f; // 敵の体力を指定するための変数

    public void TakeDamage(DamageData damageData)
    {
        _hp -= damageData.Damage;

        Debug.Log(
            $"敵に {damageData.Damage} ダメージ。" +
            $" 部位: {damageData.HitPartTypes}" +
            $" 残りHP: {_hp}"
        );

        //死亡処理(変更予定)
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
