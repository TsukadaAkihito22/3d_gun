using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private int _intialValue = 10;

    private Queue<GameObject> _pool = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < _intialValue; i++)
        {
            CreateObject();
        }
    }


    /// <summary> オブジェクトプレハブを生成、無効化、オブジェクトプールへ保存を行うメソッド </summary>
    private GameObject CreateObject()
    {
        GameObject obj = Instantiate(_objectPrefab, transform);

        obj.SetActive(false);

        IPoolable poolable = obj.GetComponent<IPoolable>();

        if (poolable != null)
        {
            poolable.SetPool(this);
        }

        _pool.Enqueue(obj);     //生成したオブジェクトをオブジェクトプールにいれる

        return obj;
    }

    /// <summary> オブジェクトプールからオブジェクトを取り出し、有効化する。在庫がなければ生成メソッドを呼び出す </summary>
    /// <returns> 取り出したオブジェクト </returns>
    public GameObject Get()
    {
        if (_pool.Count == 0)
        {
            CreateObject();
        }

        GameObject obj = _pool.Dequeue();       //poolからオブジェクトを取り出す

        obj.SetActive(true);

        return obj;
    }

    /// <summary> オブジェクトプールにオブジェクトを返還する </summary>
    /// <param name="obj"> 返還するオブジェクト </param>
    public void Release(GameObject obj)
    {
        obj.SetActive(false);

        obj.transform.SetParent(transform);

        _pool.Enqueue(obj);     //オブジェクトプール(の末尾)に戻す
    }
}
