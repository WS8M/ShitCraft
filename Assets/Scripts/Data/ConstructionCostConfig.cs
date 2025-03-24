using UnityEngine;

[CreateAssetMenu(menuName = "Game/Construction Cost Config")]
public class ConstructionCostConfig : ScriptableObject
{
    [SerializeField] private ItemData _unitCostItem;
    [SerializeField] private int _unitCostAmount;
    [SerializeField] private ItemData _baseCostItem;
    [SerializeField] private int _baseCostAmount;

    public ItemData UnitCostItem => _unitCostItem;
    public int UnitCostAmount => _unitCostAmount;
    public ItemData BaseCostItem => _baseCostItem;
    public int BaseCostAmount => _baseCostAmount;
}