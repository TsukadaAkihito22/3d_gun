using UnityEngine;
using System.Collections;

public class BulletTrail : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer; // 弾道を描画するためのLineRenderer
    [SerializeField] private float _travelTime = 0.05f; // 弾道が消えるまでの時間

    /// <summary> 弾道を描画するメソッド </summary>
    public void Play(Vector3 startPosition, Vector3 endPosition)
    {
        StartCoroutine(ShowTrail(startPosition, endPosition));
    }


    /// <summary> 弾道を描画するコルーチン </summary>
    private IEnumerator ShowTrail(Vector3 startPosition, Vector3 endPosition)
    {
        float elpsedTime = 0f;

        while (elpsedTime < _travelTime)
        {
            elpsedTime += Time.deltaTime;

            float t = elpsedTime / _travelTime;

            Vector3 currentPosition = Vector3.Lerp(startPosition, endPosition, t);

            _lineRenderer.SetPosition(0, startPosition);

            _lineRenderer.SetPosition(1, currentPosition);

            yield return null;
        }

        Destroy(gameObject);
    }
}
