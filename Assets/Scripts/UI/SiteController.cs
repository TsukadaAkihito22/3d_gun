using UnityEngine;

public class SiteController : MonoBehaviour
{
    [SerializeField] private RectTransform cursorImageTransform; // 追従させるImageのRectTransform

    void Start()
    {
        // マウスカーソルを非表示にする
        Cursor.visible = false;
    }

    void Update()
    {
        // マウスのスクリーン座標を取得し、UIの座標に設定する
        if (cursorImageTransform != null)
        {
            cursorImageTransform.position = Input.mousePosition;
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        // ゲーム画面外に出たり戻ったりした際のカーソル再非表示処理
        if (hasFocus)
        {
            Cursor.visible = false;
        }
    }
}
