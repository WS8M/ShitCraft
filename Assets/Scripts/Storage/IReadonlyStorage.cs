using System;
using System.Collections.Generic;

public interface IReadonlyStorage
{
    public event Action Changed;
    
    public IReadOnlyList<IReadonlyCell> Cells { get; }
}