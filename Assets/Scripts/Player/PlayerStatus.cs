using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    [Header("ステータス")]
    [SerializeField] private int _healthAmount;
    [SerializeField] private int _maxLife;

    [Header("サウンド")]
    [SerializeField] private AudioSource _damageSE;

    [Header("参照")]
    [SerializeField] private PlayerUIController _playerUI;

    private int _currentLife;

    public int Currentlife => _currentLife;
    public int MaxLife => _maxLife;


    void Awake()
    {
        _currentLife = _maxLife;
    }

    public void Heal()
    {
        if (_currentLife == _healthAmount)        //体力マックスならそもそも回復の選択肢が出ないようにするのでこの条件文はのちに削除
        {
            return;
        }

        _currentLife++;

        _playerUI.FluctuationLife(_currentLife);
    }

    public void TakeDamage()
    {
        _currentLife--;

        _playerUI.FluctuationLife(_currentLife);

        _damageSE.Play();

        Debug.Log($"残りライフ : {_currentLife}");

        if (_currentLife <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        Debug.Log("死亡しました");
        SceneManager.LoadScene("StartScene");
    }
}
