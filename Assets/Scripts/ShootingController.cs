using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; // 弾丸のプレハブ
    [SerializeField] private float bulletSpeed = 20f;  // 弾のスピード
    [SerializeField] private float fireInterval = 0.1f; // 連射の間隔（秒）

    private float fireTimer;

    void Update()
    {
        // 左クリックを押している間
        if (Input.GetMouseButton(0))
        {
            fireTimer += Time.deltaTime;

            // 一定間隔ごとに発射
            if (fireTimer >= fireInterval)
            {
                Shoot();
                fireTimer = 0f;
            }
        }
        else
        {
            // クリックを離したらタイマーをリセットして即座に撃てるようにする
            fireTimer = fireInterval;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null) return;

        // マウスの位置からレイ（光線）を飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 画面上のマウス位置にある3D空間の座標を計算
        Vector3 targetPosition;
        if (Physics.Raycast(ray, out hit))
        {
            targetPosition = hit.point; // ヒットした場所
        }
        else
        {
            targetPosition = ray.GetPoint(100f); // 何にも当たらない場合は100m先
        }

        // 弾丸を生成
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // 発射方向を計算
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Rigidbodyを取得して力を加える、または速度を設定
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }
    }
}
