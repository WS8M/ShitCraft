public class BaseStateCreateNewBase : BaseState, IState
{
    private ConstructionCostConfig _constructionCostConfig;
    private UnitAdministrator _unitAdministrator;
    
    public BaseStateCreateNewBase(Storage storage, Base @base, UnitAdministrator unitAdministrator, IStateSwitcher stateSwitcher) : base(storage, @base, stateSwitcher)
    {
        _constructionCostConfig = Base.ConstructionCostConfig;
        _unitAdministrator = unitAdministrator;
    }

    public void Enter()
    {
        Storage.StorageChanged += TryCreateNewBase;
    }

    public override void Exit()
    {
        Storage.StorageChanged -= TryCreateNewBase;
    }

    private void TryCreateNewBase()
    {
        Item costItem = _constructionCostConfig.BaseCostItem.GetItem();
        int costAmount = _constructionCostConfig.BaseCostAmount;

        if (Storage.GetNumberOfItem(costItem) < costAmount) 
            return;
        
        if (Storage.TryRemoveItem(costItem, costAmount) == false) 
            return;
        
        _unitAdministrator.SetBuildingFlag(Base.Flag);
        StateSwithcer.Enter<BaseStateCreateUnit>();
    }
}