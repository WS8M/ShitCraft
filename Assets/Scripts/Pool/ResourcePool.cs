using System;
using UnityEngine;
using UnityEngine.Pool;

[Serializable]
public class ResourcePool
{
    [SerializeField] private ResourceFabric _resourceFabric;
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
    
    public event Action<Resource> Released;
    
    public Resource GetObject(Vector3 position, Quaternion rotation)
    {
        Resource resource = _pool.Get() as Resource; 
        
        resource.transform.position = position;
        resource.transform.rotation = rotation;
        return resource;
    }
    
    private void ReturnObject(IPoolable obj)
    {
        _pool.Release(obj);
    }

    private void Release(IPoolable obj)
    {
        obj.Removed -= ReturnObject;
        
        Resource resource = obj as Resource;
        resource.gameObject.SetActive(false);
        
        Released?.Invoke(resource);
    }

    private void Get(IPoolable obj)
    {
        obj.Removed += ReturnObject;
        
        Resource resource = obj as Resource;
        resource.gameObject.SetActive(true);
        resource.gameObject.transform.position = Vector3.zero;
    }

    private IPoolable Create()
    {
        return _resourceFabric.Create();
    }

    private void Destroy(IPoolable obj)
    {
        obj.DestroyObject();
    }
}