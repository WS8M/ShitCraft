using System;
using UnityEngine;

public class Flag: MonoBehaviour , IPoolable, ITarget
{
    public event Action<IPoolable> Removed;

    public Vector3 Position => gameObject.transform.position;

    public void Initialize()
    {
        gameObject.SetActive(true);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void DeleteFlag()
    {
        Removed?.Invoke(this);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}