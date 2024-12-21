using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Item Data")]
public class ItemData : ScriptableObject
{
    [field: SerializeField] public int Id{ get; private set; }
    [field: SerializeField] public string Name{ get; private set; }

    public Item GetItem() => new(this);
    
}