using System;

public interface IPoolable
{
    public event Action<IPoolable> Removed;

    public void DestroyObject();
}
