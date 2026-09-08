using UnityEngine;

/// <summary> 敵の部位の種類を定義する列挙型 </summary>
public enum HitPartTypes
{
    /// <summary> 体(頭と弱点以外) </summary>
    Body,
    /// <summary> 頭 </summary>
    Head,
    /// <summary> 弱点 </summary>
    WeakPoint,
    /// <summary> ダメージを受けない部位 </summary>
    Immune
}
