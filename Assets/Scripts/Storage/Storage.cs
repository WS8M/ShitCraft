using System;
using System.Collections.Generic;

public class Storage : IReadonlyStorage
{
    private List<StorageCell> _cells;
    
    public event Action OnStorageChanged;
    
    public IReadOnlyList<IReadonlyCell> Cells => _cells;
    
    public void AddItem(Item item)
    {
        if(_cells == null)
            _cells = new List<StorageCell>();

        bool isCellFound = false;
        
        foreach (var cell in _cells)
        {
            if (cell.Item.Id == item.Id)
            {
                isCellFound = true;
                
                if (cell.TryAddItem(item)) 
                    OnStorageChanged?.Invoke();
            }
        }

        if (isCellFound == false)
        {
            _cells.Add(new StorageCell(item));
            
            OnStorageChanged?.Invoke();
        }
    }
}