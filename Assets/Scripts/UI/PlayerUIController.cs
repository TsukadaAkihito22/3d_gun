using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerUIController : MonoBehaviour
{
    [Header("オブジェクト")]
    [SerializeField] private CanvasGroup objectA;
    //[SerializeField] private Image objectBImage;       // Imageコンポーネント（Filled設定したもの）
    //[SerializeField] private CanvasGroup objectBAlpha;  // オブジェクトBのCanvasGroup

    [SerializeField] private GameObject[] _lifes;

    [Header("設定")]
    [SerializeField] private float aFadeDuration = 0.5f;   // Aが消える時間
    [SerializeField] private float bFadeDuration = 1.5f;   // Bが消える時間

    //[Header("参照")]
    //[SerializeField] private PlayerStatus _playerStatus;

    private Sequence uiSequence;

    ///<summary> ライフ変動処理 </summary>
    public void FluctuationLife(int currentLife)
    {
        int lifeCount = currentLife - 1;

        switch (currentLife)
        {
            case 3:
                _lifes[lifeCount].SetActive(true);
                break;

            case 2:
                _lifes[currentLife].SetActive(false);
                _lifes[lifeCount].SetActive(true);
                break;

            case 1:
                _lifes[currentLife].SetActive(false);
                break;

            case 0:
                _lifes[currentLife].SetActive(false);
                break;
        }
    }

    /// <summary>
    /// UIが呼び出されたときに実行するメソッド
    /// </summary>
    public void PlayUIAnimation()
    {
        // 実行中のアニメーションがあれば破棄して初期化
        uiSequence?.Kill();
        uiSequence = DOTween.Sequence();

        //初期状態
        objectA.alpha = 1f;

        // オブジェクトB：真ん中に位置したまま、表示量を0（非表示）に。アルファは1。
        //objectBImage.fillAmount = 0f;
        //objectBAlpha.alpha = 1f;

        //アニメーション
        // 【同時実行】Aは0.5秒かけて透明になる
        uiSequence.Join(objectA.DOFade(0f, aFadeDuration).SetEase(Ease.Linear));

        // 【同時実行】Bは上から下にかけて0.1秒でスッと現れる
        //uiSequence.Join(objectBImage.DOFillAmount(1f, bAppearDuration).SetEase(Ease.OutQuad));

        //// 【シークエンス追加】Bが表示し終えてから（0.1秒の後）、1.5秒かけて透明になる
        //uiSequence.Append(objectBAlpha.DOFade(0f, bFadeDuration).SetEase(Ease.Linear));
    }
}