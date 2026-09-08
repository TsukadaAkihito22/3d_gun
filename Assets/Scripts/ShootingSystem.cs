using System;
using UnityEngine;

/// <summary> 射撃システムを管理するクラス </summary>
public class ShootingSystem : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] WeaponController _weaponController;

    [SerializeField] BulletTrail _bulletTrailPrefab;

    [Range(0f, 3f)]
    private float _fireRateTimer = 0f; // 射撃間隔のタイマー

    

    void Update()
    {
        // マウスの左クリックで射撃
        if (Input.GetMouseButton(0) && _fireRateTimer >= _weaponController.CurrentWeapon.FireRate)
        {
            Shoot();
            _fireRateTimer = 0f;
        }

        _fireRateTimer += Time.deltaTime;
    }

    /// <summary> 射撃処理 </summary>
    void Shoot()
    {
        Weapon weapon = _weaponController.CurrentWeapon;    // 現在の武器データを取得

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);        // マウスの位置からレイを飛ばす

        for (int i = 0; i < weapon.RayCount; i++)
        {
            Ray spreadRay = CreateSpreadRay(ray, weapon.SpreadAngle);

            //Vector3 endPosition = GetRayEndPosition(spreadRay, weapon.ShootRange);

            //ShowTrail(spreadRay, endPosition);    // レイを可視化する

            if (Physics.Raycast(spreadRay, out RaycastHit hit, weapon.ShootRange))       // 射程範囲内でレイが何かに当たった場合
            {
                ShowTrail(spreadRay.origin, hit.point);    // レイを可視化する

                ProcessHit(hit, weapon);
            }
            else
            {
                Vector3 endPosition = spreadRay.origin + spreadRay.direction * weapon.ShootRange;    // レイが何にも当たらなかった場合は射程範囲の終点を計算

                ShowTrail(spreadRay.origin, endPosition);    // レイを可視化する
            }
        }
    }

    /// <summary> 命中したオブジェクトの処理を行う </summary>
    /// <param name="hit"> 命中したオブジェクトの情報 </param>
    /// <param name="weapon"> 使用中の武器データ </param>
    private void ProcessHit(RaycastHit hit, Weapon weapon)
    {
        HitPart hitPart = hit.collider.GetComponent<HitPart>();    // 命中したオブジェクトがHitPartを持っているか確認

        if (hitPart == null) return;

        IDamageable damageable = hitPart.GetComponentInParent<IDamageable>();    // 命中したオブジェクトがIDamageableを実装しているか確認

        float damage = weapon.Damage * hitPart.damageMultiplier;    // ダメージ計算

        // ダメージデータを作成
        DamageData damageData = new DamageData(damage, hit.point, hitPart.hitPartType);

        if (damageable != null)
        {
            damageable.TakeDamage(damageData);     // ダメージを与える
        }
    }

    /// <summary> 射撃時のレイの拡散を計算して新しいレイを作成する </summary>
    /// <param name="ray"> 元のレイ </param>
    /// <param name="spreadAngle"> 拡散角度 </param>
    /// <returns> 拡散されたレイ </returns>
    private Ray CreateSpreadRay(Ray ray, float spreadAngle)
    {
        if (spreadAngle <= 0) return ray;

        Vector2 randomPoint = UnityEngine.Random.insideUnitCircle; // ランダムな点を生成 (円の中からランダムな方向をピックアップ)

        float angle = randomPoint.magnitude * spreadAngle;  // ランダムな角度を計算 (中心からどれくらい離すかを決定)

        // ランダムな方向を計算 (元のRayから、指定された角度だけ方向を傾ける)
        Vector3 spreadDirection = Quaternion.AngleAxis(angle, Vector3.Cross(ray.direction, Vector3.up)) * ray.direction;

        float rotation = UnityEngine.Random.Range(0f, 360f);        // ランダムな回転角度を生成 (傾ける方向を360度からランダムに選択)

        spreadDirection = Quaternion.AngleAxis(rotation, ray.direction) * spreadDirection;    // ランダムな回転を適用 (選択した角度分位置を回す)

        // 新しいレイを作成して返す
        return new Ray(ray.origin, spreadDirection);
    }

    
    private void ShowTrail(Vector3 startPosition, Vector3 endPosition)
    {
        BulletTrail bulletTrail = Instantiate(_bulletTrailPrefab, startPosition, Quaternion.identity);
        bulletTrail.Play(startPosition, endPosition);
    }
}
