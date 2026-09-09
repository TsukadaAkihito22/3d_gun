using UnityEngine;
using System.Collections;

public class BulletTrail : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer; // 弾道を描画するためのLineRenderer
    [SerializeField] private float _travelTime = 0.05f; // 弾道が消えるまでの時間
    [SerializeField] private float _trailLength = 5f; //弾道の長さ


    private void Awake()
    {
        // LineRendererの初期設定
        _lineRenderer.positionCount = 2; // 弾道は2点で構成される
        _lineRenderer.useWorldSpace = true;



        Gradient gradient = new Gradient();

        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.gray, 0f), new GradientColorKey(Color.white, 1f) },
            new GradientAlphaKey[] { new GradientAlphaKey(0f, 0f), new GradientAlphaKey(0.5f, 1f) }
        );

        _lineRenderer.colorGradient = gradient;
    }

    /// <summary> 弾道を描画するメソッド </summary>
    public void Play(Vector3 startPosition, Vector3 endPosition, Vector3? hitPosition)
    {
        StartCoroutine(ShowTrail(startPosition, endPosition, hitPosition));
    }


    /// <summary> 弾道を描画するコルーチン </summary>
    private IEnumerator ShowTrail(Vector3 startPosition, Vector3 endPosition, Vector3? hitPosition)
    {
        Vector3 direction = (endPosition - startPosition).normalized; // 弾道の方向を計算

        float totalDistance = Vector3.Distance(startPosition, endPosition); // 弾道の全体の距離を計算

        float elpsedTime = 0f;

        while (elpsedTime < _travelTime)
        {
            elpsedTime += Time.deltaTime;

            float t = elpsedTime / _travelTime;

            //弾道の先端がどこまで進んだか
            float frontDistance = totalDistance * t;

            //弾道の後端がどこまで進んだか
            float backDistance = Mathf.Max(0f, frontDistance - _trailLength);

            Vector3 backPosition = startPosition + direction * backDistance;

            Vector3 frontPosition = startPosition + direction * frontDistance;

            _lineRenderer.SetPosition(0, backPosition);

            _lineRenderer.SetPosition(1, frontPosition);

            if (hitPosition.HasValue)
            {
                float hitDistance = Vector3.Distance(startPosition, hitPosition.Value);

                if (frontDistance >= hitDistance)
                {
                    break; // 弾道が命中位置に到達したらループを抜ける
                }
            }

            yield return null;
        }

        _lineRenderer.enabled = false; // 弾道を非表示にする

        Destroy(gameObject);
    }
}
