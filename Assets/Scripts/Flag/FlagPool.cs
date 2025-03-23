using System;
using UnityEngine;
using UnityEngine.Pool;

[Serializable]
public class FlagPool : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    private ObjectPool<IPoolable> _pool;
    
    public void Initialize()
    {
        _pool = new ObjectPool<IPoolable>(
            createFunc: Create,
            actionOnGet: Get,
            actionOnRelease: Release,
            actionOnDestroy: Destroy,
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }
    
    public Flag GetObject(Vector3 position)
    {
        Flag flag = _pool.Get() as Flag; 
        
        flag.transform.position = position;
        flag.Initialize();
        
        return flag;
    }
    
    private void ReturnObject(IPoolable obj)
    {
        _pool.Release(obj);
    }

    private void Release(IPoolable obj)
    {
        obj.Removed -= ReturnObject;
        obj.Hide();
    }

    private void Get(IPoolable obj)
    {
        obj.Removed += ReturnObject;
        obj.Show();
    }

    private IPoolable Create()
    {
        Flag flag = Instantiate(_flagPrefab);
        flag.Hide();
        return flag;
    }

    private void Destroy(IPoolable obj)
    {
        obj.DestroyObject();
    }
}
