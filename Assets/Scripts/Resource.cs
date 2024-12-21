using System;
using UnityEngine;

public class Resource : MonoBehaviour, IPoolable
{
    [SerializeField] private int _id;
    [SerializeField] private ItemData _itemData;
    
    public void Initialize(int id) => _id = id;
    
    public event Action<IPoolable> Removed;
    
    public int Id => _id;
    
    public void Take(Transform parent)
    {
        transform.SetParent(parent);
        transform.position = parent.position;
    }

    public Item Collect()
    {
        Removed?.Invoke(this);
        transform.SetParent(null);
        return _itemData.GetItem();
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}