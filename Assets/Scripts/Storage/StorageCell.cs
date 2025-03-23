public class StorageCell: IReadonlyCell
{
    private readonly Item _item;
    private int _value;
    
    public StorageCell(Item item, int value = 1)
    {
        _item = item;
        _value = value;
    }

    public Item Item => _item;
    public int Value => _value;
    
    public bool TryAddItem(Item item, int value = 1)
    {
        if(_item.Id != item.Id)
            return false;

        _value += value;
        return true;
    }

    public bool TryRemoveItem(Item item, int value = 1)
    {
        if (_item.Id != item.Id)
            return false;
        
        if(_value < value)
            return false;
        
        _value -= value;
        
        return true;
    }
}