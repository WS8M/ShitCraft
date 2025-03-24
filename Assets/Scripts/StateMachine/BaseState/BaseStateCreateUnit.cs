public class BaseStateCreateUnit : BaseState , IState
{
    private ConstructionCostConfig _constructionCostConfig;
    private UnitAdministrator _unitAdministrator;

    public BaseStateCreateUnit(Storage storage, Base @base, UnitAdministrator unitAdministrator,
        IStateSwitcher stateSwitcher) : base(storage, @base, stateSwitcher)
    {
        _constructionCostConfig = Base.ConstructionCostConfig;
        _unitAdministrator = unitAdministrator;
    }

    public void Enter()
    {
        Storage.Changed += TryCreateNewUnit;
    }

    public override void Exit()
    {
        Storage.Changed -= TryCreateNewUnit;
    }
    
    private void TryCreateNewUnit()
    {
        Item costItem = _constructionCostConfig.UnitCostItem.GetItem();
        int costAmount = _constructionCostConfig.UnitCostAmount;
        
        if (Storage.GetNumberOfItem(costItem) >=  costAmount)
        {
            if(Storage.TryRemoveItem(costItem, costAmount))
                _unitAdministrator.CreateNewUnit();
        }
    }
}