using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] float _shootRange = 100f;
    [SerializeField] float _damage = 10f;


    void Update()
    {
        // マウスの左クリックで射撃
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    /// <summary> 射撃処理 </summary>
    void Shoot()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);        // マウスの位置からレイを飛ばす

        if (Physics.Raycast(ray, out RaycastHit hit, _shootRange))       // 射程範囲内でレイが何かに当たった場合
        {
            //Debug.Log($"命中地点: {hit.point}");        // 命中地点をログに出力、弾痕実装時に欲しいかも

            ProcessHit(hit);
        }
    }

    /// <summary> 命中したオブジェクトの処理を行う </summary>
    /// <param name="hit"> 命中したオブジェクトの情報 </param>
    private void ProcessHit(RaycastHit hit)
    {
        HitPart hitPart = hit.collider.GetComponent<HitPart>();    // 命中したオブジェクトがHitPartを持っているか確認

        if (hitPart == null) return;

        // ダメージデータを作成
        DamageData damageData = new DamageData(_damage * hitPart.damageMultiplier, hit.point, hitPart.hitPartType);

        IDamageable damageable = hitPart.GetComponentInParent<IDamageable>();    // 命中したオブジェクトがIDamageableを実装しているか確認

        if (damageable != null)
        {
            damageable.TakeDamage(damageData);     // ダメージを与える
        }
    }
}
