using System;
using UnityEngine;

public class Resource : MonoBehaviour, IPoolable , ITarget
{
    [SerializeField] private ItemData _itemData;
    public event Action<IPoolable> Removed;

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
        ResourceRegistrar.Instance.TryUnregisterEngaged(this);
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
}