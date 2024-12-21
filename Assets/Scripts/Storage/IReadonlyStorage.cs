using System;
using System.Collections.Generic;

public interface IReadonlyStorage
{
    public IReadOnlyList<IReadonlyCell> Cells { get; }
    
    public event Action OnStorageChanged;
}