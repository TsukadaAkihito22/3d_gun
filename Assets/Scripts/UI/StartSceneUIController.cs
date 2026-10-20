using UnityEngine;
using UnityEngine.UI;

public class StartSceneUIController : MonoBehaviour
{
    [SerializeField] private GameObject _image;

    public void ShowWeaponSelectPanel()
    {
        _image.SetActive(true);
    }
}
