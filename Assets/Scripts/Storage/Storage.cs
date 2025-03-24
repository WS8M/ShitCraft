using System;
using System.Collections.Generic;
using System.Linq;

public class Storage : IReadonlyStorage
{
    private List<StorageCell> _cells;
    
    public event Action Changed;
    
    public IReadOnlyList<IReadonlyCell> Cells => _cells;
    
    public void AddItem(Item item)
    {
        if(_cells == null)
            _cells = new List<StorageCell>();

        if (TryGetCell(item, out StorageCell cell))
        {
            if (cell.TryAddItem(item))
                Changed?.Invoke();
        }
        else
        {
            _cells.Add(new StorageCell(item));

            Changed?.Invoke();
        }
    }

    public bool TryRemoveItem(Item item, int amount)
    {
        if(amount < 0)
            return false;

        if (TryGetCell(item, out StorageCell cell))
        {
            if (cell.TryRemoveItem(item, amount))
            {
                if (cell.Value == 0)
                    _cells.Remove(cell);
                
                Changed?.Invoke();
                return true;
            }
        }
        
        return false;
    }

    public int GetNumberOfItem(Item item)
    {
        int numberOfItem = 0;
        
        if(_cells.Any(cell => cell.Item.Id == item.Id))
            numberOfItem += _cells.First(cell => cell.Item.Id == item.Id).Value;
        
        return numberOfItem;
    }

    private bool TryGetCell(Item item , out StorageCell cell)
    {
        cell = _cells.FirstOrDefault(cell => cell.Item.Id == item.Id);
        
        return cell != null;
    }
}