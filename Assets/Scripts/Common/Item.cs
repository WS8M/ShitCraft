public class Item
{
    private readonly ItemData _data;

    public Item(ItemData data) => _data = data;

    public string Name => _data.Name;
    public int Id => _data.Id;
}