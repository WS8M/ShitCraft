using System;
using UnityEngine;

public class Resource : MonoBehaviour, IPoolable , ITarget
{
    [SerializeField] private ItemData _itemData;
    
    private bool _isEngaged;
    
    public event Action<IPoolable> Removed;

    public bool IsEngaged => _isEngaged;
    public Vector3 Position => gameObject.transform.position;

    public void Take(Transform parent)
    {
        transform.SetParent(parent);
        transform.position = parent.position;
    }

    public Item Collect()
    {
        Removed?.Invoke(this);
        transform.SetParent(null);
        _isEngaged = false;
        return _itemData.GetItem();
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    public void MakeEngaged() => 
        _isEngaged = true;
}