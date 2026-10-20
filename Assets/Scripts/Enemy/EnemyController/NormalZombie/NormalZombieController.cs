using UnityEngine; // UnityEditor.Animations の using は削除します

public class NormalZombieController : MonoBehaviour
{
    [Header("参照")]
    // AnimatorController から RuntimeAnimatorController に変更
    [SerializeField] private RuntimeAnimatorController _normalZombieAnimation;
}
